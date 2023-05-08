﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Infra.Model.Data;
using Infra.Model.Game;
using Infra.Model.Resource;
using Module.Game.Battle;
using UnityEngine;

namespace Module.Game.Event
{
    /// <summary>
    /// [TODO] 확인하기 MonoBehaviour로 선언된 객체가 게임 오브젝트로 없어도, 코루틴이 정상동작하는지 확인하기
    /// </summary>
    internal class CommonBattle : IEventItem
    {
        public CommonBattle(UIGame uiGame)
        {
            UIGame = uiGame;
        }

        #region Variables
        private UIGame UIGame { get; set; }
        private UIBattle UIBattle { get; set; }
        private BattleEvent BattleEvent { get; set; }
        private IEnumerator Iterator { get; set; }
        #endregion
        
        private void Init(BattleEvent battleEvent)
        {
            if (battleEvent == null)
                return; // 절대 여기로 돌면 안됨
           
            // 초기값 설정
            UIBattle = UIGame.uIBattle;
            Iterator = BattleIterator().GetEnumerator();
            BattleEvent = battleEvent;

            UIBattle.UnitList = UIGame.GameData.UnitList;
            foreach (var index in BattleEvent.Enemies)
            {
                var enemy =  Factory.EnemyFactory(index);
                enemy?.Init((EnemyType)index);
                UIBattle.EnemyList.Add(enemy);
            }
            
            UIBattle.UpdateView();
            
            // 전투 시퀀스 타기
            Next();
        }

        public void Execute()
        {
            Init(UIGame.CurrentSpot.Event as BattleEvent);
        }

        /// <summary>
        /// 스핀 후 이벤트
        /// 블럭 효과 적용 및 유닛들의 행동 부여 
        /// </summary>
        private void Spun(Block[,] blocks)
        {
            // 블럭 효과 적용
            foreach (var block in blocks)
            {
                block.PreExecute(blocks);
            }
            
            // 블럭 효과 적용
            foreach (var block in blocks)
            {
                block.Execute();
            }
            
            UIBattle.UnitList.ForEach(unit => unit.Execute());
            
            // 다음 시퀀스 진행
            Next();   
        }

        private void Next() => Iterator.MoveNext();
        private void End()
        {
            // Clear
            BattleEvent = null;
            Iterator = null;
            UIGame.EndEvent();
        }

        #region Iteratoer
        /// <summary>
        /// 이터레이턴 패턴으로 구현된 전투 로직
        /// </summary>
        /// <returns></returns>
        private IEnumerable BattleIterator()
        {
            // [TODO] 스테이지 아티펙트 효과 활성화

            while (true)
            {
                // [TODO] 턴 아티펙트 효과 활성화

                // 스핀 허용 상태로 설정 후, 스핀 대기
                UIGame.SpinEvent(Spun); 
                yield return null;

                // 사용자 턴 진행 완료 대기.
                UIGame.ExecuteActionSelector(Next ,CheckAllEnemyDie);
                yield return null;
                
                if (CheckAllEnemyDie())
                {
                    // 모든 적이 죽은 경우 종료
                    UIGame.Reward(GetBattleReword());
                    yield break;
                }
                
                // 적의 턴
                foreach (var enemy in UIBattle.EnemyList.Where(e => e.CanAction()))
                {
                    enemy.Execute();
                }
            }
        }

        private bool CheckAllEnemyDie() => UIBattle.EnemyList.All(e => e.State == State.Die);
        private bool CheckAllUnitDie() => UIBattle.EnemyList.All(e => e.State == State.Die);
        #endregion

        #region Coroutine
// private IEnumerator BattleCoroutine()
        // {
        //     if (BattleEvent == null) yield break;
        //     
        //     // [TODO] 초기화 로직
        //     var waitUntil = new WaitUntil(() => UIEvent.SpinEndFlag);
        //     
        //     // [TODO] 스테이지 아티펙트 효과 활성화
        //     while (true)
        //     {
        //         UIEvent.SpinEndFlag = false;
        //         
        //         // [TODO] 턴 아티펙트 효과 활성화
        //         
        //
        //         UIEvent.Setting = false;
        //         
        //         // Spin 대기
        //         yield return new WaitUntil(() => UIEvent.SpinEndFlag);
        //
        //         UIEvent.Setting = true;
        //         if (UIEvent.EnemyList.All(e => e.State == State.Die))
        //         {
        //             // 모든 적이 죽은 경우 종료
        //             UIEvent.Reward(GetBattleReword());
        //             break;
        //         }
        //         
        //         // 적의 턴
        //         foreach (var enemy in UIEvent.EnemyList.Where(e=> e.CanAction()))
        //         {
        //             enemy.Execute();;
        //         }
        //     }
        //
        //     // Clear
        //     BattleEvent = null;
        //     UIEvent.EndEvent();
        // }
        #endregion
        

        private Reward GetBattleReword()
        {
            return new Reward();
        }

    }
}
﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infra.Model.Data;
using Infra.Model.Game;
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
        private BattleEvent BattleEvent { get; set; }
        #endregion
        
        public void Init(BattleEvent battleEvent)
        {
            if (battleEvent == null)
                return; // 절대 여기로 돌면 안됨
            
            BattleEvent = battleEvent;
            
        }

        public void Execute()
        {
            // [TODO] 스테이지 아티펙트 효과 활성화
            // [TODO] 턴 아티펙트 효과 활성화
            UIGame.SpinEvent(Spin);
        }

        private void Spin()
        {
            if (UIGame.EnemyList.All(e => e.State == State.Die))
            {
                // 모든 적이 죽은 경우 종료
                UIGame.Reward(GetBattleReword());
            }
            else // 적의 턴
            {
                foreach (var enemy in UIGame.EnemyList.Where(e=> e.CanAction()))
                {
                    enemy.Execute();;
                }
            }
            
            // [TODO] 턴 아티펙트 효과 활성화
            UIGame.SpinEvent(Spin);
        }

        private void End()
        {
            // Clear
            BattleEvent = null;
            UIGame.EndEvent();
        }
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

        private Reward GetBattleReword()
        {
            return new Reward();
        }

    }
}
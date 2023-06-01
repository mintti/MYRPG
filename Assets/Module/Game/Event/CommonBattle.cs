using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        public UIGame UIGame { get; set; }
        private UIBattle UIBattle { get; set; }
        private BattleEvent BattleEvent { get; set; }
        private IEnumerator Iterator { get; set; }
        private List<Reward> Rewards { get; set; } = new();

        private bool CheckFlag
        {
            get => UIBattle.WhoseDieFlag;
            set => UIBattle.WhoseDieFlag = value;
        }

        #endregion
        
        private void Init(BattleEvent battleEvent)
        {
            if (battleEvent == null)
            {
                return; // 절대 여기로 돌면 안됨
            }
           
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

        public void UpdateUIGame(UIGame uiGame)
        {
            UIGame = uiGame;
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
            foreach (var block in blocks)
            {
                block.Init();
            }
            
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

        private void Win()
        {
            // Reward 적용
            UIGame.GameData.GetReward(Rewards);
                
            // Clear
            BattleEvent = null;
            Iterator = null;
            Rewards.Clear();
            
            // UI Clear
            UIBattle.Clear();

            End();
        }

        private void End(bool clear = true)
        {
            UIGame.EndEvent(clear);
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

                if (CheckFlag)
                {
                    if (CheckAllEnemyDie())
                    {
                        // 모든 적이 죽은 경우 종료
                        Rewards = GetBattleReword().ToList();
                        UIGame.Reward(Rewards, Win);
                        yield break;
                    }

                    CheckFlag = false;
                }
                
                // 적의 턴
                foreach (var enemy in UIBattle.EnemyList.Where(e => e.CanAction()))
                {
                    enemy.Execute();
                    
                    if (CheckFlag)
                    {
                        if (CheckAllUnitDie()) // 전체 전멸
                        {
                            End();
                            break;
                        }
                        CheckFlag = false;
                    }
                }
            }
        }

        private bool CheckAllEnemyDie() => UIBattle.EnemyList.All(e => e.State == State.Die);
        private bool CheckAllUnitDie() => UIBattle.EnemyList.All(e => e.State == State.Die);
        #endregion
        
        private IEnumerable<Reward> GetBattleReword()
        {
            IEnumerable<Reward> result = new List<Reward>();

            foreach (var enemy in UIBattle.EnemyList)
            {
                var reward = enemy.BaseEnemy.GetReward();
                result = result.Concat(reward);
            }

            int gold = 0;
            foreach (var reward in result.Where(x=> x.Type == RewardType.Gold))
            {
                gold += reward.Value;
            }
            var list = result.Where(x => x.Type != RewardType.Gold).ToList();
            
            var sumGold = new Reward(RewardType.Gold, gold);
            list.Add(sumGold);

            return list;
        }
    }
}
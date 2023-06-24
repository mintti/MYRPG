using System.Collections.Generic;
using System.Linq;
using Infra.Model.Game;
using Module.Game.Slot;
using UnityEngine;

namespace Module.Game.Battle
{
    internal class UIBattle : BaseMonoBehaviour, IBattleController
    {
        #region Variables
        public UIGame UIGame { get; private set; }

        public List<Unit> UnitList { get; set; }= new();
        public List<Enemy> EnemyList { get; set; } = new();

        private List<UIEntityState> UIEttSttList { get; set; } = new();

        public Camera mainCamera;
        public bool WhoseDieFlag { get; set; }
        #region External 
        public UIEntity[] UIUnits = new UIEntity[4];
        public UIEntity[] UIEnemies = new UIEntity[4];

        public GameObject uiEttSttPrefab;
        public Transform ettSttContentTr;
        #endregion

        #endregion


        public void Init(UIGame uiGame)
        {
            UIGame = uiGame;
            
            UnitList.Clear();
            EnemyList.Clear();
            
            foreach (var uiEntity in UIUnits.Concat(UIEnemies))
            {
                uiEntity.Init(this);
                var ettstt = Instantiate(uiEttSttPrefab, ettSttContentTr).GetComponent<UIEntityState>();
                UIEttSttList.Add(ettstt);
            }
        }

        /// <summary>
        /// Entity 지정
        /// </summary>
        public void UpdateView()
        {
            int etstIdx = 0;
            UIEttSttList.ForEach(x=> x.Clear());
            
            // [TODO] Entity 객체 동적 생성 가능하도록 고안해보기 
            for (int i = 0, cnt = UnitList.Count; i < cnt; i++)
            {
                if (UnitList[i].State == State.Alive)
                {
                    UIUnits[i].SetEntity(UnitList[i], UIEttSttList[etstIdx++]);
                }
            }
            
            for (int i = 0, cnt = EnemyList.Count; i < cnt; i++)
            {
                UIEnemies[i].SetEntity(EnemyList[i], UIEttSttList[etstIdx++]);  
            }
        }

        /// <summary>
        /// 이벤트 종료 시 호출
        /// </summary>
        public void Clear()
        {
            foreach (var uiEntity in UIUnits.Concat(UIEnemies))
            {
                uiEntity.Clear();
            }
            EnemyList.Clear();
        }

        #region IBattleController
        public IEnumerable<BattleEntity> GetUnits()
        {
            return UnitList.Where(x=> x.State == State.Alive);
        }

        public UIActionSelector UIActionSelector => UIGame.uIActionSelector;

        public void UpdateEntityState()
        {
            WhoseDieFlag = true;
        }

        /// <summary>
        /// Entity 객체가 죽었을 때 알림
        /// </summary>
        public void UpdateEntityState(Unit unit)
        {
            UIGame.RemoveBlock(unit);
        }
        #endregion

    }
}

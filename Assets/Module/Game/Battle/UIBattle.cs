using System.Collections.Generic;
using System.Linq;
using Infra.Model.Game;
using UnityEngine;

namespace Module.Game.Battle
{
    internal class UIBattle : MonoBehaviour, IBattleController
    {
        #region Variables
        private UIGame UIGame { get; set; }

        public List<Unit> UnitList { get; set; }= new();
        public List<Enemy> EnemyList { get; set; } = new();

        #region External 
        public UIEntity[] UIUnits = new UIEntity[4];
        public UIEntity[] UIEnemies = new UIEntity[4];
        #endregion

        #endregion


        public void Init(UIGame uiGame)
        {
            UIGame = uiGame;
            
            UnitList.Clear();
            EnemyList.Clear();
            
            foreach (var uiEntity in UIUnits.Concat(UIEnemies))
            {
                uiEntity.Init();
            }
        }

        public void UpdateView()
        {
            var unitList = UnitList;
            var enemyList = EnemyList;
            for (int i = 0, cnt = unitList.Count; i < cnt; i++)
            {
                UIUnits[i].SetEntity(UnitList[i]);    
            }
            
            for (int i = 0, cnt = enemyList.Count; i < cnt; i++)
            {
                UIEnemies[i].SetEntity(enemyList[i]);    
            }
        }

        public void Clear()
        {
            foreach (var uiEntity in UIUnits.Concat(UIEnemies))
            {
                uiEntity.Clear();
            }
        }
        
        public IEnumerable<BattleEntity> GetUnits()
        {
            return UnitList.Where(x=> x.State == State.Alive);
        }
        
    }
}

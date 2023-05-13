using System.Collections.Generic;
using System.Linq;
using Infra.Model.Game;
using UnityEngine;

namespace Module.Game.Battle
{
    internal class UIBattle : MonoBehaviour, IBattleController
    {
        #region Variables
        public UIGame UIGame { get; private set; }

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
                uiEntity.Init(this);
            }
        }

        public void UpdateView()
        {
            for (int i = 0, cnt = UnitList.Count; i < cnt; i++)
            {
                UIUnits[i].SetEntity(UnitList[i]);
            }
            
            for (int i = 0, cnt = EnemyList.Count; i < cnt; i++)
            {
                UIEnemies[i].SetEntity(EnemyList[i]);  
            }
        }

        public void Clear()
        {
            foreach (var uiEntity in UIUnits.Concat(UIEnemies))
            {
                uiEntity.Clear();
            }
        }

        #region IBattleController
        public IEnumerable<BattleEntity> GetUnits()
        {
            return UnitList.Where(x=> x.State == State.Alive);
        }

        public  UIActionSelector UIActionSelector => UIGame.uIActionSelector;

        #endregion

    }
}

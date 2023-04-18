using System.Linq;
using UnityEngine;

namespace Module.Game.Battle
{
    internal class UIBattle : MonoBehaviour
    {
        #region Variables
        private UIGame UIGame { get; set; }

        #region External 
        public UIEntity[] UIUnits = new UIEntity[4];
        public UIEntity[] UIEnemies = new UIEntity[4];
        #endregion


        public void Init()
        {
            foreach (var uiEntity in UIUnits.Concat(UIEnemies))
            {
                uiEntity.Init();
            }
        }

        public void UpdateView()
        {
            var unitList = UIGame.UnitList;
            var enemyList = UIGame.EnemyList;
            for (int i = 0, cnt = unitList.Count; i < cnt; i++)
            {
                UIUnits[i].SetEntity(unitList[i]);    
            }
            
            for (int i = 0, cnt = enemyList.Count; i < cnt; i++)
            {
                UIEnemies[i].SetEntity(enemyList[i]);    
            }
        }


        #endregion


    }
}

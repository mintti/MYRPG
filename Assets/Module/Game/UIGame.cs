using Infra.Model.Data;
using Infra.Model.Game;
using Module.Game.Battle;
using Module.Game.Map;
using UnityEngine;

namespace Module.Game
{
    internal class UIGame : MonoBehaviour
    {
        #region Variables
        private GameManager GameManager { get; set; }

        #region Button
        public void B_Spin() => UIEvent.Spin();
        public void B_Map() => Map();
        public void B_Option() => Option();
        #endregion
        private UIEvent UIEvent { get; set; }
        public UIMap uiMap;
        
        #endregion
        /// <summary>
        /// 씬로드 후 수행되는 첫 이벤트.
        /// </summary>
        private void Start()
        {
            GameManager = GameManager.Instance;
            UIEvent = GetComponent<UIEvent>();

            UIEvent.Init(this);
            uiMap.Init(this);
        }

        #region Event-Related
        public void SelectMap(Spot spot) => UIEvent.SetEvent(spot);

        #endregion


        #region Default Function
        #region Map


        private void Map()
        {
            
        }

        #endregion
        
        #region Option

        private void Option()
        {
            
        }
        
        

        #endregion
        #endregion

    }
}

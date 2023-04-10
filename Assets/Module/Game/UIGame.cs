using Infra.Model.Data;
using Infra.Model.Game;
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
        public UIOption uiOption;
        
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

        /// <summary>
        /// UIMap에서 맵 선택 이벤트 발생시 수행
        /// </summary>
        public void SelectMap(Spot spot)
        {
            UIEvent.SetEvent(spot);
            Map(false);
        }
        #endregion


        #region Default Function
        private void Map(bool state = true)
        {
            uiMap.gameObject.SetActive(state);
        }

        private void Option(bool state = true)
        {
            uiOption.gameObject.SetActive(state);
        }
        #endregion

    }
}

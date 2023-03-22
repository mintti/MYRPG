using UnityEngine;
 
namespace Module.Game.Map
{
    internal class UIMap : MonoBehaviour
    {
        #region Variables
        private UIGame UIGame { get; set; }
        
        public GameObject spotPrefab;
        public Transform startPoint;
        #endregion
        
        public void Init(UIGame game)
        {
            UIGame = game;
        }
    }
}

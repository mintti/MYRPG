using UnityEngine;

namespace Module.Game.Battle
{
    internal class UIBattle : MonoBehaviour
    {
        #region Variables
        private UIGame UIGame { get; set; }
        #endregion


        public void Init(UIGame uiGame)
        {
            UIGame = uiGame;

        }
    }
}

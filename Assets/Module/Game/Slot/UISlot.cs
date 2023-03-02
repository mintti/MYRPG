using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Module.Game.Slot
{
    public class UISlot : MonoBehaviour
    {
        private UIGame UIGame { get; set; }

        public void Init(UIGame uiGame)
        {
            UIGame = uiGame;
            
        }
    }
}

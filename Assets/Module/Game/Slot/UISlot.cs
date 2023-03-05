using System.Collections.Generic;
using UnityEngine;

namespace Module.Game.Slot
{
    public class UISlot : MonoBehaviour
    {
        private UIGame UIGame { get; set; }
        private List<Block> BaseBlockList { get; set; }
        public void Init(UIGame uiGame, List<Block> baseBlocks) 
        {
            UIGame = uiGame;
            BaseBlockList = baseBlocks;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace Module.Game.Slot
{
    public class UIBlock : MonoBehaviour
    {
        public Image backgroundColor;
        public Image icon;
        
        public void Set(Block block)
        {
            backgroundColor.color = block.Color;
            icon.sprite = block.Sprite;
        }
    }
}

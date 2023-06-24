using Infra.Model.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Module.Game.Slot
{
    internal class UIBlock : BaseMonoBehaviour
    {
        public Image backgroundColor;
        public Image icon;
        
        public void Set(Block block)
        {
            backgroundColor.color = block.Color - new Color(0, 0, 0, 0.7f);;
            icon.sprite = block.Sprite;
        }
    }
}

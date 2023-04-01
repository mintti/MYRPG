using System.Collections.Generic;
using Infra.Model.Game;
using Module.Game.Battle;
using UnityEngine;
namespace Module.Game.Slot
{
    internal class UISlot : MonoBehaviour
    {
        #region Variables
        private UIEvent UIEvent { get; set; }
        
        public RectTransform slotRectTr;
        public Transform blockContentTr;
        public GameObject blockPrefab;
        
        private List<UIBlock> Blocks { get; set; }
        #endregion
        
        public void Init(UIEvent uiGame, int width, int height) 
        {
            UIEvent = uiGame;
            
            var rect = slotRectTr.rect;
            rect.width = width * 100;
            rect.height = height * 100;

            Blocks = new List<UIBlock>();
            for (int i = 0, cnt = width * height * 3; i < cnt; i++)
            {
                var obj = Instantiate(blockPrefab, blockContentTr);
                Blocks.Add(obj.GetComponent<UIBlock>());
            }
        }

        /// <summary>
        /// 스핀 후, 결과물을 슬롯 블럭에 반영
        /// </summary>
        public void SetBlocks(IEnumerable<Block> seq)
        {
            var index = 0;
            var length = Blocks.Count * 3;

            do
            {
                foreach (var block in seq)
                {
                    Blocks[index++].Set(block);
                }
            } while (index < length);
        }
    }
}

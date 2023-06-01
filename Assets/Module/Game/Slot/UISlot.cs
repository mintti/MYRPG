using System;
using System.Collections.Generic;
using Infra.Model.Game;
using UnityEngine;
namespace Module.Game.Slot
{
    internal class UISlot : BaseMonoBehaviour
    {
        #region Variables
        private UIGame UIGame { get; set; }
        
        public RectTransform slotRectTr;
        public Transform blockContentTr;
        public GameObject blockPrefab;
        
        private List<UIBlock> Blocks { get; set; }
        #endregion

        public void Init(UIGame uiGame)
        {
            UIGame = uiGame;
        }

        public void CreateSlot(int width, int height) 
        {
            slotRectTr.sizeDelta = new Vector2(width * 100, height * 100);

            Blocks = new List<UIBlock>();
            for (int i = 0, cnt = width * height; i < cnt; i++)
            {
                var obj = Instantiate(blockPrefab, blockContentTr);
                Blocks.Add(obj.GetComponent<UIBlock>());
            }
        }

        public void CreateBlock()
        {
            
        }

        /// <summary>
        /// 스핀 후, 결과물을 슬롯 블럭에 반영
        /// </summary>
        public void SetBlocks(IEnumerable<Block> seq)
        {
            var index = 0;
            var length = Blocks.Count;

            do
            {
                foreach (var block in seq)
                {
                    Blocks[index++].Set(block);
                }
            } while (index < length);
        }

        public void SpinAnimation()
        {
            
        }
    }
}

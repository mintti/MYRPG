using System;
using System.Collections;
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
        private List<UIBlock> DummyBlocks { get; set; }

        private int _height; // 슬롯 세로 크기
        private int _augmenter; // 슬롯 애니메이션이 증가값
        #endregion

        public void Init(UIGame uiGame)
        {
            UIGame = uiGame;
        }

        public void CreateSlot(int width, int height) 
        {
            _height = height;
            _augmenter = 15 * height;
            slotRectTr.sizeDelta = new Vector2(width * 100, height * 100);
            
            Blocks = new List<UIBlock>();
            for (int i = 0, cnt = width * height; i < cnt; i++)
            {
                var obj = Instantiate(blockPrefab, blockContentTr);
                Blocks.Add(obj.GetComponent<UIBlock>());
            }
            
            // 더미도 생성
            DummyBlocks = new List<UIBlock>();
            for (int i = 0, cnt = width * height; i < cnt; i++)
            {
                var obj = Instantiate(blockPrefab, blockContentTr);
                DummyBlocks.Add(obj.GetComponent<UIBlock>());
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
                    Blocks[index].Set(block);
                    DummyBlocks[index++].Set(block);
                }
            } while (index < length);
        }

        public IEnumerator SpinAnimation()
        {
            var rect = blockContentTr.GetComponent<RectTransform>();
            
            int targetTop = 100 * _height;
            
            for (int i = 0; i < 3; i++)
            {
                for (int top = 0; top <= targetTop; top += _augmenter)
                {
                    rect.anchoredPosition = new Vector3(0, top, 0);
                    yield return new WaitForSeconds(0.01f);
                }
            }

            rect.anchoredPosition = Vector3.zero;
        }
    }
}

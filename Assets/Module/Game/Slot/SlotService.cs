using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Game.Slot
{
    
    public delegate void Del(Block block);
    
    public class SlotService
    {
        #region Variables

        private Random Random { get; set; }
        private Dictionary<int, Block> BlockDict { get; set; }
        private List<Block> OutputBlockList { get; set; }
        private StringBuilder Builder { get; set; }  = new StringBuilder();
        
        #endregion

        #region Method
        /// <summary>
        /// 스테이지 시작 후 스핀 전, 모든 블럭에 효과 적용
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="stageEffectDel"></param>
        private void ApplyEffectBeforeSpin(List<Block> blocks, Del stageEffectDel)
        {
            foreach (var block in blocks)
            {
                stageEffectDel?.Invoke(block);
                block.WeightBackup = block.Weight;
            }
        }
        
        /// <summary>
        /// 소지한 블럭 정보와 슬롯 사이즈를 입력받아 랜덤한 블럭 시퀀스 반환
        /// 일회성 블럭 효과(턴)를 입력받아 적용
        /// </summary>
        /// <param name="blocks">플레이어가 소지한 블럭들</param>
        /// <param name="width">슬롯의 가로 크기</param>
        /// <param name="height">슬롯의 세로 크기</param>
        /// <param name="turnEffectDel">턴 단위로 소멸되는 일회성 블럭효과</param>
        /// <returns>랜덤한 블럭 스퀀스</returns>
        private IEnumerable<Block> GetRandomBlock(List<Block> blocks, int width, int height, Del turnEffectDel = null)
        {
            Random ??= new Random();
            Builder ??= new StringBuilder();
            OutputBlockList ??= new List<Block>();
            BlockDict ??= new Dictionary<int, Block>();
            
            Builder.Clear();
            OutputBlockList.Clear();
            BlockDict.Clear();
            
            
            // 블럭엔 효과 적용 전 옵션과 적용 후 옵션이 둘 다 적혀 있을 것
            // 블럭 효과 적용 및 랜덤블럭에 추가
            var index = 1;
            foreach (var block in blocks)
            {
                block.Weight = block.WeightBackup; // 아티펙트 적용된 블럭 가중치 초기화
                turnEffectDel?.Invoke(block);

                // 블럭 넣기
                var weight = (int)(block.Weight * 100);
                BlockDict.Add(index, block);
                for (var i = 0; i < weight; i++) Builder.Append((char)index); 
                index++;
            }


            // 블럭 랜덤 뽑기
            List<int> temp = new ();
            var pouch = Builder.ToString();
            for (int i = 0, c = width * height; i < c; i++)
            {
                var randVal = Random.Next(0, pouch.Length);

                var value = pouch[randVal];
                temp.Add((int)value);
                pouch = pouch.Replace($"{value}", "");
            }
            
            // 한번 더 랜덤 섞기
            for (var i = temp.Count; i > 0; i--)
            {
                var randomIdx = Random.Next(0, i);
                OutputBlockList.Add(BlockDict[temp[randomIdx]]);
            }
                
            return OutputBlockList;
        }
        
        #endregion
    }
}
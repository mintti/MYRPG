using System.Collections.Generic;
using System.Linq;
using Infra.Model.Data;
using UnityEngine;

namespace Module.WorldMap
{
    internal class UIWorldMap : MonoBehaviour
    {
        private GameManager GameManager { get; set; }

        #region External Variables
        public UIDungeon[] UIDungeons;

        #endregion
        
        // Start is called before the first frame update
        private void Start()
        {
            GameManager = GameManager.Instance;

            for (int i = 0, cnt = GameManager.GameData.DungeonList.Count /*UIDungeons.Length*/; i < cnt; i++)
            {
                var type = (DungeonType) (i + 1);
                UIDungeons[i].Init(this, type, GameManager.GameData.DungeonList[i]);    
            }
        }


        #region WorldMap
        public void SelectDungeon(DungeonType type) => GameManager.MoveGameScene(type);

        private void CreateMapSpot(int maxDepth, int maxWidth)
        {
            Spot firstSpot;

            // 뎁스별 Spot 갯수 설정
            var widthList = new List<int>();
            int sum = 0;
            for (var i = 1; i < maxDepth - 1; i++)
            {
                int count = Random.Range(2, maxWidth);
                sum += count;
                widthList.Add(count);
            }

            var possibleEventList = new List<int>()
            {
                
            };
                
            // 이벤트 생성
            for (var i = 0; i < sum; i++)
            {
                
            }
            
            for (int i = 1; i < maxDepth - 1; i++)
            {
                for (int j = 0; j < widthList[i]; j++)
                {
                    
                }
            }
            GameManager.SaveData();
        }
        
        #endregion
    }
}

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
            // 세팅
            GameManager = GameManager.Instance;
            for (int i = 0, cnt = GameManager.GameData.DungeonList.Count ; i < cnt; i++)
            {
                UIDungeons[i].Init(this, GameManager.GameData.DungeonList[i]);
            }
            
            // 현재 진행 중인 던전이 존재
            if (GameManager.GameData.Map != null)
            {
                SelectDungeon();
            }
        }


        #region WorldMap

        public void SelectDungeon(Dungeon dungeon = null)
        {
            // 사용자가 던전을 선택한 경우
            if (dungeon != null)
            {
                var spot = DataUtils.GetRandomMap(dungeon);
                GameManager.GameData.Map = spot;
            } 
            
            // 씬 이동
            GameManager.MoveGameScene();  
        }

        #endregion
    }
}

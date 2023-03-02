using System.Linq;
using UnityEngine;

namespace Module.WorldMap
{
    public class UIWorldMap : MonoBehaviour
    {
        private GameManager GameManager { get; set; }

        #region External Variables
        public UIDungeon[] UIDungeons;

        #endregion
        
        // Start is called before the first frame update
        private void Start()
        {
            GameManager = FindObjectsOfType<GameManager>().First();

            for (int i = 0, cnt = UIDungeons.Length; i < cnt; i++)
            {
                var type = (DungeonType) (i + 1);
                UIDungeons[i].Init(this, type, GameManager.PlayerData.DungeonList[i]);    
            }
        }


        #region WorldMap
        public void SelectDungeon(DungeonType type) => GameManager.MoveGameScene(type);
        #endregion
    }
}

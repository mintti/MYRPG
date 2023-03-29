using Infra.Model.Data;
using Infra.Model.Game;
using UnityEngine;
using UnityEngine.UIElements;

namespace Module.WorldMap
{
    internal class UIDungeon : MonoBehaviour
    {
        private UIWorldMap WorldMap { get; set; }
        
        #region Variables
        private  Dungeon DungeonData { get; set; }
        private bool IsEnable => DungeonData.IsEnable;
        #endregion
        
        /// <summary>
        /// 월드 맵 게임 오브젝트 값 초기화
        /// </summary>
        /// <param name="worldMap">Parent Object</param>
        /// <param name="type">UI Object Index(enum)</param>
        /// <param name="dungeon">A Dungeon Data of Player</param>
        public void Init(UIWorldMap worldMap, Dungeon dungeon)
        {
            WorldMap = worldMap;
            DungeonData = dungeon;
        }

        #region User Behavior
        private void OnMouseUp()
        {
            if(IsEnable) WorldMap.SelectDungeon(DungeonData);
        }

        #endregion
    }
}

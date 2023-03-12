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
        private DungeonType Type { get; set; }
        #endregion
        
        /// <summary>
        /// 월드 맵 게임 오브젝트 값 초기화
        /// </summary>
        /// <param name="worldMap">Parent Object</param>
        /// <param name="type">UI Object Index(enum)</param>
        /// <param name="dungeon">A Dungeon Data of Player</param>
        public void Init(UIWorldMap worldMap, DungeonType type, Dungeon dungeon) // [TODO] Dungeon 매개변수 다른 타입으로 생각해보기
        {
            WorldMap = worldMap;
            Type = type;
            DungeonData = dungeon;
        }

        #region User Behavior
        private void OnMouseUp()
        {
            if(IsEnable) WorldMap.SelectDungeon(Type);
        }

        #endregion
    }
}

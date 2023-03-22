using System;
using System.Collections.Generic;
using System.Linq;
using Infra;
using Infra.Model;
using Infra.Model.Resource;
using Module.WorldMap;
using Color = System.Drawing.Color;

namespace Module
{
    /// <summary>
    /// 전체 리소스 관리
    /// </summary>
    internal class ResourceManager : Singleton<ResourceManager>
    {
        #region Initializer
        public ResourceManager()
        {
            #region 테스트 코드
            Jobs = new List<Job>()
            {
                new (0, "테스트", 20, Color.Black),
                new (1, "Warrior", 20, Color.Blue),
                new (2, "Wizard", 20, Color.Purple),
                new (3, "Archer", 20, Color.Green),
                new (4, "Knight", 20, Color.LightSalmon),
                new (5, "Priest", 20, Color.Yellow),
            };

            Dungeons = new List<Dungeon>()
            {
                new (0, "테스트 던전", 1, new (){1, 2}, new List<int>())
            };

            DungeonEvents = new List<DungeonEvent>()
            {
                new(0, "테스트 이벤트", DungeonEventType.None), 
                new(1, "아티펙트", DungeonEventType.Event),
                new(2, "체력 회복 이벤트", DungeonEventType.Event),
            };

            Enemies = new List<Enemy>()
            {
                new("테스트 몬스터")
            };

            #endregion
        }
        #endregion

        #region Variables

        #region Core Data ★★★
        public List<Job> Jobs { get; }

        public List<Dungeon> Dungeons { get; }

        public List<DungeonEvent> DungeonEvents { get;  }

        public List<Enemy> Enemies  { get;  }
        #endregion
        
        public List<(int depth, int width)> MapSizeByDungeonLevel= new ()
        {
            (0,0), // never using
            (4,2),
            (0,0),
            (0,0),
            (0,0),
            (0,0),
        };

        #endregion

        
    }
}

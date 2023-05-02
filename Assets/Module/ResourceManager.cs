using System;
using System.Collections.Generic;
using System.Linq;
using Infra;
using Infra.Model;
using Infra.Model.Resource;
using Module.WorldMap;
using UnityEngine;
using EventType = Infra.Model.Resource.EventType;

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

            var jobSprites = Resources.LoadAll<Sprite>("Sprite/Job");
            Jobs = new List<Job>()
            {
                new (0, "테스트", 20, Color.black),
                new (1, "Warrior", 20, Color.blue, jobSprites[0]),
                new (2, "Wizard", 20, Color.magenta, jobSprites[1]),
                new (3, "Archer", 20, Color.green, jobSprites[2]),
                new (4, "Knight", 20, Color.cyan, jobSprites[3]),
                new (5, "Priest", 20, Color.yellow, jobSprites[4]),
            };
            Dungeons = new List<Dungeon>()
            {
                new (0, "테스트 던전", 1, new (){ 0}, new List<int>(){ (int)EventType.HealingLake, (int)EventType.GetArtifact})
            };


            Enemies = new List<Enemy>()
            {
                new("테스트 몬스터", 10, 5, Resources.Load<Sprite>("Sprite/enemy"))
            };

            BlockSprites = Resources.LoadAll<Sprite>("Sprite/block").ToList();

            #endregion
        }
        #endregion

        #region Variables

        #region Core Data ★★★
        public List<Job> Jobs { get; }

        public List<Dungeon> Dungeons { get; }

        public List<Enemy> Enemies  { get;  }
        #endregion
        
        public readonly List<(int depth, int width)> MapSizeByDungeonLevel= new ()
        {
            (0,0), // never using
            (4,2),
            (0,0),
            (0,0),
            (0,0),
            (0,0),
        };

        public readonly (float battle, float elete, float @event, float rest) EventPercentage 
            = new (0.6f, 0.0f, 0.3f, 0.1f);

        public List<Sprite> BlockSprites { get; }
        #endregion
    }
}

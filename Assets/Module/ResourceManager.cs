using System;
using System.Collections.Generic;
using System.Linq;
using Infra;
using Infra.Model;
using Infra.Model.Data;
using Infra.Model.Resource;
using Module.WorldMap;
using UnityEngine;
using Dungeon = Infra.Model.Resource.Dungeon;
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
                new ((int)JobType.Test, "테스트", 20, Color.black),
                new ((int)JobType.Warrior, "Warrior", 10, Color.blue, jobSprites[0]),
                new ((int)JobType.Wizard, "Wizard", 10, Color.magenta, jobSprites[1]),
                new ((int)JobType.Archer, "Archer", 10, Color.green, jobSprites[2]),
                new ((int)JobType.Knight, "Knight", 10, Color.cyan, jobSprites[3]),
                new ((int)JobType.Priest, "Priest", 10, Color.yellow, jobSprites[4]),
            };
            Dungeons = new List<Dungeon>()
            {
                new (0, "테스트 던전", 1, new (){ 0}, new List<int>(){ (int)EventType.HealingLake, (int)EventType.GetArtefact})
            };

            List<Reward> defaultRewards = new() { new Reward(RewardType.Gold, 100, 10) };
            Enemies = new List<Enemy>()
            {
                new("테스트 몬스터", 15, 5, Resources.Load<Sprite>("Sprite/enemy")){Rewards = defaultRewards}
            };

            BlockSprites = Resources.LoadAll<Sprite>("Sprite/block").ToList();

            MapSprites = new ();
            var temps = Resources.LoadAll<Sprite>($"Sprite/Map");
            foreach (var name in Enum.GetNames(typeof(SpotEventType)))
            {
                MapSprites.Add(temps.FirstOrDefault(x => x.name.Equals($"Map_{name}")));
            }
            
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
            (4,4),
            (0,0),
            (0,0),
            (0,0),
            (0,0),
        };

        public readonly (float battle, float elete, float @event, float rest) EventPercentage 
            = new (0.6f, 0.0f, 0.4f, 0.0f);

        public List<Sprite> BlockSprites { get; }
        
        public List<Sprite> MapSprites { get; }
        #endregion
    }
}

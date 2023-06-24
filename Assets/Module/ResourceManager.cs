using System;
using System.Collections.Generic;
using System.Linq;
using Infra;
using Infra.Model;
using Infra.Model.Data;
using Infra.Model.Game;
using Infra.Model.Resource;
using Module.Game.Battle;
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
            for (int i = 0, cnt = Enum.GetNames(typeof(JobType)).Length; i < cnt; i++)
            {
                var job = (JobType)i;
                var sprites = Resources.LoadAll<Sprite>($"Sprite/Blocks/{job.ToString()}Block").ToList();

                if (sprites != null)
                {
                    for (int skillIdx = 1; skillIdx <= sprites.Count; skillIdx++)
                    {
                        var key = (job, skillIdx);
                        BlockSpriteDict.Add(key, sprites[skillIdx - 1]);
                    }
                }
            }

            var jobSprites = Resources.LoadAll<Sprite>("Sprite/Job");
            Jobs = new List<Job>()
            {
                new ((int)JobType.Unknown, "테스트", 20, Color.black),
                new ((int)JobType.Warrior, "Warrior", 10, Color.blue, jobSprites[0]),
                new ((int)JobType.Wizard, "Wizard", 10, Color.magenta, jobSprites[1]),
                new ((int)JobType.Archer, "Archer", 10, Color.green, jobSprites[2]),
                new ((int)JobType.Priest, "Priest", 10, Color.yellow, jobSprites[4]),
                new ((int)JobType.Knight, "Knight", 10, Color.cyan, jobSprites[3]),
            };
            Dungeons = new List<Dungeon>()
            {
                new (0, "테스트 던전", 1, new (){ 0}, new List<int>(){ (int)EventType.HealingLake, (int)EventType.GetArtefact})
            };

            List<Reward> defaultRewards = new() { new Reward(RewardType.Gold, 100, 10) };
            Enemies = new List<Enemy>()
            {
                new("테스트 몬스터", 15, 2, Resources.Load<Sprite>("Sprite/enemy")){Rewards = defaultRewards}
            };

            
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

        public readonly Dictionary<Level, (int level, int bonus)> LevelInfo = new()
        {
            {Level.Lv1, (1, 0)},
            {Level.Lv2, (2, 120)},
            {Level.Lv3, (3, 250)},
            {Level.Lv4, (4, 390)},
            {Level.Lv5, (5, 600)},
            {Level.Lv6, (6, 750)},
            {Level.Lv7, (7, 800)},
        };

        public readonly (float battle, float elete, float @event, float rest) EventPercentage 
            = new (0.6f, 0.0f, 0.4f, 0.0f);

        public Dictionary<(JobType job, int index), Sprite> BlockSpriteDict { get; } = new();
        
        public List<Sprite> MapSprites { get; }
        #endregion
    }
}

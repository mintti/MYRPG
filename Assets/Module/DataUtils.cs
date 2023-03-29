using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using Infra.Model.Data;
using UnityEngine;
using Infra.Model.Game;
using Infra.Model.Resource;
using Infra.Util.RandomMapMaker;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental;
using Dungeon = Infra.Model.Data.Dungeon;
using Resource = Module.ResourceManager;
using Unit = Infra.Model.Data.Unit;
using Block = Infra.Model.Game.Block;

namespace Module
{
    internal delegate void BlockEvents(Block block);
    
    internal static class DataUtils
    {
        private static ResourceManager Resource => ResourceManager.Instance;
        public static PlayerData CreateNewData(List<int> selectedJobList)
        {
            List<Dungeon> dungeons = new();
            List<Unit> units = new();

            selectedJobList.ForEach(index => units.Add(new Unit(index)));
            
            // 테스트 던전 정보 생성
            int[] arr = new[] {1, 2, 2, 3, 3, 3, 4, 4, 5};
            for (int i = 0; i < 1; i++) // 다회석으로 돌아가도록 설정필요
            {
                var nomineeDungeons = Resource.Dungeons.Where(d => d.Level == arr[i] && !dungeons.Select(x=> x.Index).Contains(d.Index) ).ToList();
                var dungeon = nomineeDungeons[Random.Range(0, nomineeDungeons.Count)];            
                dungeons.Add(new Dungeon(dungeon.Index));
            }

            dungeons.First().IsEnable = true;
            return new PlayerData(dungeons, units);
        }

        public static Spot GetRandomMap(Dungeon dungeonData)
        {
            RandomMapMaker map = new RandomMapMaker();
            var dungeon = Resource.Dungeons[dungeonData.Index];
            
            var size = Resource.MapSizeByDungeonLevel[dungeon.Level];
            var spotList = map.Generate(size.depth, size.width);  

            int sum = spotList.Count;
            
            // [TODO] 더 나은 이벤트 부여 방식 고안
            // [TODO] Spot에서 이벤트 어떤 방식으로 가질 지 생각. 다양한 이벤트를 어떻게 포함시킬 것인가?
            #region Event Setting
            // Set Spot event count
            var eventList = new List<SpotEventType>();
            var prtg = Resource.EventPercentage;
            InsertDungeonEvent(eventList, SpotEventType.Battle, (int) (sum * prtg.battle));
            //InsertDungeonEvent(eventList, DungeonEventType.Elete, (int) (sum * prtg.elete));
            InsertDungeonEvent(eventList, SpotEventType.Rest, (int) (sum * prtg.rest));
            InsertDungeonEvent(eventList, SpotEventType.Event, (int) (sum * prtg.@event));
            
            if((spotList.Count - 2) != eventList.Count)
            {
                int value = spotList.Count - 2 - eventList.Count;
                
                if (value > 0)
                    InsertDungeonEvent(eventList, SpotEventType.Battle, value);
                else
                {
                    while (value++ != 0)
                    {
                        eventList.RemoveAt(0);
                    }
                }
            }
                
            
            // Shuffle
            eventList = ShuffleList(eventList).ToList();
            eventList.Insert(0, SpotEventType.Battle);
            eventList.Add(SpotEventType.Boss);
            
            // Set Detail of event 
            int index = 0;
            var randomMonsterSeq = GetRandomList(dungeon.EnemyList, 1, 4);
            var randomEventSeq = (GetRandomList(dungeon.EventList, 1));
            foreach (var evt in eventList)
            {
                SpotEvent spotEvent;
                switch (evt)
                {
                    case SpotEventType.Battle:
                        spotEvent = new Battle(randomMonsterSeq.ToList());
                        break;
                    case SpotEventType.Rest:
                        spotEvent = null;
                        break;
                    case SpotEventType.Event:
                        spotEvent = new SpotEvent(randomEventSeq.First());
                        break;
                    default:
                        spotEvent = null;
                        break;
                }

                spotList[index++].Event = spotEvent;
            }
            #endregion
            
            return spotList.First();
        }

        private static void InsertDungeonEvent(List<SpotEventType> list, SpotEventType type, int count)
        {
            for (var i = 0; i < count; i++)
            {
                list.Add(type);
            }
        }

        private static IEnumerable<T> ShuffleList<T>(List<T> list)
        {
            var random = new System.Random();
            var output = new List<T>();
            
            // 한번 더 랜덤 섞기
            for (var i = list.Count; i > 0; i--)
            {
                var randomIdx = random.Next(0, i);
                output.Add(list[randomIdx]);
            }

            return output;
        }

        private static IEnumerable<int> GetRandomList(List<int> list, int count)
        {
            var random = new System.Random();
            var output = new List<int>();
            var length = list.Count;
            for (int i = 0; i < count; i++)
            {
                var index = random.Next(0, length);
                output.Add(list[index]);
            }

            return output;
        }
        
        private static IEnumerable<int> GetRandomList(List<int> list, int min, int max)
        {
            var random = new System.Random();
            var output = new List<int>();
            var length = list.Count;
            var count = random.Next(min, max + 1);
            
            for (int i = 0; i < count; i++)
            {
                var index = random.Next(0, length);
                output.Add(list[index]);
            }

            return output;
        }
    }
}
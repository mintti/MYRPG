using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using Infra.Model.Data;
using UnityEngine;
using Infra.Model.Game;
using Infra.Model.Resource;
using Infra.Util.RandomMapMaker;
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
            
            // [TODO] 랜덤으로 던전 생성 로직 작성 필요
            // 테스트 던전 정보 생성
            dungeons.Add(GetDungeonData(1, new ()));
            
            dungeons.First().IsEnable = true;
            return new PlayerData(dungeons, units);
        }

        private static Dungeon GetDungeonData(int level, List<int> addedDungeons)
        {
            RandomMapMaker map = new RandomMapMaker();
            
            
            int maxDepth = 0;
            int maxWidth = 0;
            
            // 던전 지정
            var nomineeDungeons = Resource.Dungeons.Where(d => d.Level == 1 && !addedDungeons.Contains(d.Index) ).ToList();
            var dungeon = nomineeDungeons[Random.Range(0, nomineeDungeons.Count)];

            var size = Resource.MapSizeByDungeonLevel[dungeon.Level];
            Spot firstSpot = map.Generate(size.depth, size.width);  

            int sum = 0;
            int count;
            
            // [TODO] 더 나은 이벤트 부여 방식 고안
            // [TODO] Spot에서 이벤트 어떤 방식으로 가질 지 생각. 다양한 이벤트를 어떻게 포함시킬 것인가?
            #region Event Setting
            // Set Spot event count
            var eventList = new List<DungeonEventType>();
            var type = DungeonEventType.Battle;
            count = sum / 2;
            InsertDungeonEvent(eventList, type, count);

            type = DungeonEventType.Rest;
            if (sum <= 5)
            {
                eventList.Add(type);
                sum -= count + 1;
            }
            else
            {
                InsertDungeonEvent(eventList, type, 2);
                sum -= count + 2;
            }

            type = DungeonEventType.Event;
            InsertDungeonEvent(eventList, type, sum);

            // Shuffle
            eventList = ShuffleList(eventList).ToList();
            eventList.Insert(0, DungeonEventType.Battle);
            eventList.Add(DungeonEventType.Boss);
            
            // Set Detail of event 
            foreach (var evt in eventList)
            {
                 // [TODO] 구조 설정 필요
            }
            #endregion
            
            
            return new Dungeon(dungeon.Index, firstSpot);
        }

        private static void InsertDungeonEvent(List<DungeonEventType> list, DungeonEventType type, int count)
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
    }
}
using System.Collections.Generic;

namespace Infra.Model.Resource
{
    internal class Dungeon
    {
        public int Index { get; }
        public string Name { get; }
        
        public int Level { get; }
        public List<int> EnemyList { get; }
        public List<int> EventList { get; }
        
        public Dungeon(int index, string name, int level, List<int> enemy = null, List<int> @event = null)
        {
            Index = index;
            Name = name;
            Level = level;
            EnemyList = enemy ?? new List<int>();
            EventList = @event ?? new List<int>();
        }
    }
}
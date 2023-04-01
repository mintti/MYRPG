using System.Collections.Generic;

namespace Infra.Model.Data
{
    internal class BattleEvent : SpotEvent
    {
        public List<int> Monster { get; }

        public BattleEvent(List<int> monster)
        {
            Monster = monster;
        }
    }
}
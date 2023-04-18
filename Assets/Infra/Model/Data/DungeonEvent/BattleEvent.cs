using System.Collections.Generic;
using Infra.Model.Resource;

namespace Infra.Model.Data
{
    internal class BattleEvent : SpotEvent
    {
        public List<int> Enemies { get; }

        public BattleEvent(BattleType type, List<int> enemies) : base((int)type)
        {
            Enemies = enemies;
        }
    }
}
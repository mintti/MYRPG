using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infra.Model.Resource;

namespace Infra.Model.Data
{
    internal class BattleEvent : SpotEvent
    {
        public List<int> Enemies { get; }

        public BattleEvent(IEnumerable<int> enemies)
        {
            Enemies = enemies.ToList();
        }
    }
}
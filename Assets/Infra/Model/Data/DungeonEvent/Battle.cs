using System.Collections.Generic;

namespace Infra.Model.Data
{
    internal class Battle : SpotEvent
    {
        public List<int> Monster { get; }

        public Battle(List<int> monster)
        {
            Monster = monster;
        }
    }
}
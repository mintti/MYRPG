using Infra.Model.Game.Class;
using Module.Game;
using UnityEngine;

namespace Infra.Model.Game.Blocks
{
    internal class WarriorBlock01 : Block
    {
        public override void Execute()
        {
            if (ConnectedEntity is Warrior warrior)
            {
                warrior.SlashGauge += (int)(100f * Bonus);
            }
        }

    }
}
using Infra.Model.Game.Class;

namespace Infra.Model.Game.Blocks
{
    internal class PriestBlock01 : Block
    {
        public override void Execute()
        {
            if (ConnectedEntity is Priest priest)
            {
                priest.HealGauge += (int) (100f * Bonus);
            }
        }
    }
}
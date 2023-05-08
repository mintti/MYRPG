using Infra.Model.Game.Class;

namespace Infra.Model.Game.Blocks
{
    internal class ArcherBlock01 : Block
    {
        public override void Execute()
        {
            if (ConnectedEntity is Archer archer)
            {
                archer.ShotGauge += (int)(100f * Bonus);
            }
        }
    }
}
using Infra.Model.Game.Class;

namespace Infra.Model.Game.Blocks
{
    internal class KnightBlock01 : Block
    {
        public override void Execute()
        {
            if (ConnectedEntity is Knight knight)
            {
                knight.SlashGauge += (int)(100f * Bonus);
            }
        }
    }
}
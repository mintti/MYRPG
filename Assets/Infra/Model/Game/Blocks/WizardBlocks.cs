using Infra.Model.Game.Class;

namespace Infra.Model.Game.Blocks
{
    internal class WizardBlock01 : Block
    {
        public override void Execute()
        {
            if (ConnectedEntity is Wizard wizard)
            {
                wizard.MagicShowerGauge += (int)(100f * Bonus);
            }
        }
    }
}
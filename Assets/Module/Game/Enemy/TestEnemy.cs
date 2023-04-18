using Module.Game.Event;

namespace Module.Game
{
    internal class TestEnemy : Enemy
    {
        private IBattle IBattle { get; set; }
        public TestEnemy(HowToTarget htt)
        {
            HowToTarget = htt;
        }
        
        public override void Execute()
        {
            var target = GetTarget(IBattle.GetUnits());
            
            
        }
    }
}
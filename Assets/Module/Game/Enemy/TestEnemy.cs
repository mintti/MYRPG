using Module.Game.Battle;
using Module.Game.Event;
using UnityEngine;

namespace Module.Game
{
    internal class TestEnemy : Enemy
    {
        public TestEnemy(HowToTarget htt)
        {
            HowToTarget = htt;
        }
        
        public override void Execute()
        {
            var target = GetTarget(BattleController.GetUnits());

            if (target != null)
            {
                target.Hit(Power);
                Debug.Log($"{this.Name}이가 {target.Name}에게 Hit");
            }
            else 
                Debug.Log($"{Name}: 타겟이 존재하지 않음");
        }
    }
}
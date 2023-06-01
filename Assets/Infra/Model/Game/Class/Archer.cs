using Module.Game;
using Module.Game.Battle;

namespace Infra.Model.Game.Class
{
    internal class Archer : Unit
    {
        private int _shotGauge;
        public int ShotGauge
        {
            get => _shotGauge;
            set
            {
                CalledSkill(new ActionInfo(Shot, TargetType.Enemy));
                _shotGauge = value;
            }
        }
        
        private void Shot(BattleEntity entity)
        {
            entity.Hit(5 * ShotGauge);
        }
    }
}
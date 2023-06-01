using Module.Game;
using Module.Game.Battle;

namespace Infra.Model.Game.Class
{
    internal class Knight : Unit
    {
        private int _slashGauge;
        public int SlashGauge
        {
            get => _slashGauge;
            set
            {
                CalledSkill(new ActionInfo(Slash, TargetType.Enemy));
                _slashGauge = value;
            }
        }
        
        private void Slash(BattleEntity entity)
        {
            entity.Hit(5 * SlashGauge);
        }
    }
}
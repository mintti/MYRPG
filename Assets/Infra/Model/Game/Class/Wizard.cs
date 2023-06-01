using System.Collections.Generic;
using Module.Game;
using Module.Game.Battle;

namespace Infra.Model.Game.Class
{
    internal class Wizard : Unit
    {
        #region MagicMissile

        private int _magicShowerGauge;
        public int MagicShowerGauge
        {
            get => _magicShowerGauge;
            set
            {
                CalledSkill(new ActionInfo(MagicShower, TargetType.AllEnemy));
                _magicShowerGauge = value;
            }
        }
        
        private void MagicShower(BattleEntity entity)
        {
            entity.Hit(5 * MagicShowerGauge);
        }
        #endregion
    }
}
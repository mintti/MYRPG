﻿using System.Collections.Generic;
using Module.Game;
using Module.Game.Battle;

namespace Infra.Model.Game.Class
{
    internal class Priest : Unit
    {
        private int _healGauge;
        public int HealGauge
        {
            get => _healGauge;
            set
            {
                CalledSkill(new ActionInfo(Heal, TargetType.Unit));
                _healGauge = value;
            }
        }

        private void Heal(BattleEntity entity)
        {
            entity.Hit(5 * HealGauge);
        }
    }
}
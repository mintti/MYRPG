using System;
using System.Collections.Generic;
using System.ComponentModel;
using Module.Game;
using Module.Game.Battle;

namespace Infra.Model.Game.Class
{
    /// <summary>
    /// 해당 객체를 Skill Factory 를 하나 생성하여 일괄적으로 호출할 수 있지 않을까?
    /// => 한번에 관리 할 수 있어서 용이함.
    /// </summary>
    internal class Warrior : Unit
    {
        #region Slash

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
            entity.Hit(((int)(5f * SlashGauge * 0.01f)));
        }
        #endregion
    }
}
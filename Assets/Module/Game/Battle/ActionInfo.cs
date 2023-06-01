using System;
using System.Collections;
using System.Collections.Generic;

namespace Module.Game.Battle
{
    internal class ActionInfo
    {
        public TargetType TargetType { get; set; }
        public string SkillName { get; set; }
        public Action<BattleEntity> Action { get;  }

        public ActionInfo(Action<BattleEntity> action, TargetType targetType)
        {
            SkillName = action.Method.Name;
            TargetType = targetType;
            Action = action;
        }
    }
}
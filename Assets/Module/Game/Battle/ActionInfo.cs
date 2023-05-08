using System;
using System.Collections;
using System.Collections.Generic;

namespace Module.Game.Battle
{
    internal class ActionInfo
    {
        public TargetType TargetType { get; set; }
        public string SkillName { get; set; }
        public Action<IEnumerable<BattleEntity>> Actions { get;  }
        public Action<BattleEntity> Action { get;  }

        public ActionInfo(Action<IEnumerable<BattleEntity>> actions, TargetType targetType)
        {
            SkillName = actions.Method.Name;
            TargetType = targetType;
            Actions = actions;
        }
        public ActionInfo(Action<BattleEntity> action, TargetType targetType)
        {
            SkillName = action.Method.Name;
            TargetType = targetType;
            Action = action;
        }
    }
}
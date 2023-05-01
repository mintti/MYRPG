using System.Collections.Generic;

namespace Module.Game.Battle
{
    internal interface IBattleController
    {
        IEnumerable<BattleEntity> GetUnits();
    }
}
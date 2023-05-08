using System.Collections.Generic;

namespace Module.Game.Battle
{
    internal interface IBattleController
    {
        /// <summary>
        /// Enemy가 Unit 객체를 얻어가기 위해.. 
        /// </summary>
        /// <returns></returns>
        IEnumerable<BattleEntity> GetUnits();
        
        UIActionSelector UIActionSelector { get; }
        
    }
}
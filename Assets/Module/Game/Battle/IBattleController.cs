using System.Collections.Generic;
using Infra.Model.Game;

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

        /// <summary>
        /// Entity 객체가 죽었을 때 알림
        /// </summary>
        void UpdateEntityState();
        
        /// <summary>
        /// Entity 객체가 죽었을 때 알림
        /// </summary>
        void UpdateEntityState(Unit unit);
    }
}
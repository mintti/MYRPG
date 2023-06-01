using System;
using System.Collections;
using System.Collections.Generic;
using Infra.Model.Game;
using Infra.Model.Resource;
using Module.Game.Event.Message;

namespace Module.Game
{
    internal interface IEventController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nextAction">Spin Result</param>
        void SpinEvent(Action<Block[,]> nextAction = null);

        void Reward(IEnumerable<Reward> rewards, Action confirmAction = null);
        
        void EndEvent(bool isClear = true);
        
        IMessageBox MessageBox { get; }

        void ExecuteActionSelector(Action nextAction, Func<bool> checkFunc = null);
        
        IEnumerable<Unit> UnitList { get; }
    }
}
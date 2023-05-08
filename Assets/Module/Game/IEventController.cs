using System;
using Infra.Model.Game;
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

        void Reward(Reward reward);
        
        void EndEvent(string none = null);
        
        IMessageBox MessageBox { get; }

        void ExecuteActionSelector(Action nextAction, Func<bool> checkFunc = null);
    }
}
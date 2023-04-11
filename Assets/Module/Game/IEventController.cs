using System;
using Infra.Model.Game;
using Module.Game.Event.Message;

namespace Module.Game
{
    internal interface IEventController
    {
        void SpinEvent(Action nextAction = null);

        void Reward(Reward reward);
        
        void EndEvent(string none = null);
        
        IMessageBox MessageBox { get; }
    }
}
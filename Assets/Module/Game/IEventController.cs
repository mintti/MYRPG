using System;
using Module.Game.Event.Message;

namespace Module.Game
{
    internal interface IEventController
    {
        void SetView();

        void SpinEvent();
        
        void Reward();
        
        void StartEvent();
        
        void EndEvent();
        
        IMessageBox IMessageBox { get; }
        
    }
}
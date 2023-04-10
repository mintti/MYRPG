namespace Module.Game.Event
{
    internal interface IEventItem
    {
        IEventController ICon { get; }
        void Execute();

        // void SpinEvent();
        //
        // void SpinResult();
        //
        // void MsgConfirm();
    }
}
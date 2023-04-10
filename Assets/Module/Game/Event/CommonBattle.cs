namespace Module.Game.Event
{
    internal class CommonBattle : IEventItem
    {
        public CommonBattle(IEventController controller)
        {
            ICon = controller;
        }
        public IEventController ICon { get; }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }

        public void SpinEvent()
        {
            throw new System.NotImplementedException();
        }

        public void SpinResult()
        {
            throw new System.NotImplementedException();
        }

        public void MsgConfirm()
        {
            throw new System.NotImplementedException();
        }
    }
}
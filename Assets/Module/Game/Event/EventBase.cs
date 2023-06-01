namespace Module.Game.Event
{
    internal abstract class EventBase : IEventItem
    {
        protected IEventController EC { get; set; }

        protected EventBase(IEventController ec)
        {
            EC = ec;
        }
        
        public void UpdateUIGame(UIGame uiGame)
        {
            EC = uiGame;
        }
        public virtual void Execute()
        {
            
        }

        protected virtual void End()
        {
            EC.EndEvent();
        }
    }
}
namespace Module.Game.Event
{
    internal abstract class EventBase : IEventItem
    {
        protected IEventController EC { get; }

        protected EventBase(IEventController ec)
        {
            EC = ec;
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
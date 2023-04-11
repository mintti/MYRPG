namespace Module.Game.Event
{
    internal class GetArtifact : IEventItem
    {
        public GetArtifact(IEventController controller)
        {
            ICon = controller;
        }

        public IEventController ICon { get; }

        public void Execute()
        {
            
        }
    }
}
namespace Module.Game.Event
{
    internal class GetArtefact : EventBase, IEventItem
    {
        public GetArtefact(IEventController ec) : base(ec)
        {
        }

        public override void Execute()
        {
            EC.MessageBox.SetMessageBox("Get Artefact!", callback:End);
        }
    }
}
namespace Module.Game.Event
{
    internal class GetArtefact : EventBase, IEventItem
    {
        public GetArtefact(UIGame uiGame) : base(uiGame)
        {

        }

        public override void Execute()
        {
            EC.MessageBox.SetMessageBox("Get Artefact!", callback:End);
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace Module.Game.Event
{
    internal class HealingLake : IEventItem
    {
        public HealingLake(IEventController con)
        {
            ICon = con;
        }
        
        public Dictionary<string, string> Distractor { get; set; }
        public IEventController ICon { get; }

        public void Execute()
        {
            ICon.IMessageBox.SetMessageBox("find healing lake.", Distractor.Keys.ToArray(), ReceiveAnswer );
        }

        private void ReceiveAnswer(string answer)
        {
            ICon.IMessageBox.SetMessageBox(Distractor[answer]);
        }
        
    }
}
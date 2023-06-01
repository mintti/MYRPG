using System.Collections.Generic;
using System.Linq;

namespace Module.Game.Event
{
    internal class HealingLake : EventBase, IEventItem
    {
        public HealingLake(UIGame uiGame) : base(uiGame)
        {
            
        }
        
        public Dictionary<string, string> Distractor { get; set; }

        public override void Execute()
        {
            // [PROTO] 이벤트 텍스트 넣는 더 좋은 로직 구현 필요
            Distractor = new Dictionary<string, string>()
            {
                {"Drink spring water.", "recovery the half of hp."}
            };
            
            EC.MessageBox.SetMessageBox("find healing lake.", Distractor.Keys.ToArray(), ReceiveAnswer );
        }

        private void ReceiveAnswer(string answer)
        {
            switch (Distractor.Keys.ToList().IndexOf(answer))
            {
                case 1 :
                    foreach (var unit in EC.UnitList)
                    {
                        unit.Heal((int)(unit.MaxHp * 0.5f));
                    }
                    break;
            }
            
            EC.MessageBox.SetMessageBox(Distractor[answer], callback: End);
        }
    }
}
using System.Collections.Generic;
using Module;
using Module.Game;
using Module.Game.Battle;

namespace Infra.Model.Game
{
    internal class Unit : BattleEntity
    {
        #region Variables
        public int JobIndex { get; private set;}
        public List<Block> HasBlocks { get; set; } = new();
        #endregion

        public void SetBaseData(Data.Unit data)
        {
            JobIndex = data.JobIndex;

            foreach (var info in data.HasBlocks)
            {
                var block = Factory.GetBlock((JobIndex, info.Index, info.Level));
                block.ConnectCaster(this);
                HasBlocks.Add(block);
            }

            // 리소스 로드
            var jobBase = ResourceManager.Instance.Jobs[JobIndex];
            Name = jobBase.Name;
            MaxHp = jobBase.Hp;
            Hp = data.Hp;
            State = (data.Hp > 0) ? State.Alive : State.Die;
            Sprite = jobBase.Icon;
        }
        
        public void SendAction(Block block)
        {
            BattleController.UIActionSelector.AddAction(block);
        }

        protected override void Dead()
        {
            BattleController.UpdateEntityState(this);
            base.Dead();
        }
    }
}
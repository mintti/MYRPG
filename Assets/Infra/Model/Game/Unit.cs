using System.Collections.Generic;
using Module;
using Module.Game;
using Module.Game.Battle;

namespace Infra.Model.Game
{
    internal class Unit : BattleEntity, IBattleEntity
    {
        #region Variables
        public int JobIndex { get; }
        public List<Block> HasBlocks { get; private set; } = new();
        #endregion

        public Unit(Data.Unit data)
        {
            JobIndex = data.JobIndex;
            
            foreach (var info in data.HasBlocks)
            {
                var block = Factory.GetBlock((JobIndex, info.Index, info.Level));
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

        public void AddBlock(Block block) => HasBlocks.Add(block);
        
        public void RemoveBlock(Block block) => HasBlocks.Remove(block);

    }
}
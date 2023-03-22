using System.Collections.Generic;
using Module;

namespace Infra.Model.Game
{
    internal class Unit
    {
        public int JobIndex { get; }

        public List<Block> HasBlocks { get; private set; } = new();
        
        public int Hp { get; set; }

        public Unit(Data.Unit data)
        {
            JobIndex = data.JobIndex;

            foreach (var info in data.HasBlocks)
            {
                var block = Factory.GetBlock((JobIndex, info.Index, info.Level));
                HasBlocks.Add(block);
            }
            
        }

        public void AddBlock(Block block) => HasBlocks.Add(block);
        
        public void RemoveBlock(Block block) => HasBlocks.Remove(block);

    }
}
using System.Collections.Generic;
using Infra.Model.Game;

namespace Module
{
    /// <summary>
    /// Factory that create object of every kind that is needed to game.
    /// </summary>
    internal class Factory
    {
        #region Game
        #region Block
        private static Dictionary<(int job, int index, int level), Block> BlockBuffer { get; set; } =
            new();
        public static Block GetBlock((int job, int index, int level) key)
        {
            Block block;
            if (BlockBuffer.ContainsKey(key)) block = (Block)BlockBuffer[key].Clone();
            else
            {
                block = new();
            }
            return block;
        }
        #endregion

        #region Event

        private static Dictionary<int, DungeonEvent> DungeonEventBuffer { get; set; } =
            new();
        public static DungeonEvent DungeonEventFactory(int key)
        {
            DungeonEvent de;

            // [TODO} Clone으로 할 것 인가?
            if (DungeonEventBuffer.ContainsKey(key)) de = (DungeonEvent)DungeonEventBuffer[key].Clone();
            else
            {
                de = new();
            }

            return de;
        }

        

        #endregion

        #endregion
        
        
        
    }
}
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
    }
}
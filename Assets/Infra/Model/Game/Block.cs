using UnityEngine;

namespace Infra.Model.Game
{
    internal class Block
    {
        #region Info
        public string Name { get; set; }
        public Color Color { get; set; }
        public Sprite Sprite { get; set; }
        #endregion
        
        #region Game
        public float Weight { get; set; }
        public float WeightBackup { get; set; }
        #endregion
    }
}
using System;
using UnityEngine;

namespace Infra.Model.Game
{
    internal class Block : ICloneable
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
        
        public Block(){}

        public Block(string name, Color color, Sprite sprite = null)
        {
            Name = name;
            Color = color;
            Sprite = sprite;
        }
        public object Clone()
        {
            return new Block(Name, Color, Sprite);
        }
    }
}
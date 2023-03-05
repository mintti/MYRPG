using System;
using System.Collections.Generic;
using System.Linq;
using Infra.Model.Data;
using Module.WorldMap;
using UnityEngine;
using UnityEngine.UI;

namespace Module.Game.Slot
{
    public class Block
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
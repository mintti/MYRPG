﻿using System.Collections.Generic;
using Infra.Model.Game;
using Module;
using UnityEngine;

namespace Infra.Model.Resource
{
    internal class Job
    {
        public int Index { get; }
        public string Name { get; }

        public UnityEngine.Color Color { get; }
        
        public Sprite Icon { get; }
        
        public int Hp { get; }
        
        public Job(int index, string name, int hp, Color color, Sprite icon= null)
        {
            Index = index;
            Name = name;
            Hp = hp;
            Color = color;
            Icon = icon;
        }
    }
}
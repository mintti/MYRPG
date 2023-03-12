using UnityEngine;
using Color = System.Drawing.Color;

namespace Infra.Model.Resource
{
    internal class Job
    {
        public int Index { get; }
        public string Name { get; }

        public Color Color { get; }
        
        public Sprite Icon { get; }
        
        public Job(int index, string name, Color color, Sprite icon= null)
        {
            Index = index;
            Name = name;
            Color = color;
            Icon = icon;
        }
    }
}
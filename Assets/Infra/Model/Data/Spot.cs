using System;
using System.Collections.Generic;

namespace Infra.Model.Data
{
    internal enum SpotState
    {
        None,
        Clear,
        Do,
    }
    internal class Spot
    {
        public int Index { get; protected set; }
        public SpotState State { get; set; } 
        
        public List<Spot> ChildSpots { get; set; }
       
        public SpotEvent Event { get; set; }
        
        public Spot(int index)
        {
            Index = index;
        }
        
    
        public void Connect(Spot child)
        {
            ChildSpots ??= new List<Spot>();
            ChildSpots.Add(child);
        }
    }
}
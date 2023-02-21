using System.Collections.Generic;

namespace Infra.Model.Data
{
    /// <summary>
    /// Dungeon info that saved player data.
    /// Dungeon's Index, component of event when user data created.
    /// </summary>
    public class Dungeon
    {
        public int Index { get;}
        
        public List<MapPoint> MapPoints { get; }
            
        /// <summary>
        /// Rate of Progress
        /// </summary>
        public int ProgressRate { get; set; }

        
        public Dungeon(int index, List<MapPoint> mapPoints )
        {
            Index = index;
            MapPoints = mapPoints;
        }
    }
}
using System.Collections.Generic;

namespace Infra.Model.Data
{
    /// <summary>
    /// Dungeon info that saved player data.
    /// Dungeon's Index, component of event when user data created.
    /// </summary>
    public class Dungeon
    {
        #region Variables
        public int Index { get;}
        
        public bool IsEnable { get; set; }
        public bool IsClear { get; set; }
        public List<MapPoint> MapPoints { get; }
            
        /// <summary>
        /// Rate of Progress
        /// </summary>
        public int ProgressRate { get; set; }
        #endregion

        
        /// <summary>
        /// 초기화
        /// </summary>
        public Dungeon(int index, List<MapPoint> mapPoints )
        {
            Index = index;
            MapPoints = mapPoints;
            IsClear = false;
        }
    }
}
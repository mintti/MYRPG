using System.Collections.Generic;
using Infra.Model.Data;

namespace Infra.Model.Game
{
    internal class GameData
    {
        public List<Dungeon> DungeonList { get; set; }
        
        public List<Unit> UnitList { get; set; }
        
        public List<Artefact> ArtefactList { get; set; }

        public uint Money { get; set; }


        #region Game
        public int SlotWidth { get; set; }

        public int SlotHeight { get; set; }


        #endregion

        public GameData()
        {
            
        }

        public GameData(PlayerData data) : base()
        {
            
        }
    }
}
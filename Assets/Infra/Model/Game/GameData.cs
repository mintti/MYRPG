using System.Collections.Generic;
using Infra.Model.Data;

namespace Infra.Model.Game
{
    internal class GameData
    {
        public List<Dungeon> DungeonList { get; set; } // 수정 필요
        
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

        public GameData(PlayerData data)
        {
            DungeonList = new List<Dungeon>(data.DungeonList);

            UnitList ??= new ();
            foreach (var unit in data.UnitList)
            {
                UnitList.Add( new Unit(unit));
            }

            ArtefactList = new();

        }
    }
}
﻿using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;
using Infra.Model.Data;
using Module;

namespace Infra.Model.Game
{
    internal class GameData
    {
        private PlayerData PlayerData { get; }

        #region PlayerData Navigate

        public List<Dungeon> DungeonList => PlayerData.DungeonList;

        public int DungeonIndex
        {
            get => PlayerData.DungeonIndex;
            set => PlayerData.DungeonIndex = value;
        }
        public Spot Map
        {
            get => PlayerData.Spot;
            set => PlayerData.Spot = value;
        }
        #endregion

        public List<Unit> UnitList { get; } = new ();

        public List<Artefact> ArtefactList { get; } = new();

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
            PlayerData = data;

            foreach (var unit in PlayerData.UnitList)
            {
                var unitInstance = Factory.GetUnit(unit.JobIndex);
                unitInstance.SetBaseData(unit);
                UnitList.Add(unitInstance);
            }

            // 임시 초기 값
            SlotWidth = 5;
            SlotHeight = 2;
        }
    }
}
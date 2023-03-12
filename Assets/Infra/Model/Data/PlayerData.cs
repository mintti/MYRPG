using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Infra.Model.Data
{
    /// <summary>
    /// 저장용 데이터 구조
    /// - 해당 데이타를 가공하여 게임 데이타를 생성
    /// - 가공된 데이타를 해당 데이타 구조로 저장
    /// </summary>
    internal class PlayerData
    {
        public List<Dungeon> DungeonList { get; set; }
        
        public List<Unit> UnitList { get; set; }
        
        public List<Artefact> ArtefactList { get; set; }

        public uint Money { get; set; }

        public PlayerData(List<Dungeon> dungeonList,
                          List<Unit> unitList)
        {
            DungeonList = dungeonList;
            UnitList = unitList;
            ArtefactList = new ();
        }
    }
}
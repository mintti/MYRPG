using System.Collections.Generic;
using System.Linq;
using Infra.Model.Data;
using Infra.Model.Game;
using Dungeon = Infra.Model.Data.Dungeon;
using Resource = Module.ResourceManager;
using Unit = Infra.Model.Data.Unit;
using Block = Infra.Model.Game.Block;

namespace Module
{
    
    internal delegate void BlockEvents(Block block);
    
    internal static class DataUtils
    {
        public static PlayerData CreateNewData(List<int> selectedJobList)
        {
            List<Dungeon> dungeons = new();
            List<Unit> units = new();

            selectedJobList.ForEach(index => units.Add(new Unit(index)));
            
            // [TODO] 던전 정보 생성 
            dungeons.First().IsEnable = true;
            return new PlayerData(dungeons, units);
        }
    }
}
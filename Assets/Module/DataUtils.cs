using System.Collections.Generic;
using System.Linq;
using Infra.Model.Data;
using Resource = Module.ResourceManager;
namespace Module
{
    public static class DataUtils
    {
        public static PlayerData CreateNewData(List<int> selectedJobList)
        {
            List<Dungeon> dungeons = new();
            List<Unit> units = new();

            selectedJobList.ForEach(index => units.Add(new Unit(index)));
            
            return new PlayerData(dungeons, units);
        }
    }
}
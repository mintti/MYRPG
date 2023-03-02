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
            
            // [TODO] 던전 정보 생성 
            dungeons.First().IsEnable = true;
            return new PlayerData(dungeons, units);
        }
    }
}
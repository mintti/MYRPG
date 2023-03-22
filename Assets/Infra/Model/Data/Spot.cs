using System.Collections.Generic;
using Infra.Util.RandomMapMaker;

namespace Infra.Model.Data
{
    public class Spot
    {
        public int Index { get; protected set; }
        
        public List<Spot> ChildSpots { get; set; }
    
        // [TODO] 이벤트 설정 방식 생각하기
        public int EventIndex { get; set; }
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
namespace Infra.Model.Data
{
    public class MapPoint
    {
        public int Depth { get; }
        public int EventIndex { get; }
        
        public MapPoint(int depth, int eventIdx )
        {
            Depth = depth;
            EventIndex = eventIdx;
        }
    }
}
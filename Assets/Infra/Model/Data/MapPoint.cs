namespace Infra.Model.Data
{
    internal class MapPoint
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
using Infra.Model.Resource;

namespace Infra.Model.Data
{
    internal enum SpotEventType
    {
        None, 
        Battle,
        Elete,
        Boss,
        Rest,
        Artefact,
        Event
    }
    
    internal class SpotEvent
    {
        public SpotEventType Type { get; set; }
        
        public int Index { get;}

        public SpotEvent()
        {
            
        }

        public SpotEvent(int index)
        {
            Index = index;
        }
    }
}
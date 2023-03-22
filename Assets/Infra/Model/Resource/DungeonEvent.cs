namespace Infra.Model.Resource
{
    internal enum DungeonEventType
    {
        None, 
        Battle,
        Boss,
        Rest,
        Artefact,
        Event
    }
    
    internal class DungeonEvent
    {
        public int Index { get; }
        public string Name { get; }

        public DungeonEventType Type { get; set; }

        public DungeonEvent(int index, string name, DungeonEventType type)
        {
            Index = index;
            Name = name;
            Type = type;
        }
    }
}
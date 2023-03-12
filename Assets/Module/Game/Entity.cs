using Infra.Model.Game;

namespace Module.Game
{
    internal class Entity
    {
        public uint Hp { get; set; }
        public uint MaxHp { get; set; }
        public State State { get; set; }
        
    }
}
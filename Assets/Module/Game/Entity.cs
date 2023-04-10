using Infra.Model.Game;
using UnityEditor.Sprites;

namespace Module.Game
{
    internal class Entity
    {
        public uint Hp { get; set; }
        public uint MaxHp { get; set; }
        public State State { get; set; }

        public virtual void Execute()
        {
        }

        public bool CanAction()
        {
            return State == State.Alive;
        }
    }
}
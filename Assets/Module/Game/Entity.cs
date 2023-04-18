using Infra.Model.Game;
using UnityEditor.Sprites;
using UnityEngine;

namespace Module.Game
{
    internal class Entity
    {
        public uint Hp { get; set; }
        public uint MaxHp { get; set; }
        public State State { get; set; }
        
        public Color TempColor { get; set; }
        
        public virtual void Execute()
        {
        }

        public bool CanAction()
        {
            return State == State.Alive;
        }
    }
}
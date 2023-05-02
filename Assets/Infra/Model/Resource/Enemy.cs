using UnityEngine;

namespace Infra.Model.Resource
{
    internal class Enemy
    {
        public string Name { get; }
        public int  Hp { get; }
        public int  Power { get; }
        
        public Sprite Sprite { get; }
        public Enemy(string name, int hp, int power, Sprite sprite = null)
        {
            Name = name;
            Hp = hp;
            Power = power;

            Sprite = sprite;
        }
    }
}
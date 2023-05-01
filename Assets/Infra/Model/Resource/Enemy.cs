namespace Infra.Model.Resource
{
    internal class Enemy
    {
        public string Name { get; }
        public int  Hp { get; }
        public int  Power { get; }
        
        public Enemy(string name, int hp, int power)
        {
            Name = name;
            Hp = hp;
            Power = power;
        }
    }
}
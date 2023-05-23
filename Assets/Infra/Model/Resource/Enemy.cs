using System.Collections.Generic;
using UnityEngine;

namespace Infra.Model.Resource
{
    internal class Enemy
    {
        public string Name { get; }
        public int  Hp { get; }
        public int  Power { get; }
        
        public Sprite Sprite { get; }
        
        /// <summary>
        /// 제공 가능한 보상
        /// </summary>
        public List<Reward> Rewards { get; set; }

        public int RewardCount { get; set; } = 1;
        
        public Enemy(string name, int hp, int power, Sprite sprite = null)
        {
            Name = name;
            Hp = hp;
            Power = power;
            Sprite = sprite;
        }

        public List<Reward> GetReward()
        {
            var random = new System.Random();
            List<Reward> result = new ();

            for (int i = 0, cnt = random.Next(1, RewardCount + 1); i < cnt; i++)
            {
                var value = random.NextDouble() * 100 % 100;
                
                float aggregator = 0;
                for (int index = 0; value <= 100; index++)
                {
                    var reward = Rewards[index];
                    aggregator += reward.Probability;
                    if (value <= aggregator)
                    {
                        result.Add(reward);
                        break;
                    }
                }
            }

            return result;
        }
    }
}
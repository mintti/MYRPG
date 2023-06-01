using UnityEngine.UIElements.Experimental;

namespace Infra.Model.Resource
{
    public class Reward
    {
        public RewardType Type { get; set; }
        public float Probability { get; set; }
        public int Value { get; set; }

        public Reward(RewardType type, float probab, int value)
        {
            Type = type;
            Probability = probab;
            Value = value;
        }
    }
    
    public enum RewardType
    {
        None,
        Gold,
        Item,
        Artefact
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Module.Game
{
    internal enum HowToTarget
    {
        None,
        First,
        LowHp,
        LeastDefence,
        CalculMaxDam,
    }
    
    
    /// <summary>
    /// 전투 시 호출되는 적 객체
    /// </summary>
    internal abstract class Enemy : Entity
    {
        public HowToTarget HowToTarget { get; set; }
        
        public override void Execute()
        {
            Debug.Log($"{this.GetType().Name} Execute() 미구현 ");
        }

        public Entity GetTarget(IEnumerable<Entity> entities)
        {
            return entities.First();
        }
    }
}
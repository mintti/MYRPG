﻿using System;
using System.Collections.Generic;
using System.Linq;
using Infra.Model.Game;
using Infra.Model.Resource;
using Module;
using Module.Game.Battle;
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
    internal abstract class Enemy : BattleEntity
    {
        public HowToTarget HowToTarget { get; set; }
        public Infra.Model.Resource.Enemy BaseEnemy { get; set; }
        
        public void Init(EnemyType e)
        {
            var enemy = ResourceManager.Instance.Enemies[(int) e];

            BaseEnemy = enemy;
            
            Name = enemy.Name;
            MaxHp = enemy.Hp;
            Hp = enemy.Hp;
            Power = enemy.Power;
            Sprite = enemy.Sprite;
            State = State.Alive;
        }

        public override void Execute()
        {
            Debug.Log($"{this.GetType().Name} Execute() 미구현 ");
        }

        public BattleEntity GetTarget(IEnumerable<BattleEntity> targets)
        {
            BattleEntity target = null;
            switch (HowToTarget)
            {
                case HowToTarget.First :
                    target = targets.FirstOrDefault();
                    break;
                default:
                    break;
            }

            return target;
        }
    }
}
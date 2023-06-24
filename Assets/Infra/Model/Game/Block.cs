using System;
using Infra.Model.Resource;
using Module;
using Module.Game;
using Module.Game.Battle;
using UnityEngine;
using UnityEngine.UIElements;

namespace Infra.Model.Game
{
    internal enum Level : int
    {
        Lv1 = 1,
        Lv2 = 2,
        Lv3 = 3,
        Lv4 = 4,
        Lv5 = 5,
        Lv6 = 6,
        Lv7 = 7,
    }

    internal class Block
    {
        #region Info
        public string Name { get; set; }

        /// <summary>
        /// 스킬 인덱스
        /// </summary>
        public int Index { get; private set; } = 1;
        public Color Color { get; private set;}
        public Sprite Sprite { get; private set;}
        
        public SkillType SkillType { get; }
        public TargetType TargetType { get; set; }
        public int DefaultDamage { get; private set; }
        #endregion
        
        #region Game
        protected Unit Caster { get; private set; }
        public float Weight { get; set; }
        public float WeightBackup { get; set; }
        #endregion

        public Block(int index, string name, int defDam, SkillType skillType, TargetType targetType, Color color, Sprite sprite = null)
        {
            Index = index;
            Name = name;
            DefaultDamage = defDam;
            SkillType = skillType;
            TargetType = targetType;
            Color = color;
            Sprite = sprite;
            Weight = 1;
            WeightBackup = Weight;
        }

        public void ConnectCaster(Unit caster)
        {
            Caster = caster;
        }

        protected float Bonus { get; set; }
        public virtual void Init()
        {
            Bonus = 1.0f;
        }
        
        /// <summary>
        /// 주변 블럭들에게 효과를 적용할 경우, 재정의 하여 사용
        /// </summary>
        public virtual void PreExecute(Block[,] block)
        {
            
        }

        /// <summary>
        /// 블럭 실행에 따라 Caster에게 부여할 이벤트가 존재할 경우, 재정의 하여 사용
        /// </summary>
        public void Execute()
        {
            Caster.SendAction(this);
        }

        public void Action(BattleEntity target, Level level = default)
        {
            Caster.ActionAnimation(Name);

            int gauge = (int) (DefaultDamage * (ResourceManager.Instance.LevelInfo[level].bonus + 100) * 0.01f);
            
            switch(SkillType) 
            {
                case SkillType.Attack: target.Hit(gauge);
                    break;
                case SkillType.Heal:
                    target.Heal(gauge);
                    break;
                default: break;
            }
        }
    }
}
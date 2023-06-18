using System;
using Module.Game;
using UnityEngine;
using UnityEngine.UIElements;

namespace Infra.Model.Game
{
    internal class Block
    {
        #region Info
        public string Name { get; set; }

        /// <summary>
        /// 스킬 인덱스
        /// </summary>
        public int Index { get; set; } = 1;
        public Color Color { get; set; }
        public Sprite Sprite { get; set; }
        
        protected BattleEntity ConnectedEntity { get; set; }
        #endregion
        
        #region Game
        public float Weight { get; set; }
        public float WeightBackup { get; set; }
        #endregion
        
        public void Set(string name, Color color, Sprite sprite = null)
        {
            Name = name;
            Color = color;
            Sprite = sprite;
            Weight = 1;
            WeightBackup = Weight;
        }

        public void ConnectCaster(BattleEntity entity)
        {
            ConnectedEntity = entity;
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
        public virtual void Execute()
        {
            
        }

        // #region ICloneable
        // public virtual object Clone()
        // {
        //     var block = new Block();
        //     block.Set(Name, Color, Sprite);
        //     return block;
        // }
        // #endregion
    }
}
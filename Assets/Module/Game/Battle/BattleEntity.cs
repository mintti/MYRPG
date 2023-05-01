using Infra.Model.Game;
using Module.Game.Battle;
using UnityEditor.Sprites;
using UnityEngine;

namespace Module.Game
{
    /// <summary>
    /// 전투하기 위해 필요한 개체
    /// </summary>
    internal abstract class BattleEntity : IBattleEntity
    {
        protected IBattleController BattleController { get; set; }
        private UIEntity UIEntity { get; set; }

        #region Status
        public string Name { get; set; }

        private int _hp;
        public int Hp
        {
            get => _hp;
            set
            {
                _hp = value;
                if (_hp <= 0)
                {
                    _hp = 0;
                    State = State.Die;
                }
            }
        }
        
        public int MaxHp { get; set; }
        protected int Power { get; set; }

        public State State { get; set; }
        #endregion
        
        public Color TempColor { get; set; }

        public bool CanAction()
        {
            return State == State.Alive;
        }

        public void Connect(UIEntity uiEntity)
        {
            UIEntity = uiEntity;
        }

        #region This method in region define in derived class.
        public virtual void Execute()
        {
            
        }
        #endregion

        #region Action
        public void Hit(int damage)
        {
            Hp -= damage;
        }
        
        #endregion
    }
}
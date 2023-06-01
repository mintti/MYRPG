using System.Runtime.CompilerServices;
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

        #region Status Related
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
                    BattleController.UpdateEntityState();
                }
            }
        }
        
        public int MaxHp { get; set; }
        protected int Power { get; set; }

        public State State { get; set; }
        
        public bool CanAction()
        {
            return State == State.Alive;
        }
        #endregion

        #region IBattleEntity
        /// <summary>
        /// UI Entity 인스턴스와 연결
        /// </summary>
        public void Connect(IBattleController controller, UIEntity uiEntity)
        {
            BattleController = controller;
            UIEntity = uiEntity;
        }

        /// <summary>
        /// UI Entity 인스턴스와 연결 해제
        /// </summary>
        public void Disconnect()
        {
            BattleController = null;
            UIEntity = null;
        }
        public Sprite Sprite { get; set; }

        public BattleEntity GetEntity { get => this; }
        #endregion

        #region This method in region define in derived class.
        public virtual void Execute()
        {
            
        }
        #endregion

        #region Battle Action Related
        public void Hit(int damage)
        {
            Hp -= damage;
        }

        public void Heal(int point)
        {
            Hp += point;
        }
        #endregion
    }
}
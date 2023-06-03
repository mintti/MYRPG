using System;
using UnityEngine;
using UnityEngine.UI;

namespace Module.Game.Battle
{
    internal class UIEntity : BaseMonoBehaviour
    {
        private UIBattle UIBattle { get; set; }
        private IBattleEntity BattleEntity{ get; set; }

        public SpriteRenderer spriteRenderer;

        private Animator _animator;

        public void Init(UIBattle uiBattle)
        {
            UIBattle = uiBattle;
         
            _animator= GetComponent<Animator>();
            gameObject.SetActive(false);
        } 

        public void SetEntity(IBattleEntity entity)
        {
            BattleEntity = entity;
            BattleEntity.Connect(UIBattle, this);

            spriteRenderer.sprite = BattleEntity.Sprite;
            
            gameObject.SetActive(true);
            TargetMode(false);
            
        }

        public void Clear()
        {
            if (BattleEntity != null)
            {
                
                BattleEntity.Disconnect();
                BattleEntity = null;
                spriteRenderer.sprite = null;
            }
        }

        
        public void Animation(string name)
        {
            _animator.Play(name);
        }
        
        #region Target On Event

        public GameObject canBeTargetGameObject;

        public void TargetMode(bool state)
        {
            if (BattleEntity != null)
            {
                if (state && !BattleEntity.GetEntity.CanDesignateTarget()) return;
                
                canBeTargetGameObject.SetActive(state);
            }
            
        }
        
        private void OnMouseUp()
        {
            UIBattle.UIGame.SelectedUIEntityEvent(BattleEntity.GetEntity);
        }
        #endregion

        public void EndDeadAnm()
        {
            _animator.Rebind();
            _animator.Update(0f);
            canBeTargetGameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        
    }
}

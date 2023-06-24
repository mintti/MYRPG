using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Module.Game.Battle
{
    internal class UIEntity : BaseMonoBehaviour
    {
        private UIBattle UIBattle { get; set; }
        private BattleEntity BattleEntity{ get; set; }

        public UIEntityState UIEntityState { get; private set; }
        public SpriteRenderer spriteRenderer;

        private Animator _animator;

        public void Init(UIBattle uiBattle)
        {
            UIBattle = uiBattle;
         
            _animator= GetComponent<Animator>();
            gameObject.SetActive(false);
        } 

        public void SetEntity(BattleEntity entity, UIEntityState uiEttStt)
        {
            // ui entity state
            UIEntityState = uiEttStt;
            uiEttStt.Connect(this);
            UpdatePosition();
            
            // battle entity 
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

        private bool _doTarget;
        private bool DoTarget
        {
            get => _doTarget;
            set
            {
                _doTarget = value;
                canBeTargetGameObject.SetActive(value);
            }
        }
        public GameObject canBeTargetGameObject;

        public void TargetMode(bool state)
        {
            if (BattleEntity != null)
            {
                if (state && !BattleEntity.GetEntity.CanDesignateTarget()) return;
                DoTarget = state;
            }
            
        }
        
        private void OnMouseUp()
        {
            if(_doTarget)
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

        #region Camera
        public void UpdatePosition()
        {
            Vector3 pos = UIBattle.mainCamera.WorldToScreenPoint(transform.position);
            UIEntityState.UpdatePosition(pos);
        }
        

        #endregion
        
    }
}

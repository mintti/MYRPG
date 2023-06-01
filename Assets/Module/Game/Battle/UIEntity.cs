using System;
using UnityEngine;
using UnityEngine.UI;

namespace Module.Game.Battle
{
    internal class UIEntity : BaseMonoBehaviour
    {
        private UIBattle UIBattle { get; set; }
        private IBattleEntity BattleEntity{ get; set; }

        public GameObject testDeadMark;
        public SpriteRenderer spriteRenderer;        
        public void Init(UIBattle uiBattle)
        {
            UIBattle = uiBattle;
            gameObject.SetActive(false);
            TargetMode(false);
        } 

        public void SetEntity(IBattleEntity entity)
        {
            BattleEntity = entity;
            BattleEntity.Connect(UIBattle, this);
            spriteRenderer.sprite = BattleEntity.Sprite;
            gameObject.SetActive(true);
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

        #region Target On Event

        public GameObject canBeTargetGameObject;

        public void TargetMode(bool state)
        {
            canBeTargetGameObject.SetActive(state);
        }
        
        private void OnMouseUp()
        {
            UIBattle.UIGame.SelectedUIEntityEvent(BattleEntity.GetEntity);
        }
        #endregion
        
    }
}

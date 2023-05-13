using System;
using UnityEngine;

namespace Module.Game.Battle
{
    internal class UIEntity : MonoBehaviour
    {
        private UIBattle UIBattle { get; set; }
        private IBattleEntity BattleEntity{ get; set; }

        public GameObject testDeadMark;
        
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
            GetComponent<SpriteRenderer>().sprite = BattleEntity.Sprite;
            gameObject.SetActive(true);
        }

        public void Clear()
        {
            BattleEntity.Disconnect();
            BattleEntity = null;
            GetComponent<SpriteRenderer>().sprite = null;
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

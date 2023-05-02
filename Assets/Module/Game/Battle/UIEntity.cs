using System;
using UnityEngine;

namespace Module.Game.Battle
{
    internal class UIEntity : MonoBehaviour
    {
        private IBattleEntity BattleEntity{ get; set; }

        public GameObject testDeadMark;
        
        public void Init()
        {
            gameObject.SetActive(false);
        } 

        public void SetEntity(IBattleEntity entity)
        {
            GetComponent<SpriteRenderer>().sprite = entity.Sprite;
            BattleEntity = entity;
            gameObject.SetActive(true);
        }

        public void Clear()
        {
            BattleEntity = null;
            GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}

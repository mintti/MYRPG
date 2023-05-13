using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Module.Game.Battle
{
    internal class UIAction : MonoBehaviour
    {
        #region Variables
        private UIActionSelector UIActionSelector { get; set; }
        
        #region Exteranl

        public TextMeshProUGUI titleText;
        public void B_Click() => Execute();
        
        #endregion
        
        public bool Active { get; set; }
        private ActionInfo BaseInfo { get; set; }
        #endregion

        public void Init(UIActionSelector selector)
        {
            UIActionSelector = selector;
            Active = false;
        }

        private UIBattle UIBattle => UIActionSelector.UIGame.uIBattle;
        
        public void Set(ActionInfo info)
        {
            BaseInfo = info;
            titleText.text = BaseInfo.SkillName;

            Active = true;
            gameObject.SetActive(true);   
        }


        private void Clear()
        {
            BaseInfo = null;
            Active = false;
            gameObject.SetActive(false);
            UIActionSelector.ExecuteAction();
        }

        private void Execute()
        {
            TargetType type = BaseInfo.TargetType;
            switch (type)
            {
                case TargetType.Unit:
                case TargetType.Enemy:
                    // 대상 지정 요청
                    UIActionSelector.UIGame.SelectBattleEntity(type, Execute);
                    break;
                case TargetType.AllUnit:
                    Execute(UIBattle.UnitList);
                    Clear();
                    break;
                case TargetType.AllEnemy:
                    Execute(UIBattle.EnemyList);
                    break;
            }
        }

        private void Execute(IEnumerable<BattleEntity> entities)
        {
            foreach (var entity in entities)
            {
                BaseInfo.Action(entity);
            }
            Clear();
        }
        
        private void Execute(BattleEntity entity)
        {
            BaseInfo.Action(entity);
            Clear();
        }
    }
}

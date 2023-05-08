using System;
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
        }

        private void Execute()
        {
            TargetType type = BaseInfo.TargetType;
            switch (type)
            {
                case TargetType.Unit:
                case TargetType.Enemy:
                    var target = UIActionSelector.UIGame.SelectTarget(type);
                    BaseInfo.Action(target);
                    break;
                case TargetType.AllUnit:
                    BaseInfo.Actions(UIBattle.UnitList);
                    break;
                case TargetType.AllEnemy:
                    BaseInfo.Actions(UIBattle.EnemyList);
                    break;
            }

            Clear();
        }
    }
}

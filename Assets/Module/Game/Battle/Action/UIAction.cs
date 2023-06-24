using System;
using System.Collections;
using System.Collections.Generic;
using Infra.Model.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Module.Game.Battle
{
    internal class UIAction : BaseMonoBehaviour
    {
        #region Variables
        private UIActionSelector UIActionSelector { get; set; }
        
        #region Exteranl

        public TextMeshProUGUI titleText;
        public Image iconImage;
        public Transform actionLevelTr;
        public void B_Click() => Execute();
        
        #endregion
        
        public bool Active { get; private set; }
        private Block Block { get; set; }

        private int _level;
        #endregion

        public void Init(UIActionSelector selector)
        {
            UIActionSelector = selector;
            Active = false;
        }

        private UIBattle UIBattle => UIActionSelector.UIGame.uIBattle;
        
        public void Set(Block block)
        {
            // 블럭 정보 설정
            Block = block;
            titleText.text = Block.Name;
            iconImage.sprite = block.Sprite;

            // 강화 초기화
            _level = 1;
            for (int i = 0; i < actionLevelTr.childCount; i++)
            {
                actionLevelTr.GetChild(i).gameObject.SetActive(false);
            }
            
            // 활성화
            Active = true;
            gameObject.SetActive(true);   
        }
        
        public void Upgrade()
        {
            _level++;
            actionLevelTr.GetChild(_level-1).gameObject.SetActive(true);
        }

        public void Hide()
        {
            Block = null;
            Active = false;
            gameObject.SetActive(false);
        }
        
        private void Clear()
        {
            Hide();
            UIActionSelector.ExecuteAction();
        }

        private void Execute()
        {
            TargetType type = Block.TargetType;
            switch (type)
            {
                case TargetType.Unit:
                case TargetType.Enemy:
                    // 대상 지정 요청
                    UIActionSelector.UIGame.SelectBattleEntity(type, Execute);
                    break;
                case TargetType.AllUnit:
                    Execute(UIBattle.UnitList);
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
                Block.Action(entity,  (Level)_level);
            }
            
            Clear();
        }
        
        private void Execute(BattleEntity entity)
        {
            Block.Action(entity,  (Level)_level);
            
            Clear();
        }
    }
}

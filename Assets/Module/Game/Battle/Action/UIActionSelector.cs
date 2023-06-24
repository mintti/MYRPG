using System;
using System.Collections.Generic;
using System.Linq;
using Infra.Model.Game;
using UnityEngine;

namespace Module.Game.Battle
{
    /// <summary>
    /// 사용자가 처리해야할 액션
    /// </summary>
    internal class UIActionSelector : BaseMonoBehaviour
    {
        #region Varialbes
        public UIGame UIGame { get; private set; } 
        #region External

        public Transform actionContent;
        public GameObject actionPrefab;

        /// <summary>
        /// 직전 스킬 블럭의 인덱스. 동일한 경우 강화
        /// </summary>
        private int _beforeBlockIndex = -1;
        #endregion

        /// <summary>
        /// 오브젝트 관리
        /// </summary>
        private List<UIAction> Actions { get; set; } = new ();
        
        /// <summary>
        /// Action 종료 시, 다음 동작 콜백
        /// </summary>
        private Action NextAction { get; set; }

        /// <summary>
        /// 추가 종료 조건
        /// </summary>
        private Func<bool> CheckFunc { get; set; }
        #endregion

        public void Init(UIGame uiGame)
        {
            UIGame = uiGame;
            Off();
        }

        public void On() => gameObject.SetActive(true);
        public void Off() =>  gameObject.SetActive(false);

        public void AddAction(Block block)
        {
            if (block.Index == _beforeBlockIndex) // 강화
            {
                var uiAction = Actions.Last(x => x.Active);
                uiAction.Upgrade();    
            }
            else // 신규
            {
                var uiAction = Actions.FirstOrDefault(x => !x.Active);
                if (uiAction == null)
                {
                    var obj = Instantiate(actionPrefab, actionContent);
                    uiAction = obj.GetComponent<UIAction>();
                    Actions.Add(uiAction);
                    uiAction.Init(this);
                }

                uiAction.Set(block);
                _beforeBlockIndex = block.Index;
            }
        }

        
        /// <summary>
        /// 표시
        /// </summary>
        public void Show(Action nextAction, Func<bool> checkFunc = null)
        {
            NextAction = nextAction;
            CheckFunc = checkFunc;
            On();

            if (Actions.Count == 0) ExecuteAction();
        }

        public void EndBattle()
        {
            _beforeBlockIndex = -1;
            Actions.ForEach(x=> x.Hide());
        }
        
        /// <summary>
        /// 액션 수행을 알려, 남은 액션이 존재하지 않다면 다음 동작(NextAction) 수행.
        /// </summary>
        public void ExecuteAction()
        {
            if (Actions.All(x=> !x.Active) || CheckFunc == null || CheckFunc())
            {
                NextAction();
                
                Off();
                NextAction = null;
                CheckFunc = null;
                _beforeBlockIndex = -1;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Module.Game.Battle
{
    /// <summary>
    /// 사용자가 처리해야할 액션
    /// </summary>
    internal class UIActionSelector : MonoBehaviour
    {
        #region Varialbes
        public UIGame UIGame { get; private set; } 
        #region External

        public Transform actionContent;
        public GameObject actionPrefab;

    
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

        public void AddAction(ActionInfo info)
        {
            var uiAction = Actions.FirstOrDefault(x => !x.Active);
            if (uiAction == null)
            {
                var obj = Instantiate(actionPrefab, actionContent);
                uiAction = obj.GetComponent<UIAction>();
                uiAction.Init(this);
            }

            uiAction.Set(info);
        }

        
        /// <summary>
        /// 표시
        /// </summary>
        public void Show(Action nextAction, Func<bool> checkFunc = null)
        {
            NextAction = nextAction;
            CheckFunc = checkFunc;
            On();
        }
        
        public void ExecuteAction()
        {
            if (Actions.Count == 0 || CheckFunc == null || CheckFunc())
            {
                NextAction();
                
                Off();
                NextAction = null;
                CheckFunc = null;
            }
        }
    }
}

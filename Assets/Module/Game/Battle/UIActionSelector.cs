using System;
using System.Collections.Generic;
using System.Linq;
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
                Actions.Add(uiAction);
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
            }
        }
    }
}

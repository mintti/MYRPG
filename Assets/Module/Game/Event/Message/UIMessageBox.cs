using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Module.Game.Event.Message
{
    internal interface IMessageBox
    {
        void SetMessageBox(string msg, string[] answers = null, Action<string> callbackString = null,
            Action callback = null);
    }
    
    /// <summary>
    /// 다용도 메세지창
    /// </summary>
    internal class UIMessageBox : BaseMonoBehaviour, IMessageBox
    {
        #region Varaibles
        private UIGame UIGame { get; set; }
        private List<UIAnswer> AnswerBuffer {get; set; }
        private bool HasAnswer { get; set; }

        private Action<string> CallbackString { get; set; }
        private Action Callback { get; set; }
        
        #region External
        public TextMeshProUGUI questionText;
        public GameObject answerPrefab;
        public Transform answerTr;
        public void B_Click() => Click();
        #endregion
        #endregion

        private void On() => gameObject.SetActive(true);
        private void Off() => gameObject.SetActive(false);

        public void Init(UIGame uiGame)
        {
            UIGame = uiGame;
            AnswerBuffer = new ();
            HasAnswer = false;
        }
        
        public void SetMessageBox(string msg, string[] answers = null, Action<string> callbackString = null, Action callback = null)
        {
            On();
            questionText.SetText(msg);
            CallbackString = callbackString;
            Callback = callback;

            HasAnswer = answers != null; 
            
            if (HasAnswer)
            { 
                // 표시할 오브젝트 생성
                while (AnswerBuffer.Count < answers.Length)
                {
                    var uiAnswer = Instantiate(answerPrefab, answerTr).GetComponent<UIAnswer>();
                    uiAnswer.Init(this);
                    AnswerBuffer.Add(uiAnswer);
                }
                
                // 답변 설정
                for (int i = 0, cnt = answers.Length; i < cnt; i++)
                {
                    AnswerBuffer[i].On(answers[i]);
                }
            }
            else
            {
                // 클릭으로 이벤트 종료
                // StartCoroutine(nameof(EndCountDown));
            }
        }

        IEnumerator EndCountDown()
        {
            yield return new WaitForSeconds(1.0f);
            Clear();
        }

        public void SelectedAnswer(string selectedData)
        {
            AnswerBuffer.ForEach(a => a.Off());
            CallbackString?.Invoke(selectedData);
        }
        
        private void Click()
        {
            if (!HasAnswer)
            {
                Clear();
            }
        }

        private void Clear(string selectedData = null)
        {
            Callback?.Invoke();
            CallbackString?.Invoke(selectedData);
            
            AnswerBuffer.ForEach(a => a.Off());
            Off();
            Callback = null;
        }
    }
}

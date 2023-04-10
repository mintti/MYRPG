using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Module.Game.Event.Message
{
    internal class UIAnswer : MonoBehaviour
    {
        #region Variables
        private UIMessageBox UIMessageBox { get; set; }
        private TextMeshProUGUI AnswerText{ get; set; }
        #endregion

        public void Init(UIMessageBox uiIMessageBox)
        {
            UIMessageBox = uiIMessageBox;
            GetComponent<Button>().onClick.AddListener(Click);
            AnswerText = GetComponentInChildren<TextMeshProUGUI>();
            Off();
        }

        public void On(string answer)
        {
            gameObject.SetActive(true);   
            this.AnswerText.SetText(answer);
        }
        public void Off() => gameObject.SetActive(false); 
        

        private void Click()
        {
            UIMessageBox.SelectedAnswer(AnswerText.text);
        }
    }
}

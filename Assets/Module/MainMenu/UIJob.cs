using System;
using Infra.Model.Resource;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Module.MainMenu
{
    internal class UIJob : MonoBehaviour
    {
        public GameObject selectedObj;
        private UIMainMenu Parent { get; set; }
        private Job Job { get; set; }
        
        public void Start()
        {
            GetComponent<Button>().onClick.AddListener(ClickedEvent);
        }

        public void OnEnable()
        {
            selectedObj.SetActive(false);
        }

        /// <summary>
        /// 오브젝트가 생성될 때, 외부에서 값을 주입해줍니다.
        /// </summary>
        public void SetData(UIMainMenu parent, Job job)
        {
            Parent = parent;
            Job = job;
            
            var image = gameObject.GetComponent<Image>();
            var testText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            testText.text = Job.Name;
        }

        private void ClickedEvent()
        {
            var isSelected =  Parent.SelectJobObj(Job);
            selectedObj.SetActive(isSelected);
        }
    }
}

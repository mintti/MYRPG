using System;
using Infra.Model.Resource;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Module.Game
{
    internal class UIRewardItem: BaseMonoBehaviour
    {
        private UIReward UIReward { get; set; }

        public TextMeshProUGUI textGUGI;
        public Image Image;
        
        public void Init(UIReward uiReward)
        {
            UIReward = uiReward;
        }

        public void SetRewardItem(Reward reward)
        {
            On();
            
            // Resource에서 Sprite 읽어 설정 
            string text = String.Empty;
            switch (reward.Type)
            {
                case RewardType.Gold :
                    text = reward.Value.ToString();
                    break;
            }
            textGUGI.SetText(text);
        }

        private void On()
        {
            gameObject.SetActive(true);
        }
        
        public void Off()
        {
            gameObject.SetActive(false);
        }
    }
}
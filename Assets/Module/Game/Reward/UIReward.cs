using System;
using System.Collections.Generic;
using Infra.Model.Game;
using Infra.Model.Resource;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Module.Game
{
    internal class UIReward : MonoBehaviour
    {
        private UIGame UIGame { get; set; }
        
        public GameObject rewardItemPrefab;
        public Transform contentTr;

        public void B_Confirm() => Confirm();

        private List<UIRewardItem> UIRewardItemsList { get; set; } = new();
        
        private Action ConfirmAction { get; set; }
        public void Init(UIGame uiGame)
        {
            UIGame = uiGame;
            gameObject.SetActive(false);
        }

        public void Set(IEnumerable<Reward> rewards, Action confirmAction)
        {
            this.gameObject.SetActive(true);
            ConfirmAction = confirmAction;
            
            // reward 값대로 설정
            int index = 0;
            foreach (var reward in rewards)
            {
                if(index >= UIRewardItemsList.Count)
                {
                    var item = Instantiate(rewardItemPrefab, contentTr).GetComponent<UIRewardItem>();
                    UIRewardItemsList.Add(item);
                }
                
                UIRewardItemsList[index].SetRewardItem(reward);
                index++;
            }
        }

        private void Confirm()
        {
            // 오브젝트 비우기
            foreach (var item in UIRewardItemsList)
            {
                item.Off();
            }
            
            ConfirmAction?.Invoke();
            
            // 종료
            ConfirmAction = null;
            gameObject.SetActive(false);
        }
    }
}

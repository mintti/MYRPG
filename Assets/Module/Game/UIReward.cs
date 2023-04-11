using Infra.Model.Game;
using UnityEngine;

namespace Module.Game
{
    internal class UIReward : MonoBehaviour
    {
        public void Init(Reward reward)
        {
            this.gameObject.SetActive(true);
            
            // reward 값대로 설정
        }
    }
}

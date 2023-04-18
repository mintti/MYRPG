using UnityEngine;

namespace Module.Game.Battle
{
    internal class UIEntity : MonoBehaviour
    {
        public Sprite sprite;
        
        public void Init()
        {
            gameObject.SetActive(false);
        }

        public void SetEntity(Entity entity)
        {
            gameObject.SetActive(true);
        }
    }
}

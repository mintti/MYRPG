using UnityEngine;

namespace Infra
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if ((Object) MonoSingleton<T>._instance == (Object) null)
                    MonoSingleton<T>._instance = Object.FindObjectOfType<T>();
                return MonoSingleton<T>._instance;
            }
        }

        public virtual void Awake()
        {
            if ((Object) MonoSingleton<T>.Instance != (Object) this)
                Object.Destroy((Object) this.gameObject);
            else
                Object.DontDestroyOnLoad((Object) this);
        }

        public virtual void Start()
        {
        }
    }
}
    
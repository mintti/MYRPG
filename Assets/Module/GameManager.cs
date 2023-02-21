using System;
using System.Linq;
using Infra.Model.Data;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

namespace Module
{
    /// <summary>
    /// 전체적인 게임 흐름 제어
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public PlayerData PlayerData { get; set; }

        private bool IsLoaded { get; set; }


        #region Initialize
        public void Awake()
        {
            // 한 하늘 아래 GameManager 개체는 2개 일 수 없기에 ... 
            var obj = FindObjectsOfType<GameManager>();
            
            if ( obj.Length == 1)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (obj.First() != this) 
                    Destroy(gameObject);
            }
        }

        public void Start()
        {
            // 시스템 초기화는 한 번만 수행한다.
            if (!IsLoaded)
            {

                IsLoaded = true;
            }
        }

        #endregion

        #region Program
        public void ExitProgram()
        {
            
        }
        

        #endregion

        #region Game
        public void StartGame(PlayerData data = null)
        {
            data ??= PlayerData;

            SceneManager.LoadScene($"WorldScene");
        }

        #endregion
    }
}

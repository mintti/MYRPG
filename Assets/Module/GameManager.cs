using System;
using System.Linq;
using Infra;
using Infra.Model.Data;
using Infra.Model.Game;
using Module.WorldMap;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

namespace Module
{
    /// <summary>
    /// 전체적인 게임 흐름 제어
    /// </summary>
    internal class GameManager : MonoSingleton<GameManager>
    {
        #region Player Data
        public GameData GameData { get; set; }
        #endregion

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
                SceneManager.LoadScene($"MainMenuScene");
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
        /// <summary>
        /// Move to WorldScene from MainMenu.
        /// </summary>
        /// <param name="data"></param>
        public void StartGame(GameData data = null)
        {
            if (data != null) // New Game
            {
                GameData = data;
                SaveData();
            }
            
            SceneManager.LoadScene($"WorldMapScene");
        }

        /// <summary>
        /// Move to SlotScene from WorldScene.
        /// </summary>
        /// <param name="type"></param>
        public void MoveGameScene()
        {
            
            SceneManager.LoadScene($"SlotScene");
        }

        #endregion

        #region Data
        /// <summary>
        /// 임시로 작성된 저장하는 용도
        /// </summary>
        public void SaveData()
        {
            
        }

        #endregion
    }
}

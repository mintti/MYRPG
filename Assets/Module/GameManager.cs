using System;
using System.Linq;
using Infra.Model.Data;
using Module.WorldMap;
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
        #region Player Data
        public PlayerData PlayerData { get; set; }
        public DungeonType DungeonType { get; set; }
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
        public void StartGame(PlayerData data = null)
        {
            data ??= PlayerData;
            SceneManager.LoadScene($"WorldScene");
        }

        /// <summary>
        /// Move to SlotScene from WorldScene.
        /// </summary>
        /// <param name="type"></param>
        public void MoveGameScene(DungeonType type)
        {
            DungeonType = type;
            SceneManager.LoadScene($"SlotScene");
        }

        #endregion
    }
}

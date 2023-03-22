using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Infra.Model.Data;
using Infra.Model.Game;
using Infra.Model.Resource;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Module.MainMenu
{
    internal class UIMainMenu : MonoBehaviour
    {
        #region Varaibles
        private GameManager GameManager { get; set; }
        
        #region Menu
        public void B_Continue() => Continue();
        public void B_NewGame() => NewGame();
        public void B_Collection() => Collection();
        public void B_Option() => Option();
        public void B_Exit() => Exit();
        #endregion
        
        #region New Game Variables
        public GameObject newGameObject;
        public Transform jobListTarget;
        public GameObject jobPrefab;
        public Button startButton;

        private readonly List<int> _selectedJobList = new ();
        
        public void B_NewGame_Start() => StartNewGame();
        public void B_NewGame_Cancel() => CancelNewGame();
        #endregion

        #region Option Variables
        public GameObject optionGameObject;
        
        #endregion
        #endregion

        public void Start()
        {
            GameManager = FindObjectsOfType<GameManager>().First();
            newGameObject.SetActive(false);
        }


        #region Menu Method
        private void Continue()
        {
            // 기존 데이타를 로드
            GameManager.StartGame();
        }

        /// <summary>
        /// Show New Game Popup
        /// </summary>
        private void NewGame()
        {
            // Init
            _selectedJobList.Clear();
            newGameObject.SetActive(true);
            startButton.interactable = false;
            
            // 처음으로 화면을 띄울 때 로드
            if (jobListTarget.childCount > 0) return;
            foreach (var job in ResourceManager.Instance.Jobs.Skip(1))
            {
                var obj = Instantiate(jobPrefab, jobListTarget);
                obj.GetComponent<UIJob>().SetData(this, job);
            }
        }

        private void Collection()
        {
            
        }

        private void Option() => optionGameObject.SetActive(true);
        
        private void Exit() => GameManager.ExitProgram();
        #endregion

        #region New Game Method
        /// <summary>
        /// Create new player data after collect selected job's data.
        /// This Method that just do execute when list count as 4.
        /// </summary>
        private void StartNewGame()
        {
            var data = DataUtils.CreateNewData(_selectedJobList);
            GameManager.StartGame(new GameData(data));
        }

        public bool SelectJobObj(Job job)
        {
            var index = job.Index;
            
            if (_selectedJobList.Contains(index)) _selectedJobList.Remove(index);
            else if (_selectedJobList.Count != 4) _selectedJobList.Add(index);

            startButton.interactable = (_selectedJobList.Count == 4);
            return _selectedJobList.Contains(index);
        }
        
        private void CancelNewGame() => newGameObject.SetActive(false);
        #endregion
    }
}

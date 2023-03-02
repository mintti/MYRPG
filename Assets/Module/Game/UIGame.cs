using System;
using System.Collections.Generic;
using System.Linq;
using Infra.Model.Data;
using UnityEngine;

namespace Module.Game
{
    public class UIGame : MonoBehaviour, IGameCommand
    {
        #region Variables
        private GameManager GameManager { get; set; }

        #region Button
        public void B_Spin() => Spin();
        public void B_Map() => Map();
        public void B_Option() => Option();
        #endregion

        #region Game
        private PlayerData PlayerData { get; set; }
        
        

        #endregion
        #region Slot
        private bool Spinning { get; set; }
        


        #endregion
        #endregion


        #region Game

        private void Start()
        {
            GameManager = FindObjectsOfType<GameManager>().First();
            
        }

        #endregion

        #region IGameCommand

        public void Initialize()
        {
            
        }

        #endregion
        
        #region Spin Event

        private void Spin()
        {
            if (Spinning) return;

            Spinning = true;


            Spinning = false;
        }
        

        #endregion

        #region Map


        private void Map()
        {
            
        }

        #endregion
        
        #region Option

        private void Option()
        {
            
        }
        
        

        #endregion

    }
}

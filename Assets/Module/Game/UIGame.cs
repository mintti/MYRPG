using System;
using UnityEngine;

namespace Module.Game
{
    public class UIGame : MonoBehaviour
    {
        #region Command

        public Action SpinEvent { get; set; }
        public void SpinCommand() => SpinEvent();
    
        public void ShowMapCommand()
        {
        
        }
        #endregion


        #region 

    

        #endregion
    }
}

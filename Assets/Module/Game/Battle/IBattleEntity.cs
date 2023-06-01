﻿using Infra.Model.Game;
using UnityEngine;

namespace Module.Game.Battle
{
    /// <summary>
    /// UI Entity 연결 및 관련 기능
    /// </summary>
    internal interface IBattleEntity
    {
        void Connect(IBattleController controller, UIEntity uiEntity);

        void Disconnect();
        
        Sprite Sprite { get; set; }

        BattleEntity GetEntity { get; }
    }
}
using System.Collections;
using System.Collections.Generic;
using Module.Game.Battle;
using UnityEngine;

internal class UIBattleGUI : MonoBehaviour
{
    private UIBattle UIBattle { get; set; }
    
    private List<UIEntityState> EntityStates { get; set; }

    public void Init(UIBattle uiBattle)
    {
        UIBattle = uiBattle;
        EntityStates = new();
    }
    
}

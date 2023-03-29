using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infra.Model.Data;
using Module.Game.Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class UISpot : MonoBehaviour
{
    #region Varaibles
    private UIMap UIMap { get; set; }
    private Spot BaseSpot { get; set; }
    private Button _button;

    #region External
    public Sprite icon;
    public void B_Select() => UIMap.SelectSpot(BaseSpot);
    #endregion
    #endregion
    
    public void Init(UIMap map, Spot spot)
    {
        UIMap = map;
        
        _button = GetComponentInChildren<Button>();
        _button.interactable = false;

        BaseSpot = spot;
        
        // [TEST] 텍스트 설정
        string childtext = "";
        if (spot.ChildSpots != null)
        {
            foreach (var s in spot.ChildSpots)
            {
                childtext += $"{s.Index} ";
            }
        }
        string text = $"{spot.Index}\n({childtext})";
        GetComponentInChildren<TextMeshProUGUI>().SetText(text);
        
        
        Refresh();
    }

    public void Refresh()
    {
        switch (BaseSpot.State)
        {
            case SpotState.Do:
                _button.interactable = true;
                break;
            case SpotState.None:
            case SpotState.Clear :
                _button.interactable = false;
                break;
        }
    }
}

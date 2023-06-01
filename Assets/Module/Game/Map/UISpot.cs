using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infra.Model.Data;
using Module;
using Module.Game.Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class UISpot : BaseMonoBehaviour
{
    #region Varaibles
    private UIMap UIMap { get; set; }
    private Spot BaseSpot { get; set; }
    private Button _button;

    #region External
    public void B_Select() => UIMap.UIGame.SelectMap(BaseSpot);
    public Image icon;
    #endregion
    #endregion
    
    public void Init(UIMap map, Spot spot)
    {
        UIMap = map;
        BaseSpot = spot;
        
        _button = GetComponentInChildren<Button>();
        _button.interactable = false;

        icon.sprite = ResourceManager.Instance.MapSprites[(int) BaseSpot.Event.Type];
        
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

    /// <summary>
    /// 객체를 제거
    /// </summary>
    public void ClearAndDestroy()
    {
        BaseSpot = null;
        UIMap = null;
        Destroy(gameObject);
    }
}

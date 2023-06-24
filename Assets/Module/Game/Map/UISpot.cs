using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infra.Model.Data;
using Module;
using Module.Game.Map;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

internal class UISpot : BaseMonoBehaviour
{
    #region Varaibles
    private UIMap UIMap { get; set; }
    private Spot BaseSpot { get; set; }
    
    private Button _button;
    private List<UILine> _childLines;
    private bool _updateStop;
    #region External
    public void B_Select() => UIMap.UIGame.SelectMap(BaseSpot);
    public GameObject linePrefab;
    public Transform lineContentTr;
    public Image icon;
    #endregion
    
    #endregion
    
    public void Init(UIMap map, Spot spot)
    {
        UIMap = map;
        BaseSpot = spot;

        _button = GetComponentInChildren<Button>();
        _button.interactable = false;

        _updateStop = false;
        
        icon.sprite = ResourceManager.Instance.MapSprites[(int) BaseSpot.Event.Type];
    }

    public void CreateLine(in List<UISpot> list)
    {
        if (BaseSpot.ChildSpots != null)
        {
            var currentTr = GetComponent<RectTransform>();
            _childLines = new();
            
            foreach (var child in BaseSpot.ChildSpots)
            {
                int index = list.FindIndex(x => x.BaseSpot.Index == child.Index);
                var line = Instantiate(linePrefab, lineContentTr).GetComponent<UILine>();
                var targetTr = list[index].GetComponent<RectTransform>();
                
                line.Init(child.Index, currentTr, targetTr);
                line.Color = _button.colors.disabledColor;
                _childLines.Add(line);
            }
        }
        
        Refresh();
    }

    public void Refresh()
    {
        if (_updateStop) return;
        
        var interac = false;
        
        switch (BaseSpot.State)
        {
            case SpotState.Do:
                interac = true;
                _childLines?.ForEach(line => line.Color = _button.colors.disabledColor);
                break;
            case SpotState.Clear :
                icon.color =  _button.colors.normalColor;
                BaseSpot.State = SpotState.History;
                _childLines?.ForEach(line => line.Color = _button.colors.normalColor);
                break;
            case SpotState.History :
                int index = 0;
                foreach (var child in BaseSpot.ChildSpots)
                {
                    if (child.State != SpotState.Clear)
                    {
                        _childLines[index].Color = _button.colors.disabledColor;
                    }
                    index++;
                }
                _updateStop = true;
                return;
                break;
        }
        
        _button.interactable = interac;
        
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

using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Module;
using Module.Game.Battle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

internal class UIEntityState : BaseMonoBehaviour
{
    private UIEntity _uiEntity;
    
    public RectTransform hpGaugeTr;
    public TextMeshProUGUI hpText;
    
    public void Connect(UIEntity uiEntity)
    {
        _uiEntity = uiEntity;
        gameObject.SetActive(true);
    }
    
    public void UpdateHp(int current, int max)
    {
        Vector2 size = new(50f * ((float) current / max), hpGaugeTr.sizeDelta.y); 
        hpGaugeTr.sizeDelta = size;
        hpText.SetText($"{current}/{max}");
    }

    public void Dead()
    {
        Clear();
    }

    public void Clear()
    {
        gameObject.SetActive(false);
    }

    public void UpdatePosition(Vector3 pos)
    {
        transform.position = pos;
    }
}

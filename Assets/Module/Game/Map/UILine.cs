using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class UILine : MonoBehaviour
{
    public int TargetIndex { get; set; }
    private Image _image;

    public Color Color { set { _image.color = value; } }
    
    public void Init(int targetIndex, RectTransform currentTr, RectTransform targetTr)
    {
        TargetIndex = targetIndex;
        _image = GetComponent<Image>();
        
        var rect = GetComponent<RectTransform>();
        
        var curPos = currentTr.position;
        var targetPos = targetTr.position; 

        var x = targetPos.x - curPos.x;
        var y = targetPos.y - curPos.y;
 
        var height = Math.Sqrt(x * x + y * y);
        rect.sizeDelta = new(8,(float)height);
        
        var angle = (Math.Atan(Math.Abs(x) / y) * 180 / Math.PI) * (x < 0 ? 1 : -1) ;
        rect.eulerAngles  = new Vector3(0, 0, (float) angle);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaspeClicker : Button
{
    float startSize = 1;
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        startSize -= 0.1f;
        if(startSize >= 0)
        {
            transform.localScale = Vector3.one * startSize;
        }
    }
}

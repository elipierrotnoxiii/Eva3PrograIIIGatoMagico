using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Raspe : Button
{
    public float value;
    public delegate void OnRaspeClick(float val);
    OnRaspeClick onRaspeClicked;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        transform.localScale = Vector3.one * 0.8f;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        transform.localScale = Vector3.one;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        onRaspeClicked?.Invoke(value);
    }

    public void RaspeAddListener(OnRaspeClick raspeClick)
    {
        onRaspeClicked += raspeClick;
    }

    public void RaspeRemoveListener(OnRaspeClick raspeClick)
    {
        onRaspeClicked -= raspeClick;
    }
}

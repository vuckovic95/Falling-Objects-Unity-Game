using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Arrow : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private bool _hasClicked;
    public void OnPointerDown(PointerEventData eventData)
    {
        _hasClicked = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hasClicked = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _hasClicked = false;
    }

    public bool HasClicked
    {
        get { return _hasClicked; }
    }
}

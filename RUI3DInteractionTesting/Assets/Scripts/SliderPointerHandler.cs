using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderPointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public delegate void SliderEnter(bool turnCameraOff);
    public static event SliderEnter SliderEnterEvent;

    public void OnPointerEnter(PointerEventData data)
    {
        SliderEnterEvent?.Invoke(true);
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (!Input.GetMouseButton(0)) SliderEnterEvent?.Invoke(false);
    }

    public void OnPointerUp(PointerEventData data) {
        SliderEnterEvent?.Invoke(false);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderPointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public delegate void SliderEnter(bool turnCameraOff);
    public static event SliderEnter SliderEnterEvent;

    private bool m_IsCameraUsed = false;

    void OnEnable()
    {
        UserInputHandler.MouseHeldEvent += SetCameraUse;
    }

    void OnDestroy()
    {
        UserInputHandler.MouseHeldEvent -= SetCameraUse;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (m_IsCameraUsed) return;
        SliderEnterEvent?.Invoke(true);
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (m_IsCameraUsed) return;
        if (!Input.GetMouseButton(0)) SliderEnterEvent?.Invoke(false);
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (m_IsCameraUsed) return;
        SliderEnterEvent?.Invoke(false);
    }

    void SetCameraUse(bool isMouseUsed)
    {
        m_IsCameraUsed = isMouseUsed;
    }
}

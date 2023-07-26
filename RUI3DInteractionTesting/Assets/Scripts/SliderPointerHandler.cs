using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderPointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{

    public static event Action<bool> OnPointerChange;

    [SerializeField] private bool m_IsCameraUsed = false;

    void OnEnable()
    {
        //UserInputHandler.MouseHeldEvent += SetCameraUse;
        SimpleOrbitCamera.OnCameraMove += SetCameraUse;
    }

    void OnDestroy()
    {
        //UserInputHandler.MouseHeldEvent -= SetCameraUse;
        SimpleOrbitCamera.OnCameraMove -= SetCameraUse;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        //if (m_IsCameraUsed) return;
        OnPointerChange?.Invoke(true);
    }

    public void OnPointerExit(PointerEventData data)
    {
        //if (m_IsCameraUsed) return;
        OnPointerChange?.Invoke(false);
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (m_IsCameraUsed) return;
        OnPointerChange?.Invoke(false);
    }

    void SetCameraUse(bool isMouseUsed)
    {
        m_IsCameraUsed = isMouseUsed;
    }
}

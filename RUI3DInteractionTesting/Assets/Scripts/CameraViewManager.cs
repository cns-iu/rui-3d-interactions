using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraViewManager : MonoBehaviour
{

    public delegate void CameraReset();
    public static event CameraReset CameraResetEvent;

    [SerializeField] private Button m_ViewReset;
    [SerializeField] private Vector3 m_DefaultPosition;
    [SerializeField] private Quaternion m_DefaultRotation;
    [SerializeField] private float m_DefaultFOV;

    void Start()
    {
        SetDefaultView();

        m_ViewReset.onClick.AddListener(
            delegate
            {
                ResetCamera();
                CameraResetEvent?.Invoke();
            }
         );
    }

    void ResetCamera()
    {
        transform.position = m_DefaultPosition;
        transform.rotation = m_DefaultRotation;
        GetComponent<Camera>().fieldOfView = m_DefaultFOV;
    }

    void SetDefaultView()
    {
        m_DefaultPosition = transform.position;
        m_DefaultRotation = transform.rotation;
        m_DefaultFOV = GetComponent<Camera>().fieldOfView;
    }
}

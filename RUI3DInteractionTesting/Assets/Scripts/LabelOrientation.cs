using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelOrientation : MonoBehaviour
{
    public Camera[] m_Cameras;
    public Camera m_ActiveCamera;
    // Start is called before the first frame update
    void OnEnable()
    {
        UserInputModule.CameraChangeEvent += DetermineActiveCamera;
    }

    void OnDestroy()
    {
        UserInputModule.CameraChangeEvent -= DetermineActiveCamera;
    }

    void Awake()
    {
        m_ActiveCamera = Camera.main;
    }

    void DetermineActiveCamera(NewActiveCamera newCamera)
    {
        m_ActiveCamera = m_Cameras[(int)newCamera];
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(m_ActiveCamera.transform);
        transform.Rotate(0f, 180f, 0f);
    }
}

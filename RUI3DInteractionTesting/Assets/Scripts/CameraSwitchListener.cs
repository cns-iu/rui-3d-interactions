using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchListener : MonoBehaviour
{
    public NewActiveCamera m_Role;
    void OnEnable()
    {
        // UserInputModule.CameraChangeEvent += SetDepth;
        UserInputModule.CameraChangeEvent += EnableCamera;
    }

    void OnDisable()
    {
        // UserInputModule.CameraChangeEvent -= SetDepth;
        UserInputModule.CameraChangeEvent += EnableCamera;
    }
    // Start is called before the first frame update
    void SetDepth(NewActiveCamera newActiveCamera)
    {
        float depth;
        if (newActiveCamera == m_Role)
        {
            depth = 0f;
        }
        else
        {
            depth = -2f;
        }
        this.GetComponent<Camera>().depth = depth;
    }

    void EnableCamera(NewActiveCamera newActiveCamera)
    {
        this.GetComponent<Camera>().enabled = newActiveCamera == m_Role;
    }
}

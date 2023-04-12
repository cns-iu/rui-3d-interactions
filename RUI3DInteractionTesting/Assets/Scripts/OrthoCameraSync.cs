using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthoCameraSync : MonoBehaviour
{
    private void OnEnable()
    {
        UserInputModule.CameraChangeEvent += MoveCamera;
    }

    private void OnDestroy()
    {
        UserInputModule.CameraChangeEvent -= MoveCamera;
    }

    void MoveCamera(NewActiveCamera newCamera)
    {
        
        if (newCamera == NewActiveCamera.Orbit)
        {
            Debug.Log("yes");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetReset : MonoBehaviour
{
    private Vector3 defaultPosition;

    private void Awake()
    {
        defaultPosition = transform.position;
    }

    private void OnEnable()
    {
        UserInputModule.CameraChangeEvent += (NewActiveCamera newCamera) =>
        {
            if (newCamera == NewActiveCamera.Register)
            {
                transform.position = defaultPosition;
            } };
    }

    private void OnDestroy()
    {
        
    }
}

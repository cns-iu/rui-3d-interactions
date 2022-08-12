using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCamera : MonoBehaviour
{
    [SerializeField] GameObject CameraToFollow;
    [SerializeField] bool isMovementAllowed = false;
    [SerializeField] bool isZoomAllowed = false;

    // Update is called once per frame
    void Update()
    {
        switch (CameraToFollow.GetComponent<CameraSwitchListener>().m_Role)
        {
            case NewActiveCamera.Register:
            
                transform.position = CameraToFollow.transform.position;
                transform.forward = CameraToFollow.transform.forward;
                break;
            case NewActiveCamera.Orbit:
                
                transform.rotation = CameraToFollow.transform.rotation;
                //transform.forward = CameraToFollow.transform.forward;
                break;
            default:
                break;
        }
        // transform.position = m_MainCamera.transform.position;

    }
}

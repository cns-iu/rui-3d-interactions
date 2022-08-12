using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera orbitCamera;
    [SerializeField] private Camera staticCamera;
    [SerializeField] private Camera skinCamera;

    [SerializeField] private CameraControls orbitCameraScript;
    [SerializeField] private CameraControls skinCameraScript;

    private Camera activeCamera;

    private void Start()
    {
        StartStatic();
    }

    /// <summary>
    /// Start the game in static camera mode
    /// needs to be used over swap because of raceconditions with the reset
    /// </summary>
    public void StartStatic()
    {
        staticCamera.enabled = true;
        orbitCamera.enabled = false;

        //will want to reset the positions of the cameras, but for now lock their movement

        orbitCameraScript.m_CanBeUsed = false;
        skinCameraScript.m_CanBeUsed = false;
    }

    /// <summary>
    /// Swap the camera to static mode
    /// reset the orbit and skin cameras
    /// </summary>
    public void SwapToStatic()
    {
        staticCamera.enabled = true;
        orbitCamera.enabled = false;

        //will want to reset the positions of the cameras, but for now lock their movement

        orbitCameraScript.m_CanBeUsed = false;
        skinCameraScript.m_CanBeUsed = false;

        orbitCameraScript.ResetPosition();
        skinCameraScript.ResetPosition();
    }

    /// <summary>
    /// Swap to the orbit camera
    /// enable the the orbit and skin cameras
    /// </summary>
    public void SwapToOrbit()
    {
        staticCamera.enabled = false;
        orbitCamera.enabled = true;

        orbitCameraScript.m_CanBeUsed = true;
        skinCameraScript.m_CanBeUsed = true;
    }
}

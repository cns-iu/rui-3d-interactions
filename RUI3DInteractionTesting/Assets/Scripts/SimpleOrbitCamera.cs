using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleOrbitCamera : MonoBehaviour
{
    public static event Action<bool> OnCameraMove;

    [SerializeField]
    Transform _target;
    [SerializeField] Vector3 _defaultTargetPosition;
    [SerializeField] Quaternion _defaultTargetRotation;
    [SerializeField]
    float rotationSpeed;
    public float m_PanSpeed;
    [SerializeField] Vector3 _cameraOffset;
    [SerializeField] Vector3 _defaultCameraOffset;
    [SerializeField] float _defaultFOV;
    [SerializeField] Quaternion camTurnAngleX;
    [SerializeField] Quaternion camTurnAngleY;
    [SerializeField] float smoothFactor;
    public bool m_DoesPointerAllow = true;
    [SerializeField] private bool _canBeUsed = true;



    void OnEnable()
    {
        SliderPointerHandler.OnPointerChange += SetCameraUsage;
        CameraViewManager.CameraResetEvent += ResetCamera;

    }

    void OnDestroy()
    {
        SliderPointerHandler.OnPointerChange -= SetCameraUsage;
        CameraViewManager.CameraResetEvent -= ResetCamera;
    }

    void Awake()
    {
        _defaultFOV = GetComponent<Camera>().fieldOfView;
        SetDefaultCameraOffset();
        SetDefaultTarget();
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    void SetDefaultTarget()
    {
        _defaultTargetPosition = _target.transform.position;
        _defaultTargetRotation = _target.transform.rotation;
    }

    void SetDefaultCameraOffset()
    {
        _defaultCameraOffset = this.transform.position - _target.transform.position;
        _cameraOffset = _defaultCameraOffset;
    }

    void ResetCamera()
    {
        _target.transform.position = _defaultTargetPosition;
        _target.transform.rotation = _defaultTargetRotation;
        _cameraOffset = _defaultCameraOffset;
        GetComponent<Camera>().fieldOfView = _defaultFOV;
    }

    void LateUpdate()
    {
        if (!_canBeUsed) return;

        if (Input.GetMouseButton(0))
        {
            Quaternion camTurnAngleX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            Quaternion camTurnAngleY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotationSpeed, Vector3.back);

            if (transform.rotation.eulerAngles.y >= 0f && transform.rotation.eulerAngles.y <= 180f)
            {
                camTurnAngleY = Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * rotationSpeed, Vector3.back);
            }

            _cameraOffset = camTurnAngleX * camTurnAngleY * _cameraOffset;
            Vector3 newPos = _target.position + _cameraOffset;
            this.transform.position = Vector3.Slerp(this.transform.position, newPos, smoothFactor);

            OnCameraMove(true);
        }
        else
        {
            OnCameraMove(false);
        }

        if (Input.GetMouseButton(1))
        {
            float camPosDeltaX = Input.GetAxis("Mouse X");
            float camPosDeltaY = Input.GetAxis("Mouse Y");

            transform.Translate(Vector3.right * -camPosDeltaX * m_PanSpeed, Space.Self);
            transform.Translate(Vector3.up * -camPosDeltaY * m_PanSpeed, Space.Self);
            _target.transform.rotation = transform.rotation;
            _target.transform.Translate(Vector3.right * -camPosDeltaX * m_PanSpeed, Space.Self);
            _target.transform.Translate(Vector3.up * -camPosDeltaY * m_PanSpeed, Space.Self);
        }
        if (Input.mouseScrollDelta != new Vector2(0f, 0f) && !Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<Camera>().fieldOfView -= Input.mouseScrollDelta.y;
        }
        this.transform.LookAt(_target.transform);

    }

    void SetCameraUsage(bool turnCameraOff)
    {
        _canBeUsed = !turnCameraOff;
    }
}

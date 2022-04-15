using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleOrbitCamera : MonoBehaviour
{
    [SerializeField]
    Transform m_Target;
    [SerializeField] Vector3 m_DefaultTargetPosition;
    [SerializeField] Quaternion m_DefaultTargetRotation;
    [SerializeField]
    float rotationSpeed;
    public float m_PanSpeed;
    [SerializeField] Vector3 _cameraOffset;
    [SerializeField] Vector3 m_DefaultCameraOffset;
    [SerializeField] Quaternion camTurnAngleX;
    [SerializeField] Quaternion camTurnAngleY;
    [SerializeField] float smoothFactor;
    public bool m_DoesPointerAllow = true;
    [SerializeField] bool m_CanBeUsed = true;


    void OnEnable()
    {
        SliderPointerHandler.SliderEnterEvent += SetCameraUsage;
        CameraViewManager.CameraResetEvent += ResetCamera;
    }

    void OnDestroy()
    {
        SliderPointerHandler.SliderEnterEvent -= SetCameraUsage;
        CameraViewManager.CameraResetEvent -= ResetCamera;
    }

    void Start()
    {
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
        m_DefaultTargetPosition = m_Target.transform.position;
        m_DefaultTargetRotation = m_Target.transform.rotation;
    }

    void SetDefaultCameraOffset()
    {
        m_DefaultCameraOffset = this.transform.position - m_Target.transform.position;
        _cameraOffset = m_DefaultCameraOffset;
    }

    void ResetCamera()
    {
        _cameraOffset = m_DefaultCameraOffset;
        m_Target.transform.position = m_DefaultTargetPosition;
        m_Target.transform.rotation = m_DefaultTargetRotation;
    }

    void LateUpdate()
    {
        if (!m_CanBeUsed) return;

        if (Input.GetMouseButton(0))
        {
            Quaternion camTurnAngleX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            Quaternion camTurnAngleY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotationSpeed, Vector3.back);

            if (transform.rotation.eulerAngles.y >= 0f && transform.rotation.eulerAngles.y <= 180f)
            {
                camTurnAngleY = Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * rotationSpeed, Vector3.back);
            }

            _cameraOffset = camTurnAngleX * camTurnAngleY * _cameraOffset;
            Vector3 newPos = m_Target.position + _cameraOffset;
            this.transform.position = Vector3.Slerp(this.transform.position, newPos, smoothFactor);
        }
        if (Input.GetMouseButton(1))
        {
            float camPosDeltaX = Input.GetAxis("Mouse X");
            float camPosDeltaY = Input.GetAxis("Mouse Y");

            transform.Translate(Vector3.right * -camPosDeltaX * m_PanSpeed, Space.Self);
            transform.Translate(Vector3.up * -camPosDeltaY * m_PanSpeed, Space.Self);
            m_Target.transform.rotation = transform.rotation;
            m_Target.transform.Translate(Vector3.right * -camPosDeltaX * m_PanSpeed, Space.Self);
            m_Target.transform.Translate(Vector3.up * -camPosDeltaY * m_PanSpeed, Space.Self);
        }
        if (Input.mouseScrollDelta != new Vector2(0f, 0f) && !Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<Camera>().fieldOfView -= Input.mouseScrollDelta.y;
        }
        this.transform.LookAt(m_Target.transform);

    }

    void SetCameraUsage(bool turnCameraOff)
    {
        m_CanBeUsed = !turnCameraOff;
    }
}

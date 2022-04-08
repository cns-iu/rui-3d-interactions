using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleOrbitCamera : MonoBehaviour
{
    [SerializeField]
    Transform m_Target;
    [SerializeField]
    float rotationSpeed;
    public float m_PanSpeed;
    Vector3 _cameraOffset;
    Quaternion camTurnAngleX;
    Quaternion camTurnAngleY;
    [SerializeField]
    float smoothFactor;
    public bool m_DoesPointerAllow = true;
    [SerializeField] bool m_CanBeUsed = true;

    void OnEnable()
    {
        SliderPointerHandler.SliderEnterEvent += SetCameraUsage;
    }

    void OnDestroy()
    {
        SliderPointerHandler.SliderEnterEvent -= SetCameraUsage;
    }



    void Start()
    {
        _cameraOffset = this.transform.position - m_Target.transform.position;
    }
    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
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
            // transform.Translate(Vector3.forward * Input.mouseScrollDelta.y * m_PanSpeed, Space.Self);
            // m_Target.transform.rotation = transform.rotation;
            // m_Target.transform.Translate(Vector3.forward * Input.mouseScrollDelta.y * m_PanSpeed, Space.Self);
        }
        this.transform.LookAt(m_Target.transform);

    }

    void SetCameraUsage(bool turnCameraOff)
    {
        m_CanBeUsed = !turnCameraOff;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CamControl : MonoBehaviour
{

    [SerializeField]
    CameraSwitchListener m_CameraSwitchListener;
    [SerializeField]
    private bool m_IsMovementAllowed = false;
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


    // public Slider[] m_Sliders = new Slider[3];
    // Start is called before the first frame update

    void OnEnable()
    {
        UserInputModule.CameraChangeEvent += SetMovementAllowed;
        MouseEventTriggerComponent.PointerEvent += SetSliderUsageStatus;
    }

    void OnDisable()
    {
        UserInputModule.CameraChangeEvent -= SetMovementAllowed;
        MouseEventTriggerComponent.PointerEvent -= SetSliderUsageStatus;
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
        if (m_IsMovementAllowed)
        {
            if (Input.GetMouseButton(0))
            {
                Quaternion camTurnAngleX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
                Quaternion camTurnAngleY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotationSpeed, Vector3.back);

                // Quaternion camTurnAngleX = Quaternion.Euler(0f, Input.GetAxis("Mouse X") * rotationSpeed, 0f);
                // Quaternion camTurnAngleY = Quaternion.Euler(-Input.GetAxis("Mouse Y") * rotationSpeed, 0f, 0f);

                // Debug.Log(transform.rotation.eulerAngles);

                if (transform.rotation.eulerAngles.y >= 0f && transform.rotation.eulerAngles.y <= 180f)
                {
                    camTurnAngleY = Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * rotationSpeed, Vector3.back);
                }

                // Debug.DrawRay(transform.position, Vector3.back, Color.blue);
                // Debug.LogFormat("Mouse X: {0}, Mouse Y: {1}, camTurnAngleX: {2}, camTurnAngleY: {3}", Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), camTurnAngleX, camTurnAngleX);
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
            if (Input.mouseScrollDelta != new Vector2(0f, 0f))
            {
                // this.GetComponent<Camera>().fieldOfView -= Input.mouseScrollDelta.y;
                transform.Translate(Vector3.forward * Input.mouseScrollDelta.y * m_PanSpeed, Space.Self);
                m_Target.transform.rotation = transform.rotation;
                m_Target.transform.Translate(Vector3.forward * Input.mouseScrollDelta.y * m_PanSpeed, Space.Self);
            }
            this.transform.LookAt(m_Target.transform);
        }
    }

    void SetMovementAllowed(NewActiveCamera newActiveCamera)
    {
        m_IsMovementAllowed = ((newActiveCamera == m_CameraSwitchListener.m_Role));
        // Debug.Log(m_IsMovementAllowed);
    }

    public void SetSliderUsageStatus(bool isAllowed)
    {
        // Debug.Log(isAllowed);
        m_IsMovementAllowed = isAllowed;
    }
}

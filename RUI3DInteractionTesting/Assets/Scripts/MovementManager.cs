using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementManager : MonoBehaviour
{
    [SerializeField]
    private Dropdown m_Dropdown;

    [SerializeField]
    private Space m_ReferenceSpace;

    [SerializeField]
    float m_MovementUnit = 0.001f;
    [SerializeField]
    bool isWASDAllowed = true;
    bool isDragAllowed = true;

    public delegate void ReferenceSpaceChange(Space newReferenceSpace);
    public static event ReferenceSpaceChange OnReferenceSpaceChangeEvent;

    public delegate void Movement(string direction);
    public static event Movement MovementEvent;
    private float mZCoord;
    private Vector3 mOffset;
    void OnEnable()
    {
        UserInputModule.OnUserButtonPressEvent += MoveBlock;
        // UserInputModule.CameraChangeEvent += EnableWASDMode;
        UserInputModule.CameraChangeEvent += EnableMouseDrag;
        m_Dropdown.onValueChanged.AddListener(
   delegate
   {
       SetReferenceSpace(m_Dropdown.value);
   }
        );
    }

    void OnDestroy()
    {
        UserInputModule.OnUserButtonPressEvent -= MoveBlock;
        // UserInputModule.CameraChangeEvent -= EnableWASDMode;
        UserInputModule.CameraChangeEvent -= EnableMouseDrag;
    }

    private void OnMouseDrag()
    {
        if (isDragAllowed)
        {
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            transform.position = GetMouseAsWorldPoint() + mOffset;

            // Store offset = gameobject world pos - mouse world pos
            mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
        }

    }

    private Vector3 GetMouseAsWorldPoint()

    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void EnableMouseDrag(NewActiveCamera newCamera)
    {
        isDragAllowed = newCamera == NewActiveCamera.Register;
    }


    void EnableWASDMode(NewActiveCamera newActiveCamera)
    {
        isWASDAllowed = newActiveCamera == NewActiveCamera.Orbit;
    }

    void SetReferenceSpace(int option = 0)
    {
        // Debug.Log(option);
        m_ReferenceSpace = (Space)option;
        OnReferenceSpaceChangeEvent?.Invoke(m_ReferenceSpace);
    }

    void MoveBlock(KeyCode key)
    {
        if (isWASDAllowed)
        {
            Transform t = this.transform;

            MovementEvent?.Invoke(key.ToString());

            switch (key)
            {
                case KeyCode.W:
                    t.Translate(0, m_MovementUnit, 0, m_ReferenceSpace);
                    break;
                case KeyCode.A:
                    t.Translate(-m_MovementUnit, 0f, 0, m_ReferenceSpace);
                    break;
                case KeyCode.S:
                    t.Translate(0, -m_MovementUnit, 0, m_ReferenceSpace);
                    break;
                case KeyCode.D:
                    t.Translate(m_MovementUnit, 0f, 0, m_ReferenceSpace);
                    break;
                case KeyCode.Q:
                    t.Translate(0, 0f, m_MovementUnit, m_ReferenceSpace);
                    break;
                case KeyCode.E:
                    t.Translate(0, 0f, -m_MovementUnit, m_ReferenceSpace);
                    break;
                default:
                    break;
            }
        }

    }
}

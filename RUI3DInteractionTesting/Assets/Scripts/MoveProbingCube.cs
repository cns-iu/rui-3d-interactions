using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProbingCube : MonoBehaviour
{
    [SerializeField] private float m_MovementUnitActive;
    [SerializeField] private float m_MovementUnitNormal;
    [SerializeField] private float m_MovementUnitModified;
    [SerializeField] private Space m_ReferenceSpace;

    public delegate void CubeMove(KeyCode key);
    public static event CubeMove CubeMoveEvent;

    void OnEnable()
    {
        UserInputHandler.KeyHeldEvent += SetMovementUnit;
    }

    void OnDestroy()
    {
        UserInputHandler.KeyHeldEvent -= SetMovementUnit;
    }

    void Start()
    {
        m_MovementUnitActive = m_MovementUnitNormal;
    }

    void OnGUI()
    {
        TakeUserInput();
    }
    void TakeUserInput()
    {
        Event e = Event.current;
        if (e.isKey && e.keyCode != KeyCode.None && e.type != EventType.KeyUp)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                MoveBlock(e.keyCode, true);
            }
            MoveBlock(e.keyCode, false);
            CubeMoveEvent?.Invoke(e.keyCode);

        }
    }

    void SetMovementUnit(string key, bool isHeld)
    {
        if (isHeld)
        {
            m_MovementUnitActive = m_MovementUnitModified;
        }
        else
        {
            m_MovementUnitActive = m_MovementUnitNormal;
        }
    }

    void MoveBlock(KeyCode key, bool modified)
    {
        Transform t = this.transform;
        switch (key)
        {
            case KeyCode.W:
                t.Translate(0, m_MovementUnitActive, 0, m_ReferenceSpace);
                break;
            case KeyCode.A:
                t.Translate(m_MovementUnitActive, 0f, 0, m_ReferenceSpace);
                break;
            case KeyCode.S:
                t.Translate(0, -m_MovementUnitActive, 0, m_ReferenceSpace);
                break;
            case KeyCode.D:
                t.Translate(-m_MovementUnitActive, 0f, 0, m_ReferenceSpace);
                break;
            case KeyCode.Q:
                t.Translate(0, 0f, -m_MovementUnitActive, m_ReferenceSpace);
                break;
            case KeyCode.E:
                t.Translate(0, 0f, m_MovementUnitActive, m_ReferenceSpace);
                break;
            default:
                break;
        }
    }
}

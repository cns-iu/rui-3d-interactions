using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProbingCube : MonoBehaviour
{
    [SerializeField] private float m_MovementUnit;
    [SerializeField] private Space m_ReferenceSpace;

    void OnGUI()
    {
        TakeUserInput();
    }
    void TakeUserInput()
    {
        Event e = Event.current;
        if (e.isKey && e.keyCode != KeyCode.None && e.type != EventType.KeyUp)
        {
            MoveBlock(e.keyCode);
        }
    }
    void MoveBlock(KeyCode key)
    {
        Transform t = this.transform;
        switch (key)
        {
            case KeyCode.W:
                t.Translate(0, m_MovementUnit, 0, m_ReferenceSpace);
                break;
            case KeyCode.A:
                t.Translate(m_MovementUnit, 0f, 0, m_ReferenceSpace);
                break;
            case KeyCode.S:
                t.Translate(0, -m_MovementUnit, 0, m_ReferenceSpace);
                break;
            case KeyCode.D:
                t.Translate(-m_MovementUnit, 0f, 0, m_ReferenceSpace);
                break;
            case KeyCode.Q:
                t.Translate(0, 0f, -m_MovementUnit, m_ReferenceSpace);
                break;
            case KeyCode.E:
                t.Translate(0, 0f, m_MovementUnit, m_ReferenceSpace);
                break;
            default:
                break;
        }
    }
}

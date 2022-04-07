using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreserveSize : MonoBehaviour
{
    private Transform m_Parent;
    private Vector3 m_StartScale;

    void OnEnable()
    {
        UserInputHandler.MouseScrollEvent += Unparent;
        UserInputHandler.KeyPressedEvent += Parent;
        MoveProbingCube.CubeMoveEvent += Parent;
        SizeAdjust.ResetEvent += Reset;
        SizeAdjust.SliderMovedEvent += Unparent;
    }

    void OnDestroy()
    {
        UserInputHandler.MouseScrollEvent -= Unparent;
        UserInputHandler.KeyPressedEvent -= Parent;
        MoveProbingCube.CubeMoveEvent -= Parent;
        SizeAdjust.ResetEvent -= Reset;
        SizeAdjust.SliderMovedEvent -= Unparent;
    }

    void Awake()
    {
        m_Parent = transform.parent;
        m_StartScale = transform.localScale;
    }

    void Reset()
    {
        transform.localScale = m_StartScale;
    }

    void Unparent(float value)
    {
        transform.parent = null;
    }

    void Parent(string key)
    {
        transform.parent = m_Parent;
    }

    void Parent(KeyCode key)
    {
        transform.parent = m_Parent;
    }
}

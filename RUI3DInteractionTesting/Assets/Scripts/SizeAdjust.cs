using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeAdjust : MonoBehaviour
{
    [SerializeField] private GameObject m_OuterProbingCube;
    [SerializeField] private float m_CubeSize = .01f;
    [SerializeField] private float m_SizeAdjustment = .01f;
    void OnEnable()
    {
        UserInputHandler.MouseScrollEvent += AdjustSize;
    }

    void OnDestroy()
    {
        UserInputHandler.MouseScrollEvent -= AdjustSize;
    }

    void AdjustSize(float value)
    {
        float newsize = m_OuterProbingCube.transform.localScale.x + value * m_SizeAdjustment;
        m_OuterProbingCube.transform.localScale = new Vector3(newsize, newsize, newsize);
    }

}

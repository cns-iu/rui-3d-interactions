using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrientation : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Target;
    void Update()
    {
        GetComponent<Camera>().transform.LookAt(m_Target.transform.position);
    }
}

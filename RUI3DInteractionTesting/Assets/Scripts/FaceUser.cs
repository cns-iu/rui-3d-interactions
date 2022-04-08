using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceUser : MonoBehaviour
{
    [SerializeField] private Camera m_MainCamera;
    [SerializeField] private Vector3 m_Rotation;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(m_MainCamera.transform);
        transform.Rotate(m_Rotation.x, m_Rotation.y, m_Rotation.z);
    }
}

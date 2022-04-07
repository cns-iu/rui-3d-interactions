using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceUser : MonoBehaviour
{
    [SerializeField] private Camera m_MainCamera;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(m_MainCamera.transform);
        transform.Rotate(0, 180, 0);
    }
}

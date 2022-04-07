using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCamera : MonoBehaviour
{
    [SerializeField] GameObject m_MainCamera;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = m_MainCamera.transform.position;
        transform.position = m_MainCamera.transform.position;
        transform.forward = m_MainCamera.transform.forward;
    }
}

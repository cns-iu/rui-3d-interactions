using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceUser : MonoBehaviour
{
    //[SerializeField] private Camera m_MainCamera;
    [SerializeField] private Vector3 m_Rotation;

    [SerializeField] private List<GameObject> cameras;

    private GameObject m_Camera;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(m_Camera.transform);
        transform.Rotate(m_Rotation.x, m_Rotation.y, m_Rotation.z);
    }

    private void Awake()
    {
        m_Camera = Camera.main.gameObject;
    }

    private void OnEnable()
    {
        UserInputModule.CameraChangeEvent += (NewActiveCamera n) =>
        {

            switch (n)
            {
                case NewActiveCamera.Register:
                    m_Camera = cameras[0];
                    break;
                case NewActiveCamera.Orbit:
                    m_Camera = cameras[1];
                    break;
                default:
                    break;
            }
        };
    }

    private void OnDestroy()
    {

    }
}

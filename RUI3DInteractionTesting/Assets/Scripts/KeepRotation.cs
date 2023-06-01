using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRotation : MonoBehaviour
{
    [SerializeField] private Vector3 _targetRotation = new Vector3(0, 0, 0);

    void Update()
    {
        transform.eulerAngles = _targetRotation;
    }
}

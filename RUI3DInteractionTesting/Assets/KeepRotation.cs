using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRotation : MonoBehaviour
{
    [SerializeField] private Transform Twin;
   
    void Update()
    {
        transform.rotation = Twin.rotation;
    }
}

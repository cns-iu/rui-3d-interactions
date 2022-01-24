using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] List<Material> m_TissueBlockMaterials = new List<Material>();
    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Renderer>().material = m_TissueBlockMaterials[1];
    }

    void OnTriggerStay(Collider other)
    {
        if (FullyContains(other))
        {
            other.GetComponent<Renderer>().material = m_TissueBlockMaterials[2];
        }
        else
        {
            other.GetComponent<Renderer>().material = m_TissueBlockMaterials[1];
        }

    }

    void OnTriggerExit(Collider other)
    {
        other.GetComponent<Renderer>().material = m_TissueBlockMaterials[0];
    }

    bool FullyContains(Collider resident)
    {
        Collider zone = GetComponent<Collider>();
        Debug.Log(GetComponent<Collider>());
        if (zone == null)
        {
            return false;
        }
        return zone.bounds.Contains(resident.bounds.max) && zone.bounds.Contains(resident.bounds.min);
    }
}

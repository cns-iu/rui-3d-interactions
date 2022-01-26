using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public delegate void CollisionData(TissueBlockData data, bool isCollided);
    public static event CollisionData CollisionDataEvent;
    [SerializeField] List<Material> m_TissueBlockMaterials = new List<Material>();
    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Renderer>().material = m_TissueBlockMaterials[1];
        CollisionDataEvent?.Invoke(other.GetComponent<TissueBlockData>(), true);
        SetOutline(other, true);
    }

    void OnTriggerStay(Collider other)
    {
        if (FullyContains(other))
        {
            other.GetComponent<Renderer>().material = m_TissueBlockMaterials[2];
            other.gameObject.GetComponent<Outline>().OutlineColor = Color.green;
        }
        else
        {
            other.GetComponent<Renderer>().material = m_TissueBlockMaterials[1];
            other.gameObject.GetComponent<Outline>().OutlineColor = Color.yellow;
        }

    }

    void OnTriggerExit(Collider other)
    {
        other.GetComponent<Renderer>().material = m_TissueBlockMaterials[0];
        other.gameObject.GetComponent<Outline>().OutlineColor = Color.white;
        CollisionDataEvent?.Invoke(other.GetComponent<TissueBlockData>(), false);
        SetOutline(other, false);
    }

    bool FullyContains(Collider resident)
    {
        Collider zone = GetComponent<Collider>();
        if (zone == null)
        {
            return false;
        }
        return zone.bounds.Contains(resident.bounds.max) && zone.bounds.Contains(resident.bounds.min);
    }

    void SetOutline(Collider other, bool isOn)
    {
        if (isOn)
        {

            other.gameObject.GetComponent<Outline>().OutlineWidth = 2f;
        }
        else
        {
            other.gameObject.GetComponent<Outline>().OutlineWidth = .5f;
        }

    }
}

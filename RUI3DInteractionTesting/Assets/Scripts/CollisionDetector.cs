using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public delegate void CollisionData(TissueBlockData data, bool isCollided);
    public static event CollisionData CollisionDataEvent;
    private int m_NumSelectedBlocks = 0;
    [SerializeField] private List<Material> m_TissueBlockMaterials = new List<Material>();
    [SerializeField]
    private List<Color> m_Colors = new List<Color>() {
        new Color(255f,255f,255f),
        new Color(22f,95f,159f),
    };
    private Color m_StartColor;

    void OnTriggerEnter(Collider other)
    {
        m_StartColor = other.GetComponent<Renderer>().material.color;
        other.gameObject.GetComponent<Outline>().OutlineColor = m_Colors[1];
        other.GetComponent<Renderer>().material.color = m_Colors[1];
        CollisionDataEvent?.Invoke(other.GetComponent<TissueBlockData>(), true);
        SetOutline(other, true);
    }

    void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<Outline>().OutlineColor = m_Colors[0];
        other.GetComponent<Renderer>().material.color = m_StartColor;
        CollisionDataEvent?.Invoke(other.GetComponent<TissueBlockData>(), false);
        SetOutline(other, false);
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

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
        new Color(255f,136f,0f),
        new Color(0,229,255),
        new Color(224,64,251),
        new Color(130,177,255),
        new Color(172,243,43),
        new Color(115,35,226)
    };


    void OnTriggerEnter(Collider other)
    {
        m_NumSelectedBlocks++;
        int colorIndex;
        if (m_NumSelectedBlocks >= m_Colors.Count)
        {
            colorIndex = m_NumSelectedBlocks - m_Colors.Count;
        }
        else
        {
            colorIndex = m_NumSelectedBlocks - 1;
        }

        other.GetComponent<Renderer>().material.color = m_Colors[colorIndex];
        other.gameObject.GetComponent<Outline>().OutlineColor = m_Colors[colorIndex];
        CollisionDataEvent?.Invoke(other.GetComponent<TissueBlockData>(), true);
        SetOutline(other, true);
    }

    void OnTriggerExit(Collider other)
    {
        m_NumSelectedBlocks--;
        other.GetComponent<Renderer>().material = m_TissueBlockMaterials[0];
        other.gameObject.GetComponent<Outline>().OutlineColor = Color.white;
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

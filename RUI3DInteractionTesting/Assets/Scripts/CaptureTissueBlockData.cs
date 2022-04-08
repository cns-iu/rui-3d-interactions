using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureTissueBlockData : MonoBehaviour
{
    public delegate void Inventory(List<AnatomicalStructures> uniqueAS, List<CellTypes> uniqueCT, int number);
    public static event Inventory UpdateInventoryEvent;
    private List<AnatomicalStructures> m_UniqueAS = new List<AnatomicalStructures>();
    private List<CellTypes> m_UniqueCT = new List<CellTypes>();
    [SerializeField] private int m_NumberTissueBlocks;


    void OnEnable()
    {
        CollisionDetector.CollisionDataEvent += GetTissueBlockData;
    }

    void OnDestroy()
    {
        CollisionDetector.CollisionDataEvent -= GetTissueBlockData;
    }
    void GetTissueBlockData(TissueBlockData data, bool isCollided)
    {
        // Debug.Log(data.m_ListAS[0]);
        UpdateAS(data.m_ListAS, isCollided);
        UpdateCT(data.m_ListCT, isCollided);
        UpdateNumberTissueBlock(isCollided);
        Debug.Log(m_NumberTissueBlocks);
        UpdateInventoryEvent?.Invoke(m_UniqueAS, m_UniqueCT, m_NumberTissueBlocks);
    }

    void UpdateNumberTissueBlock(bool isCollided)
    {
        m_NumberTissueBlocks = isCollided ? m_NumberTissueBlocks + 1 : m_NumberTissueBlocks - 1;
    }

    void UpdateAS(List<AnatomicalStructures> list, bool isCollided)
    {
        if (isCollided)
        {
            foreach (var item in list)
            {
                m_UniqueAS.Add(item);
            }

        }
        else
        {
            foreach (var item in list)
            {
                m_UniqueAS.Remove(item);
            }
        }

    }
    void UpdateCT(List<CellTypes> list, bool isCollided)
    {
        if (isCollided)
        {
            foreach (var item in list)
            {
                m_UniqueCT.Add(item);
            }

        }
        else
        {
            foreach (var item in list)
            {
                m_UniqueCT.Remove(item);
            }
        }
    }
}

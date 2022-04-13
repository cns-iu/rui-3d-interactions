using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureTissueBlockData : MonoBehaviour
{
    public delegate void Inventory(List<AnatomicalStructures> uniqueAS, List<CellTypes> uniqueCT, int number);
    public static event Inventory UpdateInventoryEvent;
    private List<AnatomicalStructures> m_AnatomicalStructures = new List<AnatomicalStructures>();
    private List<CellTypes> m_CellTypes = new List<CellTypes>();
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
        UpdateInventoryEvent?.Invoke(m_AnatomicalStructures, m_CellTypes, m_NumberTissueBlocks);
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
                m_AnatomicalStructures.Add(item);
            }

        }
        else
        {
            foreach (var item in list)
            {
                m_AnatomicalStructures.Remove(item);
            }
        }

    }
    void UpdateCT(List<CellTypes> list, bool isCollided)
    {
        if (isCollided)
        {
            foreach (var item in list)
            {
                m_CellTypes.Add(item);
            }

        }
        else
        {
            foreach (var item in list)
            {
                m_CellTypes.Remove(item);
            }
        }
    }
}

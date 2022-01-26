using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureTissueBlockData : MonoBehaviour
{
    public delegate void Inventory(List<AnatomicalStructures> uniqueAS, List<CellTypes> uniqueCT);
    public static event Inventory UpdateInventoryEvent;
    private List<AnatomicalStructures> m_UniqueAS = new List<AnatomicalStructures>();
    private List<CellTypes> m_UniqueCT = new List<CellTypes>();


    void OnEnable()
    {
        CollisionDetector.CollisionDataEvent += GetTissueBlockData;
    }

    void OnDestroy()
    {
        CollisionDetector.CollisionDataEvent -= GetTissueBlockData;
    }
    // Start is called before the first frame update
    void GetTissueBlockData(TissueBlockData data, bool isCollided)
    {
        // Debug.Log(data.m_ListAS[0]);
        UpdateAS(data.m_ListAS, isCollided);
        UpdateCT(data.m_ListCT, isCollided);

        UpdateInventoryEvent?.Invoke(m_UniqueAS, m_UniqueCT);
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

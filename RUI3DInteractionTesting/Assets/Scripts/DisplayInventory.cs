using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    [SerializeField] List<TMP_Text> m_Text = new List<TMP_Text>();
    [SerializeField] HashSet<AnatomicalStructures> uniqueASHash = new HashSet<AnatomicalStructures>();
    [SerializeField] HashSet<CellTypes> uniqueCTHash = new HashSet<CellTypes>();

    void OnEnable()
    {
        CaptureTissueBlockData.UpdateInventoryEvent += SetText;
    }

    void OnDestroy()
    {
        CaptureTissueBlockData.UpdateInventoryEvent -= SetText;
    }
    void SetText(List<AnatomicalStructures> uniqueAS, List<CellTypes> uniqueCT, int number)
    {
        m_Text[0].text = "You are colliding with these anatomical structures:\n";
        m_Text[1].text = "You are colliding with these cell types:\n";

        uniqueASHash = ConvertListToHashSet<AnatomicalStructures>(uniqueAS);
        uniqueCTHash = ConvertListToHashSet<CellTypes>(uniqueCT);

        foreach (var item in uniqueASHash)
        {
            m_Text[0].text += " - " + item + "\n";
        }

        foreach (var item in uniqueCTHash)
        {
            m_Text[1].text += " - " + item + "\n";
        }
        // m_Text[1].text += "</mark>";

        SetCounts(uniqueASHash, uniqueCTHash, number);

        foreach (var item in m_Text)
        {
            if (item.text.Equals(""))
            {
                ShowStandardText();
            }
        }

    }

    void SetCounts(HashSet<AnatomicalStructures> uniqueASHash, HashSet<CellTypes> uniqueCTHash, int number)
    {
        m_Text[2].text = "Tissue Block Collisions: " + number + " \nAnatomical Structure Collisions: " + uniqueASHash.Count + "\nCell Type Predictions via ASCT + B Tables: " + uniqueCTHash.Count;
    }

    HashSet<T> ConvertListToHashSet<T>(List<T> list)
    {
        HashSet<T> result = new HashSet<T>();
        foreach (var item in list)
        {
            result.Add(item);
        }
        return result;
    }

    private void Start()
    {
        ShowStandardText();
    }

    private void ShowStandardText()
    {
        m_Text[0].text = "You are colliding with these anatomical structures:\n";
        m_Text[1].text = "You are colliding with these cell types:\n";
        m_Text[2].text = "Tissue Block Collisions: " + " \nAnatomical Structure Collisions: " + "\nCell Type Predictions via ASCT + B Tables: ";
    }
}

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
    void SetText(List<AnatomicalStructures> anatomicalStructures, List<CellTypes> cellTypes, int number)
    {
        m_Text[0].text = "You are colliding with these anatomical structures:" + BuildString(GetCounts(anatomicalStructures));
        m_Text[1].text = "You are colliding with these cell types:" + BuildString(GetCounts(cellTypes));

        uniqueASHash = ConvertListToHashSet<AnatomicalStructures>(anatomicalStructures);
        uniqueCTHash = ConvertListToHashSet<CellTypes>(cellTypes);

        // foreach (var item in uniqueASHash)
        // {
        //     m_Text[0].text += " - " + item + "\n";
        // }

        // foreach (var item in uniqueCTHash)
        // {
        //     m_Text[1].text += " - " + item + "\n";
        // }
        // // m_Text[1].text += "</mark>";

        SetCounts(uniqueASHash, uniqueCTHash, number);

        foreach (var item in m_Text)
        {
            if (item.text.Equals(""))
            {
                ShowStandardText();
            }
        }

    }

    Dictionary<AnatomicalStructures, int> GetCounts(List<AnatomicalStructures> list)
    {
        var result = new Dictionary<AnatomicalStructures, int>();
        for (int i = 0; i < list.Count; i++)
        {
            result[list[i]] = result.ContainsKey(list[i]) ? result[list[i]] + 1 : 1;
        }

        foreach (var item in result)
        {
            Debug.Log(item);
        }

        return result;
    }

    Dictionary<CellTypes, int> GetCounts(List<CellTypes> list)
    {
        var result = new Dictionary<CellTypes, int>();
        for (int i = 0; i < list.Count; i++)
        {
            result[list[i]] = result.ContainsKey(list[i]) ? result[list[i]] + 1 : 1;
        }

        foreach (var item in result)
        {
            Debug.Log(item);
        }

        return result;
    }

    void SetCounts(HashSet<AnatomicalStructures> uniqueASHash, HashSet<CellTypes> uniqueCTHash, int number)
    {
        m_Text[2].text =
        "Tissue Block Collisions: " + number
        + " \nUnique Anatomical Structure Collisions: " + uniqueASHash.Count
        + "\nUnique Cell Type Predictions via ASCT + B Tables: " + uniqueCTHash.Count;
    }

    string BuildString(Dictionary<AnatomicalStructures, int> dict)
    {
        string result = "";

        foreach (var pair in dict)
        {
            result += "\n" + pair.Key + ": " + pair.Value;
        }
        return result;
    }

    string BuildString(Dictionary<CellTypes, int> dict)
    {
        string result = "";

        foreach (var pair in dict)
        {
            result += "\n" + pair.Key + ": " + pair.Value;
        }
        return result;
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

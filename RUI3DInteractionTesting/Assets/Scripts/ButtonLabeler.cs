using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLabeler : MonoBehaviour
{
    public List<GameObject> m_Labels = new List<GameObject>();
    // Start is called before the first frame update
    void OnEnable()
    {
        MovementManager.OnReferenceSpaceChangeEvent += SetLabels;
    }

    private void OnDestroy()
    {
        MovementManager.OnReferenceSpaceChangeEvent -= SetLabels;
    }

    // Update is called once per frame
    void SetLabels(Space newSpace)
    {
        for (int i = 0; i < m_Labels.Count; i++)
        {
            m_Labels[i].gameObject.SetActive(i == (int)newSpace);
        }
    }
}


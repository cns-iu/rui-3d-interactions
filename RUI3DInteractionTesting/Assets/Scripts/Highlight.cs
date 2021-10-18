using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Highlight : MonoBehaviour
{
    public GameObject m_Panel;
    public float m_Duration = .05f;
    void OnEnable()
    {
        MovementManager.MovementEvent += SetPanelActive;
    }

    void OnDisable()
    {
        MovementManager.MovementEvent -= SetPanelActive;
    }

    private void Awake()
    {
        m_Panel = transform.GetChild(0).gameObject;
    }

    // Start is called before the first frame update
    void SetPanelActive(string key)
    {
        if (this.gameObject.name.Equals(key))
        {
            Debug.Log(key);
            StartCoroutine(HighlightForDuration());
        }

    }

    IEnumerator HighlightForDuration()
    {
        m_Panel.SetActive(true);
        yield return new WaitForSeconds(m_Duration);
        m_Panel.SetActive(false);
    }
}

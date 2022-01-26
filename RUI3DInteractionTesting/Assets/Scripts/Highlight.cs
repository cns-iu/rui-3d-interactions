using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Highlight : MonoBehaviour
{
    public GameObject m_Panel;
    public float m_Duration = .05f;

    [SerializeField] private bool m_IsHoldingKey = false;

    void OnEnable()
    {
        MovementManager.MovementEvent += SetPanelActive;

        UserInputHandler.KeyPressedEvent += SetPanelActive;

        UserInputHandler.KeyHeldEvent += SetPanelActive;



    }

    void OnDisable()
    {
        MovementManager.MovementEvent -= SetPanelActive;
        UserInputHandler.KeyPressedEvent -= SetPanelActive;
        UserInputHandler.KeyHeldEvent += SetPanelActive;
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
            if (!m_IsHoldingKey)
            {
                StartCoroutine(HighlightForDuration());
            }

        }
    }

    void SetPanelActive(string key, bool isHeld)
    {

        if (m_IsHoldingKey)
        {
            if (this.gameObject.name.Equals(key))
            {
                StartCoroutine(HighlightUntilRelease(isHeld));
            }
        }

    }

    IEnumerator HighlightUntilRelease(bool isHeld)
    {
        m_Panel.SetActive(isHeld);
        yield return null;
    }

    IEnumerator HighlightForDuration()
    {
        m_Panel.SetActive(true);
        yield return new WaitForSeconds(m_Duration);
        m_Panel.SetActive(false);
    }
}

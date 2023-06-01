using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Highlight : MonoBehaviour
{
    public GameObject _panel;
    public float m_Duration = .05f;

    [SerializeField] private bool m_IsHoldingKey = false;

    void OnEnable()
    {
        //MovementManager.MovementEvent += SetPanelActive;

        //UserInputHandler.KeyPressedEvent += SetPanelActive;

        //UserInputHandler.KeyHeldEvent += SetPanelActive;

        KeyHandler.keyPressed += SetPanelActive;
    }

    void OnDisable()
    {
        //MovementManager.MovementEvent -= SetPanelActive;
        //UserInputHandler.KeyPressedEvent -= SetPanelActive;
        //UserInputHandler.KeyHeldEvent -= SetPanelActive;

        KeyHandler.keyPressed -= SetPanelActive;
    }

    private void Awake()
    {
        _panel = transform.GetChild(0).gameObject;
    }

    // Start is called before the first frame update
    void SetPanelActive(KeyCode key)
    {
        if (this.gameObject.name.Equals(key.ToString()))
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
        _panel.SetActive(isHeld);
        yield return null;
    }

    IEnumerator HighlightForDuration()
    {
        _panel.SetActive(true);
        yield return new WaitForSeconds(m_Duration);
        _panel.SetActive(false);
    }
}

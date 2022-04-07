using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideOnToggle : MonoBehaviour
{
    [SerializeField] private Toggle m_Toggle;

    // Start is called before the first frame update
    void Start()
    {
        m_Toggle.onValueChanged.AddListener(
            delegate {
                gameObject.SetActive(m_Toggle.isOn);
            }
        );
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

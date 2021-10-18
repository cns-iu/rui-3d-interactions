using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationManager : MonoBehaviour
{
    public Slider[] m_RotationSliders = new Slider[3];
    public Button m_ResetButton;

    [SerializeField]
    private float m_RotationUnit;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var slider in m_RotationSliders)
        {
            string axis = slider.gameObject.name.Substring(0, 1);
            slider.onValueChanged.AddListener(
            delegate
            {
                switch (axis)
                {
                    case "X":
                        transform.eulerAngles = new Vector3(slider.value,
                                                            transform.rotation.eulerAngles.y,
                                                            transform.rotation.eulerAngles.z);
                        break;
                    case "Y":
                        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x,
                                                             slider.value,
                                                             transform.rotation.eulerAngles.z);
                        break;
                    case "Z":
                        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x,
                                                            transform.rotation.eulerAngles.y,
                                                            slider.value);
                        break;
                    default:
                        break;
                }

            }
            );
        }

        m_ResetButton.onClick.AddListener(delegate
        {
            ResetSliders();
            
        });

    }

    void ResetSliders() { 
        foreach (var slider in m_RotationSliders)
        {
            slider.value = 0f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdjustRotationText : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private TMP_Text _text;

    private void OnEnable()
    {
        _text = GetComponent<TMP_Text>();
        _slider.onValueChanged.AddListener(
            (v) =>
            {
                _text.text = Mathf.Round(v).ToString();
            }
            );
    }
}

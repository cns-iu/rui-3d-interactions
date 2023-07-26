using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdjustIncrementText : MonoBehaviour
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _text.text = Math.Round((AdjustMovementUnit.Instance.CurrentMovementUnit * 1000f), 2).ToString() + " mm";
    }
}

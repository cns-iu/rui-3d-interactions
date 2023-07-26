using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustMovementUnit : MonoBehaviour
{
    public static AdjustMovementUnit Instance { get; private set; }
    [SerializeField] private float _currentMovementUnit = 0.005f;

    public float CurrentMovementUnit
    {
        get { return _currentMovementUnit; }
        private set { }
    }

    [SerializeField] private float _changeUnit = 0.0025f;
    [SerializeField] private List<Button> _buttons = new List<Button>();

    private void OnEnable()
    {
        _buttons[0].onClick.AddListener(
            () =>
            {
                _currentMovementUnit += _changeUnit;
                _currentMovementUnit = Mathf.Clamp(_currentMovementUnit, 0.0005f, 0.0095f);
            }
            );

        _buttons[1].onClick.AddListener(
            () =>
            {
                _currentMovementUnit -= _changeUnit;
                _currentMovementUnit = Mathf.Clamp(_currentMovementUnit, 0.0005f, 0.0095f);
            }
            );
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
}

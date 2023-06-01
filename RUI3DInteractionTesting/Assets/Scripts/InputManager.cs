using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public List<KeyHandler> allKeyHandlers = new List<KeyHandler>();
    public static event Action<Space> OnSpaceChange;
    public Space CurrentSpace { get; private set; }

    [SerializeField] private TMP_Dropdown _dropdown;

    private void Update()
    {
        HandleKeyEvents();
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(SetSpace);
    }

    private void HandleKeyEvents()
    {
        foreach (KeyHandler handler in allKeyHandlers)
        {
            handler.HandleState();
        }
    }

    private void SetSpace(int selection)
    {
        CurrentSpace = selection == 0 ? Space.World : Space.Self;
        OnSpaceChange?.Invoke(CurrentSpace);
    }

    public static InputManager Instance { get; private set; }


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

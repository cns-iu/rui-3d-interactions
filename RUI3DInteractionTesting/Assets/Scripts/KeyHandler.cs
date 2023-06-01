using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "NewKeyHandler")]
public class KeyHandler : InputHandler
{
    public KeyCode keyCode;
    public static event Action<KeyCode> keyPressed;

    public override void HandleState()
    {
        if (Input.GetKeyDown(keyCode))
        {
            keyPressed?.Invoke(keyCode);
        }
    }
}

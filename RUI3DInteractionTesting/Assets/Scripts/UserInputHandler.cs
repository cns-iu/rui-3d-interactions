using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputHandler : MonoBehaviour
{
    public delegate void MouseUpdate(float value);
    public static event MouseUpdate MouseScrollEvent;

    public delegate void KeyPressed(string key);
    public static event KeyPressed KeyPressedEvent;

    public delegate void KeyHeld(string key, bool isHeld);
    public static event KeyHeld KeyHeldEvent;

    public delegate void MouseHeld(bool isHeld);
    public static event MouseHeld MouseHeldEvent;

    void OnGUI()
    {
        // GetMouseScrollWheel();
        GetKeyPress();
        GetMouseButtonHold();
    }

    void GetKeyPress()
    {
        Event e = GetCurrentEvent();
        if (e.isKey && e.keyCode != KeyCode.None && e.type != EventType.KeyUp)
        {
            KeyPressedEvent?.Invoke(e.keyCode.ToString());
        }

        if (e.isKey && e.type == EventType.KeyDown && e.keyCode == KeyCode.LeftShift)
        {
            KeyHeldEvent?.Invoke(e.keyCode.ToString(), true);
        }

        if (e.isKey && e.type == EventType.KeyUp && e.keyCode == KeyCode.LeftShift)
        {
            KeyHeldEvent?.Invoke(e.keyCode.ToString(), false);
        }
    }

    void GetMouseScrollWheel()
    {
        Event e = GetCurrentEvent();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (e.isScrollWheel)
            {
                MouseScrollEvent?.Invoke(Input.mouseScrollDelta.y);
                KeyPressedEvent?.Invoke("MouseScroll");
            }
        }


    }

    void GetMouseButtonHold()
    {
        MouseHeldEvent?.Invoke(Input.GetMouseButton(0));
    }


    Event GetCurrentEvent()
    {
        Event e = Event.current;
        return e;
    }

}



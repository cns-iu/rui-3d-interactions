using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputHandler : MonoBehaviour
{
    public delegate void MouseUpdate(float value);
    public static event MouseUpdate MouseScrollEvent;
    void OnGUI()
    {
        GetMouseScrollWheel();
    }

    void GetMouseScrollWheel()
    {
        Event e = Event.current;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (e.isScrollWheel)
            {
                MouseScrollEvent?.Invoke(Input.mouseScrollDelta.y);
            }
        }
    }

}

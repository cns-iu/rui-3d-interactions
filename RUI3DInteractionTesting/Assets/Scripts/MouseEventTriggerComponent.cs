using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseEventTriggerComponent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void PointerAction(bool isDown);
    public static event PointerAction PointerEvent;

    // public void OnPointerDown(PointerEventData eventData)
    // {
    //     Debug.Log(eventData);

    //     // Your code here
    // }

    // Start is called before the first frame update

    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log(eventData);
        PointerEvent?.Invoke(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Debug.Log(eventData);
        PointerEvent?.Invoke(true);
    }
}
//     void Awake()
//     {
//         GetComponent<Slider>().OnPointerDown(
// data
//         );


//         // trigger = gameObject.AddComponent<EventTrigger>();
//         // EventTrigger.Entry entry = new EventTrigger.Entry();
//         // entry.eventID = EventTriggerType.PointerDown;
//         // entry.callback.AddListener((eventData) =>
//         // {
//         //     PointerEvent?.Invoke(false);
//         // });

//         // // trigger.delegates.Add(entry);
//         // trigger.triggers.Add(entry);

//         // entry.eventID = EventTriggerType.PointerUp;
//         // entry.callback.AddListener((eventData) =>
//         // {
//         //     PointerEvent?.Invoke(true);
//         // });

//         // // trigger.delegates.Add(entry);
//         // trigger.triggers.Add(entry);
//     }



//     // Update is called once per frame
//     public void OnPointerDownDelegate(PointerEventData data)
//     {
//         Debug.Log("OnPointerDownDelegate called.");
//     }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelHider : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        UserInputModule.CameraChangeEvent += ShowOrHide;

    }

    // Update is called once per frame
    void OnDestroy()
    {
        UserInputModule.CameraChangeEvent -= ShowOrHide;
    }

    void ShowOrHide(NewActiveCamera newActiveCamera) {
        GetComponent<Text>().enabled = newActiveCamera == NewActiveCamera.Orbit;
    }
}

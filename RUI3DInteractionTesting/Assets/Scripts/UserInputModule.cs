using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NewRendering { Perspective, Orthographic }
public enum NewPosition { Right, Left, Posterior, Anterior }

public enum NewActiveCamera { Register, Orbit, Second }
public class UserInputModule : MonoBehaviour
{
    public delegate void Perspective(NewRendering newRendering);
    public static event Perspective PerspectiveChangeEvent;

    public delegate void Position(NewPosition newPosition);
    public static event Position PositionChangeEvent;

    public delegate void Animations(bool areAnimationsOn);
    public static event Animations AnimationsChangeEvent;
    public delegate void CameraChange(NewActiveCamera newActiveCamera);
    public static event CameraChange CameraChangeEvent;

    public delegate void UserButtonPress(KeyCode key);
    public static event UserButtonPress OnUserButtonPressEvent;

    [SerializeField]
    private Toggle[] m_TogglesPosition = new Toggle[4];

    [SerializeField]
    private Toggle[] m_TogglesRendering = new Toggle[2];

    [SerializeField]
    private Toggle[] m_TogglesCameras = new Toggle[2];

    [SerializeField]
    private Toggle m_Animations;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var button in m_TogglesPosition)
        {
            button.onValueChanged.AddListener(
                delegate
                {
                    NewPosition moveTo;
                    moveTo = (NewPosition)System.Enum.Parse(typeof(NewPosition), button.name);
                    PositionChangeEvent?.Invoke(moveTo);
                    m_TogglesCameras[0].isOn = true;
                }
            );
        }

        foreach (var button in m_TogglesRendering)
        {
            button.onValueChanged.AddListener(
                delegate
                {
                    NewRendering renderAs;
                    if (button.gameObject.name.Equals("Orthographic"))
                    {
                        renderAs = NewRendering.Orthographic;
                    }
                    else
                    {
                        renderAs = NewRendering.Perspective;
                    }
                    PerspectiveChangeEvent?.Invoke(renderAs);
                }
            );
        }

        foreach (var button in m_TogglesCameras)
        {
            button.onValueChanged.AddListener(
                delegate
                {
                    NewActiveCamera newCamera = NewActiveCamera.Orbit;
                    if (button.gameObject.name.Equals("Register"))
                    {
                        if (button.isOn) {
                            Debug.Log(button.gameObject.name.Equals("Register"));
                            newCamera = NewActiveCamera.Register;
                        } 
                        
                    }
                    else
                    {
                        newCamera = NewActiveCamera.Orbit;
                    }
                    CameraChangeEvent?.Invoke(newCamera);
                }
            );
        }

        m_Animations.onValueChanged.AddListener(
            delegate
            {
                AnimationsChangeEvent?.Invoke(m_Animations.isOn);
            }
        );
    }

   
    void OnGUI()
    {
        TakeUserInput();
    }
    void TakeUserInput()
    {
        Event e = Event.current;
        if (e.isKey && e.keyCode != KeyCode.None && e.type != EventType.KeyUp)
        {
            OnUserButtonPressEvent?.Invoke(e.keyCode);
        }
    }
}

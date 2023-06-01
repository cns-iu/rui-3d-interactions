using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveManager : MonoBehaviour
{
    public static MoveManager Instance { get; private set; }
    [SerializeField] private float _movementUnit = 0.005f;
    [SerializeField] private Transform _tissueBlock;
    [SerializeField] private Slider[] _sliders = new Slider[3];

    private void OnEnable()
    {
        KeyHandler.keyPressed += OnMove;

        foreach (var slider in _sliders)
        {
            string axis = slider.gameObject.name.Substring(0, 1);
            slider.onValueChanged.AddListener(
                (val) =>
                {
                    string axis = slider.gameObject.name.Substring(0, 1);
                    OnRotate(val, axis);
                }
                );
        }

    }

    private void OnDestroy()
    {
        KeyHandler.keyPressed -= OnMove;
    }

    private void OnMove(KeyCode k)
    {
        switch (k)
        {
            case KeyCode.W:
                _tissueBlock.Translate(0f, _movementUnit, 0f, InputManager.Instance.CurrentSpace);
                break;

            case KeyCode.A:
                _tissueBlock.Translate(_movementUnit, 0f, 0f, InputManager.Instance.CurrentSpace);
                break;

            case KeyCode.S:
                _tissueBlock.Translate(0f, -_movementUnit, 0f, InputManager.Instance.CurrentSpace);
                break;

            case KeyCode.D:
                _tissueBlock.Translate(-_movementUnit, 0f, 0f, InputManager.Instance.CurrentSpace);
                break;

            case KeyCode.Q:
                _tissueBlock.Translate(0f, 0f, -_movementUnit, InputManager.Instance.CurrentSpace);
                break;

            case KeyCode.E:
                _tissueBlock.Translate(0f, 0f, _movementUnit, InputManager.Instance.CurrentSpace);
                break;

            default:
                break;
        }
    }

    private void OnRotate(float newVal, string axis)
    {
        switch (axis)
        {
            case "X":
                _tissueBlock.eulerAngles = new Vector3(newVal,
                                                    _tissueBlock.eulerAngles.y,
                                                    _tissueBlock.eulerAngles.z);
                break;
            case "Y":
                _tissueBlock.eulerAngles = new Vector3(_tissueBlock.eulerAngles.x,
                                                     newVal,
                                                     _tissueBlock.eulerAngles.z);
                break;
            case "Z":
                _tissueBlock.eulerAngles = new Vector3(_tissueBlock.eulerAngles.x,
                                                    _tissueBlock.eulerAngles.y,
                                                    newVal);
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        _tissueBlock = transform.GetChild(0).transform;
    }
}
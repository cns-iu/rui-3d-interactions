using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SizeAdjust : MonoBehaviour
{
    public delegate void Reset();
    public static event Reset ResetEvent;

    public delegate void SliderMoved(float value);
    public static event SliderMoved SliderMovedEvent;

    public delegate void SliderUsage();
    public static event SliderUsage SliderUsageEvent;
    [SerializeField] private float m_SizePercentage = 1f;
    [SerializeField] private Slider m_OpacitySlider;
    [SerializeField] private GameObject m_OuterProbingCube;
    [SerializeField] private float m_SizeAdjustment = .01f;
    [SerializeField] private Button m_ResetButton;
    [SerializeField] private float m_DefaultDiameter;
    [SerializeField] private float m_MinDiameter;
    [SerializeField] private float m_MaxDiameter;
    private bool m_IsSliderBeingSetNow = false;
    private Vector3 m_StartPosition;

    void OnEnable()
    {
        UserInputHandler.MouseScrollEvent += AdjustSize;
        MoveProbingCube.CubeMoveEvent += ResetSize;
    }

    void OnDestroy()
    {
        UserInputHandler.MouseScrollEvent -= AdjustSize;
        MoveProbingCube.CubeMoveEvent -= ResetSize;
    }

    void Start()
    {
        ResetSize();
        m_StartPosition = transform.position;
        SetUpSlider();

        m_ResetButton.onClick.AddListener(
          delegate
          {
              ResetPosition();
              ResetEvent?.Invoke();
          });
    }


    void SetUpSlider()
    {
        m_OpacitySlider.minValue = m_MinDiameter;
        m_OpacitySlider.maxValue = m_MaxDiameter;

        m_OpacitySlider.onValueChanged.AddListener(
                    delegate
                    {
                        if (!m_IsSliderBeingSetNow)
                        {
                            SliderMovedEvent?.Invoke(0f);

                            transform.localScale = new Vector3(
                       Mathf.Lerp(m_DefaultDiameter, 1f, m_OpacitySlider.value),
                       Mathf.Lerp(m_DefaultDiameter, 1f, m_OpacitySlider.value),
                       Mathf.Lerp(m_DefaultDiameter, 1f, m_OpacitySlider.value)
                       );
                        }
                    }
                );
    }

    void ResetSize(KeyCode key = KeyCode.L)
    {
        if (key == KeyCode.L)
        {
            transform.localScale = new Vector3(m_DefaultDiameter, m_DefaultDiameter, m_DefaultDiameter);
        }
    }

    void ResetPosition()
    {
        transform.position = m_StartPosition;
    }

    void AdjustSize(float value)
    {
        float newSize = transform.localScale.x + value * m_SizeAdjustment;

        if (newSize > m_MaxDiameter)
        {
            newSize = m_MaxDiameter;
        }
        else if (newSize < m_MinDiameter)
        {
            newSize = m_MinDiameter;
        }
        transform.localScale = new Vector3(newSize, newSize, newSize);

        m_IsSliderBeingSetNow = true;
        m_OpacitySlider.value = Mathf.Lerp(0, 1, transform.localScale.x);
        m_IsSliderBeingSetNow = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputListener : MonoBehaviour
{
    [SerializeField]
    private float m_OrthographicSize;
    [SerializeField]
    private float transitionTime = 1f;
    [SerializeField]
    private Transform[] m_PredeterminedCameraPositions = new Transform[4];
    private Camera m_Camera;
    private bool m_ShowAnimations = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        UserInputModule.PerspectiveChangeEvent += ChangeRendering;
        UserInputModule.PositionChangeEvent += ChangePosition;
        UserInputModule.AnimationsChangeEvent += SetAnimations;
    }

    void OnDisable()
    {
        UserInputModule.PerspectiveChangeEvent -= ChangeRendering;
        UserInputModule.PositionChangeEvent -= ChangePosition;
        UserInputModule.AnimationsChangeEvent -= SetAnimations;
    }

    void ChangeRendering(NewRendering newRendering)
    {
        m_Camera.orthographic = (newRendering == NewRendering.Orthographic);
        m_Camera.orthographicSize = m_OrthographicSize;
    }

    void ChangePosition(NewPosition newPosition)
    {
        Transform target = DetermineNewCameraPosition(newPosition);
        if (m_ShowAnimations)
        {
            StartCoroutine(MoveToTarget(transitionTime, target));
        }
        else
        {
            transform.position = target.position;
        }
    }

    void SetAnimations(bool isToggleOn)
    {
        m_ShowAnimations = isToggleOn;
    }


    private IEnumerator MoveToTarget(float time, Transform target)
    {
        Vector3 startingPosition = transform.position;
        Vector3 finalPosition = target.position;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPosition, finalPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    Transform DetermineNewCameraPosition(NewPosition newPosition)
    {
        switch (newPosition)
        {
            case NewPosition.Left:
                return m_PredeterminedCameraPositions[0];
            case NewPosition.Right:
                return m_PredeterminedCameraPositions[1];
            case NewPosition.Posterior:
                return m_PredeterminedCameraPositions[3];
            default:
                return m_PredeterminedCameraPositions[2];
        }
    }
    void Start()
    {
        m_Camera = GetComponent<Camera>();
    }
}

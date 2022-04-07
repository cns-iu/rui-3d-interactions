using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAxisLabels : MonoBehaviour
{
    [SerializeField] private float m_HighlightTime = .3f;
    [SerializeField] private SpriteRenderer[] m_SpriteRenderers;
    [SerializeField] private float m_MinAlpha = .3f;
    [SerializeField] private float m_MaxAlpha = 1f;
    // Start is called before the first frame update

    void OnEnable()
    {
        MoveProbingCube.CubeMoveEvent += ShowHide;
    }

    void OnDestroy()
    {
        MoveProbingCube.CubeMoveEvent -= ShowHide;
    }

    void ShowHide(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.W:
                StartCoroutine(Show(m_SpriteRenderers[0]));
                break;
            case KeyCode.S:
                StartCoroutine(Show(m_SpriteRenderers[1]));
                break;
            case KeyCode.D:
                StartCoroutine(Show(m_SpriteRenderers[2]));
                break;
            case KeyCode.A:
                StartCoroutine(Show(m_SpriteRenderers[3]));
                break;
            case KeyCode.E:
                StartCoroutine(Show(m_SpriteRenderers[4]));
                break;
            case KeyCode.Q:
                StartCoroutine(Show(m_SpriteRenderers[5]));
                break;
            default:
                break;
        }
    }

    IEnumerator Show(SpriteRenderer renderer)
    {
        renderer.color = SetAlpha(renderer.color, m_MaxAlpha);
        yield return new WaitForSeconds(m_HighlightTime);
        renderer.color = SetAlpha(renderer.color, m_MinAlpha);
    }

    void Awake()
    {
        m_SpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var item in m_SpriteRenderers)
        {
            item.color = SetAlpha(item.color, m_MinAlpha);
        }
    }

    Color SetAlpha(Color color, float alpha)
    {
        return new Color(
            color.r,
            color.g,
            color.b,
            alpha
        );
    }
}

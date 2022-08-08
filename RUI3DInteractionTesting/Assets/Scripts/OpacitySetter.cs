using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacitySetter : MonoBehaviour
{
    [SerializeField]
    private float alpha;

    void Start()
    {
        AdjustAlpha();
    }

    void AdjustAlpha()
    {
        List<GameObject> list = new List<GameObject>();
        Utils.FindLeaves(this.gameObject.transform, list);
        for (int i = 0; i < list.Count; i++)
        {
            Renderer r = list[i].GetComponent<Renderer>();
            Color newColor = new Color(
                r.material.color.r,
                r.material.color.g,
                r.material.color.b);
            newColor.a = alpha;

            r.material.color = newColor;

            Shader standard;
            standard = Shader.Find("Standard");
            r.material.shader = standard;
            MaterialExtensions.ToFadeMode(r.material);
        }
    }
}

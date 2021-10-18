using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacitySetter : MonoBehaviour
{
    [SerializeField]
    private float alpha = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        AdjustAlpha();
        // Debug.Log(GetComponent<Renderer>().material.color);
    }

    void AdjustAlpha()
    {
        Color color = GetComponent<Renderer>().material.color;
        GetComponent<MeshRenderer>().material.color = new Color(.2f,.2f,.2f, alpha);
        // Color color = GetComponent<Renderer>().material.color;
        // color.a = alpha;
        //     GetComponent<Renderer>().material.color = new Color(color.r,
        //                       color.g,
        //                       color.b,
        //                       alpha);
        // 
    }
}

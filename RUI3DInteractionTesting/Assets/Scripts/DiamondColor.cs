using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Unity.VisualScripting;
using UnityEngine;

public class DiamondColor : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;

    [Header("Diamonds in scene")]
    [SerializeField] private List<DiamondData> _data = new List<DiamondData>();
    [SerializeField] private List<GameObject> gos = new List<GameObject>();
    [SerializeField] private float _minTissueBlocks;
    [SerializeField] private float _maxTissueBlocks;

    [Header("Render")]
    [SerializeField] private int _renderQueue = 3999;
    [SerializeField] private float _maxFOV = .5f;

    private void Awake()
    {
        GetLists();

        _minTissueBlocks = GetRange()[0];
        _maxTissueBlocks = GetRange()[1];
        ColorByTissueBlocks();
    }


    private void Update()
    {
        GetAlphaForDiamonds();
    }

    private void GetAlphaForDiamonds()
    {
        foreach (var g in gos)
        {
            Color adjustedAlpha = g.GetComponent<Renderer>().material.color;
            adjustedAlpha.a = Mathf.Lerp(0, 1, Camera.main.fieldOfView / _maxFOV);
            g.GetComponent<Renderer>().material.color = adjustedAlpha;
            //Debug.Log(Vector3.Distance(Camera.main.transform.position, g.transform.position) / _maxDistance);
        }
    }

    private void ColorByTissueBlocks()
    {
        for (int i = 0; i < gos.Count; i++)
        {
            //color DiamondLocation material and outline, set render queue
            Color color = gos[i].GetComponent<Renderer>().material.color;
            Color newColor = Color.Lerp(_startColor, _endColor, gos[i].GetComponent<DiamondData>().NumberOfTissueBlocks / _maxTissueBlocks);
            newColor.a = gos[i].GetComponent<Renderer>().material.color.a;
            gos[i].GetComponent<Renderer>().material.color = newColor;
            gos[i].GetComponent<Renderer>().material.renderQueue = _renderQueue;
            gos[i].GetComponent<Outline>().OutlineColor = newColor;

            //color connected AS outline
            foreach (var r in gos[i].GetComponent<DiamondData>().ConnectedRenderers)
            {
                Outline line = r.gameObject.AddComponent<Outline>();
                line.OutlineColor = newColor;
                line.OutlineWidth = 5;
            }
        }
    }


    private int[] GetRange()
    {
        List<int> list = new List<int>();

        for (int i = 0; i < _data.Count; i++)
        {
            list.Add(_data[i].NumberOfTissueBlocks);
        }

        return new int[2] { list.Min(), list.Max() };
    }

    private void GetLists()
    {
        _data = FindObjectsOfType<DiamondData>().ToList();

        foreach (var d in _data)
        {
            gos.Add(d.gameObject);
        }
    }
}

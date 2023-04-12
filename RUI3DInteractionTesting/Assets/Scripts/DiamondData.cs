using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondData : MonoBehaviour
{
    [field: SerializeField]
    public int NumberOfTissueBlocks { get; set; }
    [field: SerializeField]
    public int NumberOfCells { get; set; }
    [field: SerializeField]
    public List<Renderer> ConnectedRenderers { get; set; } = new List<Renderer>();
}

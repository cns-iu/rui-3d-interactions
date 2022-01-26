using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnatomicalStructures { LeftVentricle, RightVentricle, LeftAtrium, Septum, AorticValve }
public enum CellTypes { CD8, Epithelial, ABCD25, R2D2, C3PO }
public class TissueBlockData : MonoBehaviour
{
    public List<AnatomicalStructures> m_ListAS = new List<AnatomicalStructures>();
    public List<CellTypes> m_ListCT = new List<CellTypes>();
}

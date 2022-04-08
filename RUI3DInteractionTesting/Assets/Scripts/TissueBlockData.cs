using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnatomicalStructures { LeftVentricle, RightVentricle, LeftAtrium, Septum, AorticValve, MitralValve,PulmonaryValve,RightAtrium,AnteriorPapillaryMuscle,AnterolateralHead}
public enum CellTypes { CD8, Epithelial, ABCD25, R2D2, C3PO, BB8,R5D4,WWW,W3W}
public class TissueBlockData : MonoBehaviour
{
    public List<AnatomicalStructures> m_ListAS = new List<AnatomicalStructures>();
    public List<CellTypes> m_ListCT = new List<CellTypes>();
}

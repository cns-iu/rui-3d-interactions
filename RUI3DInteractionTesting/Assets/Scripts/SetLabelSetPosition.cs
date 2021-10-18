using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLabelSetPosition : MonoBehaviour
{
    public Transform m_TissueBlock;
    // Start is called before the first frame update
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = m_TissueBlock.position;
    }
}

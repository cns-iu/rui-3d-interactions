using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBoxMaker : MonoBehaviour
{
    [SerializeField] private Transform pre_NoundingBox;
    [SerializeField] Transform m_KidneyParentObject;
    [SerializeField] List<Transform> m_AnatomicalStructures;

    void Start()
    {
        FindLeaves(m_KidneyParentObject, m_AnatomicalStructures);
        AddBoundingBoxes(m_AnatomicalStructures);
    }

    void AddBoundingBoxes(List<Transform> transforms)
    {
        for (int i = 0; i < transforms.Count; i++)
        {
            Bounds bounds = transforms[i].GetComponent<Renderer>().bounds;
            Transform boundingBox = Instantiate(pre_NoundingBox);
            boundingBox.position = bounds.center;
            boundingBox.localScale = new Vector3(
                bounds.extents.x * 2,
                bounds.extents.y * 2,
                bounds.extents.z * 2
            );
            boundingBox.parent = transforms[i];
        }
    }

    // Start is called before the first frame update
    void FindLeaves(Transform parent, List<Transform> result)
    {
        if (parent.childCount == 0)
        {
            result.Add(parent.gameObject.transform);
        }
        else
        {
            foreach (Transform child in parent)
            {
                FindLeaves(child, result);
            }
        }
    }
}

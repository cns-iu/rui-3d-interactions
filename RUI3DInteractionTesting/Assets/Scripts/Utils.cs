using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    public static void FindLeaves(Transform parent, List<GameObject> result)
    {
        if (parent.childCount == 0)
        {
            result.Add(parent.gameObject);
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

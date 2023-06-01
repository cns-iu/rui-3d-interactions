using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLocalAxisSwitcher : MonoBehaviour
{
    [SerializeField] private Space _space;
    private void OnEnable()
    {
        InputManager.OnSpaceChange += SetChildren;
    }
    private void OnDestroy()
    {
        InputManager.OnSpaceChange -= SetChildren;
    }

    private void SetChildren(Space newSpace) {
        for (int i = 0; i < transform.childCount; i++) { 
            transform.GetChild(i).gameObject.SetActive(newSpace == _space);
        }
    }
}

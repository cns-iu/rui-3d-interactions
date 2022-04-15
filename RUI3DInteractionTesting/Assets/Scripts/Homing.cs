using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Homing : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private float MovingTime;
    [SerializeField] Toggle HomingOnOff;
    private bool CanClick = true;

    void Start()
    {
        HomingOnOff.onValueChanged.AddListener(delegate
        {
            GetComponent<Homing>().enabled = HomingOnOff.isOn;
        });

        GetComponent<Homing>().enabled = false;
    }

    void Update()
    {
        CheckClick();
    }

    void CheckClick()
    {
        if (Input.GetMouseButtonDown(0) && CanClick && Input.GetKey(KeyCode.LeftShift))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layerMask = 1 << 6;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.gameObject.tag.Equals("TissueBlock"))
                {
                    CanClick = false;
                    StartCoroutine(Home(hit.collider.gameObject.transform.position, MovingTime));
                }
            }
        }
    }

    IEnumerator Home(Vector3 destination, float time)
    {

        Vector3 startingPos = transform.position;
        Vector3 finalPos = destination;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        CanClick = true;
    }
}

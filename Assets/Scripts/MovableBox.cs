using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBox : MonoBehaviour
{

    private Vector3 moveTo;

    private bool beRay = false;
    [SerializeField] Camera _cam;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float _CameraRotationY = Camera.main.transform.localEulerAngles.y;
        print(_CameraRotationY);

        if (Input.GetMouseButtonDown(0))
        {
            RayCheck();
        }

        if (beRay)
        {
            MovePoisition();
        }

        if (Input.GetMouseButtonUp(0))
        {
            beRay = false;
        }

    }

    private void RayCheck()
    {
        Ray ray = new Ray();
        RaycastHit hit = new RaycastHit();
        ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity) && hit.collider == gameObject.GetComponent<Collider>())
        {
            beRay = true;
        }
        else
        {
            beRay = false;
        }

    }

    private void MovePoisition()
    {

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;

        moveTo = _cam.ScreenToWorldPoint(mousePos);
        transform.position = moveTo;

    }
}
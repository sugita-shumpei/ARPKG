using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBox : MonoBehaviour
{

    private bool useGravity = false;
    private bool beRay = false;
    private float depth = 0;
    private float distance = 0;

    private Ray _ray;

    public Camera _cam;

    // Use this for initialization
    void Awake()
    {
        _ray = new Ray();
    }

    private bool isCamLookingX;
    // Update is called once per frame
    void Update()
    {
        float _CameraRotationY = _cam.transform.localEulerAngles.y;
        if( 
            (45 <=_CameraRotationY && _CameraRotationY < 135) ||
            (225 <= _CameraRotationY && _CameraRotationY < 315)
            )
        {
            isCamLookingX = true;

        } else if (
            (135 <= _CameraRotationY && _CameraRotationY < 225) ||
            (315 <= _CameraRotationY && _CameraRotationY <= 360) ||
            (0 <= _CameraRotationY && _CameraRotationY < 45)
            )
        {
            isCamLookingX = false;

        }

        if (Input.GetMouseButtonDown(0))
        {
            RayGetObjectDepth();
        }

        if (beRay)
        {
            MovePoisition(depth);
        }

        if (Input.GetMouseButtonUp(0))
        {
            beRay = false;
        }

        if (Input.GetMouseButton(1))
        {
            gameObject.GetComponent<Rigidbody>().useGravity = !useGravity;

        }

    }

    private void RayGetObjectDepth()
    {
        //print("Position : " + transform.position);

        RaycastHit hit = new RaycastHit();
        _ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray.origin, _ray.direction, out hit, Mathf.Infinity) && hit.collider == gameObject.GetComponent<Collider>())
        {
            beRay = true;
            distance = Vector3.Distance(_cam.transform.position, hit.collider.transform.position);

            if (isCamLookingX)
            {
                depth = transform.position.x;
            }
            else
            {
                depth = transform.position.z;
            }
            
        }
        else
        {
            beRay = false;
        }

    }

    private void MovePoisition(float depth)
    {
        Vector3 mousePos = Input.mousePosition;
        _ray = _cam.ScreenPointToRay(Input.mousePosition);
        Vector3 rayDestination;
        if (isCamLookingX)
        {
            rayDestination = _ray.origin + (depth - _ray.origin.x) / _ray.direction.x * _ray.direction;
        } else
        {
            rayDestination = _ray.origin + (depth - _ray.origin.z) / _ray.direction.z * _ray.direction;
        }
        distance = Vector3.Distance(_cam.transform.position, rayDestination);
        mousePos.z = distance;
        mousePos = _cam.ScreenToWorldPoint(mousePos);
        

        if (isCamLookingX)
        {
            //print("X : " + depth);

            mousePos.x = depth;
        }
        else
        {
            //print("Z : " + depth);

            mousePos.z = depth;
        }
        
        transform.position = mousePos;

    }
}
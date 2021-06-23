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

    private bool isCamLookingX;
    // Update is called once per frame
    void Update()
    {
        float _CameraRotationY = Camera.main.transform.localEulerAngles.y;
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

        float depth = 0;
        if (Input.GetMouseButtonDown(0))
        {
            depth = RayGetObjectDepth();
        }

        if (beRay)
        {
            MovePoisition(depth);
        }

        if (Input.GetMouseButtonUp(0))
        {
            beRay = false;
        }

    }

    private float RayGetObjectDepth()
    {
        Ray ray = new Ray();
        RaycastHit hit = new RaycastHit();
        ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity) && hit.collider == gameObject.GetComponent<Collider>())
        {
            beRay = true;
            if (isCamLookingX)
            {
                print(gameObject.transform.position);
                return gameObject.transform.position.x;
            }
            else
            {

                return gameObject.transform.position.z;
            }
            
        }
        else
        {
            beRay = false;
        }

        return 0;
    }

    private void MovePoisition(float depth)
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition;

        mousePos = _cam.ScreenToWorldPoint(mousePos);
        print(mousePos);
        if (isCamLookingX)
        {
            mousePos.x = depth;
        }
        else
        {
            mousePos.z = depth;
        }
        
        transform.position = moveTo;

    }
}
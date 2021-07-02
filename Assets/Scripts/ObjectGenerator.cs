using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this script to the transform which will compile and manage the objects.
public class ObjectGenerator : MonoBehaviour
{
    private Transform _objParent;
    [SerializeField] private float _objectScaleScalar = 1;
    private Camera _cam;

    public void GenerateObject(GameObject gb)
    {
        _cam = Camera.main;
        _objParent = transform;

        GameObject obj = Instantiate(gb);
        obj.transform.parent = _objParent;
        obj.transform.localPosition = new Vector3(0, 0, 0);

        
        obj.transform.localScale = new Vector3(
            1 * _objectScaleScalar,
            1 * _objectScaleScalar,
            1 * _objectScaleScalar
            );

        Vector3 arBoxScale = _objParent.parent.localScale;
        obj.GetComponent<DrawBounds>()._boxSize = arBoxScale;

        if (obj.GetComponent<Rigidbody>() == null)
        {
            obj.AddComponent<Rigidbody>();
        }
        obj.GetComponent<Rigidbody>().constraints =
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;
        obj.GetComponent<Rigidbody>().useGravity = false;


        obj.GetComponent<MovableBox>()._cam = _cam;

    }

    public void GenerateObject(GameObject gb, Vector3 position, Vector3 scale)
    {
        _cam = Camera.main;
        _objParent = transform;

        GameObject obj = Instantiate(gb);
        obj.transform.parent = _objParent;

        obj.transform.position = position;
        obj.transform.localScale = scale;

        Vector3 arBoxScale = _objParent.parent.localScale;
        obj.GetComponent<DrawBounds>()._boxSize = arBoxScale;

        if (obj.GetComponent<Rigidbody>() == null)
        {
            obj.AddComponent<Rigidbody>();
        }
        obj.GetComponent<Rigidbody>().constraints =
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;
        obj.GetComponent<Rigidbody>().useGravity = false;


        obj.GetComponent<MovableBox>()._cam = _cam;
    }
}

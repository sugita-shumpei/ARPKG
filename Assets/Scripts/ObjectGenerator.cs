using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MaterialChanger))]
public class ObjectGenerator : MonoBehaviour
{
    private Transform _objParent;
    [SerializeField] public float _objectScaleScalar = 1;
    private MaterialChanger _mtChanger;
    private Camera _cam;

    private void Awake()
    {
        _mtChanger = GetComponent<MaterialChanger>();
    }

    public void GenerateObject(GameObject gb)
    {
        _cam = Camera.main;
        _objParent = transform;

        GameObject obj = Instantiate(gb);
        obj.name = gb.name;
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

        _mtChanger.UpdateChangingMaterialObjects();
    }

    public void GenerateObject(GameObject gb, Vector3 position, Vector3 scale)
    {
        _cam = Camera.main;
        _objParent = transform;

        GameObject obj = Instantiate(gb);
        obj.name = gb.name;
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

        _mtChanger.UpdateChangingMaterialObjects();
    }
}

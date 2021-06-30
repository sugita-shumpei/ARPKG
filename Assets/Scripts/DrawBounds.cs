using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBounds : MonoBehaviour
{
    private GameObject baseObject;
    private GameObject newBounds;
    public Vector3 _boxSize = new Vector3(1,1,1);
    private string materialPath = "Materials/RimLightMaterial";

    // Start is called before the first frame update
    void Start()
    {
        if (transform.childCount > 0)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(0).gameObject);
            }

        }

        baseObject = gameObject;
        newBounds = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var boundsCollider = newBounds.GetComponent<Collider>();
        Destroy(boundsCollider);

        newBounds.name = "Visualized AABB";
        newBounds.transform.parent = baseObject.transform;
        newBounds.transform.localScale = baseObject.GetComponent<Collider>().bounds.size;
        newBounds.transform.localScale = new Vector3(
            newBounds.transform.localScale.x / baseObject.transform.localScale.x / _boxSize.x,
            newBounds.transform.localScale.y / baseObject.transform.localScale.y / _boxSize.y,
            newBounds.transform.localScale.z / baseObject.transform.localScale.z / _boxSize.z
            );

        newBounds.transform.localPosition = baseObject.GetComponent<Collider>().bounds.center;
        newBounds.transform.localPosition = new Vector3(
            newBounds.transform.localPosition.x / baseObject.transform.localScale.x / _boxSize.x,
            newBounds.transform.localPosition.y / baseObject.transform.localScale.y / _boxSize.y,
            newBounds.transform.localPosition.z / baseObject.transform.localScale.z / _boxSize.z
            );
        print("CENTER: " + baseObject.GetComponent<Collider>().bounds.center);
        print("POSITION: " + newBounds.transform.position);
        print("LOCALPOSITION: " + newBounds.transform.localPosition);
        //print(Resources.Load<Material>(materialPath));
        Material[] mat = new Material[1] { Resources.Load<Material>(materialPath) };
        newBounds.GetComponent<MeshRenderer>().materials = mat;

    }

}

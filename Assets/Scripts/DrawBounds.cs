using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawBounds : MonoBehaviour
{
    private GameObject baseObject;
    private GameObject newBounds;
    private string materialPath = "Materials/RimLightMaterial";

    // Start is called before the first frame update
    void Start()
    {
        if (transform.childCount > 0) return;
        baseObject = gameObject;
        newBounds = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var boundsCollider = newBounds.GetComponent<Collider>();
        Destroy(boundsCollider);

        newBounds.name = "Visualized AABB";
        newBounds.transform.parent = baseObject.transform;
        newBounds.transform.localScale = baseObject.GetComponent<Collider>().bounds.size;
        newBounds.transform.localScale = new Vector3(
            newBounds.transform.localScale.x / baseObject.transform.localScale.x,
            newBounds.transform.localScale.y / baseObject.transform.localScale.y,
            newBounds.transform.localScale.z / baseObject.transform.localScale.z
            );
        newBounds.transform.position = baseObject.GetComponent<Collider>().bounds.center;
        //print(Resources.Load<Material>(materialPath));
        Material[] mat = new Material[1] { Resources.Load<Material>(materialPath) };
        newBounds.GetComponent<MeshRenderer>().materials = mat;

    }

}

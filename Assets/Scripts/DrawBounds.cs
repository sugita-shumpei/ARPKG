using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBounds : MonoBehaviour
{
    private GameObject newBounds;
    public Vector3 _boxSize = new Vector3(1,1,1);
    private string materialPath = "Materials/RimLightMaterial";

    // Start is called before the first frame update
    void Awake()
    {
        if (transform.childCount > 0)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

        }

        newBounds = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var boundsCollider = newBounds.GetComponent<Collider>();
        Destroy(boundsCollider);

        Transform objManager = transform.parent;
        transform.parent = null;

        newBounds.name = "Visualized AABB";
        newBounds.transform.SetParent(gameObject.transform);


        var center = gameObject.GetComponent<Collider>().bounds.center;
        newBounds.transform.position = center;

        newBounds.transform.localScale = gameObject.GetComponent<Collider>().bounds.size;
        newBounds.transform.localScale = new Vector3(
            newBounds.transform.localScale.x / gameObject.transform.localScale.x,// / _boxSize.x,
            newBounds.transform.localScale.y / gameObject.transform.localScale.y,// / _boxSize.y,
            newBounds.transform.localScale.z / gameObject.transform.localScale.z /// _boxSize.z
            );


        Material[] mat = new Material[1] { Resources.Load<Material>(materialPath) };
        newBounds.GetComponent<MeshRenderer>().materials = mat;

        transform.parent = objManager;

    }

    /*
    private void Update()
    {
        print("CENTER: " + gameObject.GetComponent<Collider>().bounds.center);
    }
    */
}

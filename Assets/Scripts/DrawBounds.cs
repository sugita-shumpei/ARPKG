using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBounds : MonoBehaviour
{
    [SerializeField]
    private GameObject bounds;
    private GameObject newBounds;
    // Start is called before the first frame update
    void Start()
    {
        newBounds = Instantiate(bounds, transform.position, Quaternion.identity) as GameObject;

    }

    // Update is called once per frame
    void Update()
    {
        var col = GetComponent<Collider>();
        if ( col == null)
        {
            newBounds.SetActive(false);
        } else
        {
            newBounds.SetActive(true);
            newBounds.transform.localScale = col.bounds.size;
        }
    }
}

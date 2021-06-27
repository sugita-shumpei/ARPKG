using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnHit : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("VirtualSphere"))
        {
            Destroy(collision.gameObject);

        }
    }
}

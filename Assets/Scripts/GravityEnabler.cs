using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityEnabler : MonoBehaviour
{
    private Rigidbody objRB;

    void Update()
    {
        if (objRB && objRB.useGravity == false)
        {
             objRB.useGravity = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Explosive"))
        {
            objRB = other.gameObject.GetComponent<Rigidbody>();
        }
    }
}

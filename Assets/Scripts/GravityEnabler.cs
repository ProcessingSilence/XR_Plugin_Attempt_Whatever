using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GravityEnabler : MonoBehaviour
{
    private Rigidbody objRB;

    private XRGrabInteractable _xrGrabInteractable;

    void Awake()
    {
        _xrGrabInteractable = GetComponent<XRGrabInteractable>();
    }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEnabler : MonoBehaviour
{
    public Rigidbody BatRB;

    public TrailRenderer trailRenderer;

    public float highestVelocity;
    // Start is called before the first frame update
    void Awake()
    {
        trailRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        trailRenderer.enabled = BatRB.velocity.magnitude > highestVelocity;
    }
}

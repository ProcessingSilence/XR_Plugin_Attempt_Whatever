using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    private bool startProcess;
    private bool startExplosion;
    private bool destroyTrail;

    public GameObject model;

    private SphereCollider _sphereCollider;

    private TrailRenderer _trailRenderer;
    
    private Rigidbody rb;

    private ParticleSystem _particleSystem;
    
    public float explosiveRadius;
    public float force;

    public float[] particleSizeRange;

    public Light explosionLight;


    void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _particleSystem = GetComponent<ParticleSystem>();
        explosionLight.intensity = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startProcess)
        {
            explosionLight.intensity = transform.localScale.x;
        }

        if (startProcess && startExplosion == false)
        {
            startExplosion = true;
            SetValues();
            StartCoroutine(Explosion());
            ParticleExplosion();
        }

        if (destroyTrail)
        {
            SmoothDestroyTrail();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Bat") && startProcess == false)
        {
            startProcess = true;
        }
    }

    IEnumerator Explosion()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosiveRadius);
        foreach (Collider hit in hitColliders)
        {
            var hitRB = hit.GetComponent<Rigidbody>();
            if (hitRB && !hitRB.gameObject.CompareTag("Bat"))
            {
                hitRB.isKinematic = false;
                hitRB.useGravity = true;
                hitRB.AddExplosionForce(force, transform.position, explosiveRadius, 1f, ForceMode.Impulse);
            }
        }
        
        yield return new WaitForSecondsRealtime(3f);
        Destroy(gameObject);
    }

    void SetValues()
    {
        var sizeMultiplier = transform.localScale.x;
        explosiveRadius *= sizeMultiplier;
        force *= sizeMultiplier;
            
        model.SetActive(false);
        
        destroyTrail = true;
        _sphereCollider.enabled = false;
        Destroy(rb);
    }

    void SmoothDestroyTrail()
    {
        float trailStart = _trailRenderer.startWidth;
        _trailRenderer.startWidth = Mathf.Lerp(trailStart, 0, Time.deltaTime * 5);
        explosionLight.intensity = _trailRenderer.startWidth * 50;
    }

    void ParticleExplosion()
    {
        _particleSystem.startSize = Random.Range(particleSizeRange[0] * transform.localScale.x, particleSizeRange[1] * transform.localScale.x);
        var particleSize = _particleSystem.shape;
        particleSize.scale = Vector3.one * transform.localScale.x;
        _particleSystem.Play();
    }
}

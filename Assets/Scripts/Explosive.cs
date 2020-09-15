using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public bool startProcess;
    private bool startExplosion;
    private bool destroyTrail;
    private bool playSoundAgain;

    public GameObject model;

    private SphereCollider _sphereCollider;

    private TrailRenderer _trailRenderer;
    
    private Rigidbody rb;

    private ParticleSystem _particleSystem;
    
    public float explosiveRadius;
    public float force;
    public float fallDestroy;

    public float[] particleSizeRange;

    public Light explosionLight;
    
    public AudioClip explosionSound;
    public AudioClip heavyBatHit;
    public AudioClip batHit;
    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _sphereCollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _particleSystem = GetComponent<ParticleSystem>();
        explosionLight.intensity = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < fallDestroy)
        {
            startProcess = true;
        }

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
        if (other.gameObject.CompareTag("Bat") && playSoundAgain == false)
        {
            playSoundAgain = true;
            if (transform.localScale.x >= 0.7f)
            {
                _audioSource.clip = heavyBatHit;
            }
            else
            {
                _audioSource.clip = batHit;
            }
            _audioSource.Play();
            
        }
        if (!other.gameObject.CompareTag("Bat") && startProcess == false)
        {
            startProcess = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Bat") && playSoundAgain)
        {
            playSoundAgain = false;
        }
    }

    IEnumerator Explosion()
    {
        Destroy(_sphereCollider);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosiveRadius);
        foreach (Collider hit in hitColliders)
        {
            if (!hit.gameObject.CompareTag("GoldenBall") || !hit.gameObject.CompareTag("Bat"))
            {
                var hitRB = hit.GetComponent<Rigidbody>();
                hitRB.isKinematic = false;
                hitRB.useGravity = true;
                var hitCollider = hitRB.GetComponent<BoxCollider>();
                if (hitCollider)
                    hitCollider.enabled = false;
                hitRB.AddExplosionForce(force, transform.position, explosiveRadius, 1f, ForceMode.Impulse);              
            }
        }                 
        _audioSource.clip = explosionSound;
        _audioSource.Play();
        Destroy(rb);       
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

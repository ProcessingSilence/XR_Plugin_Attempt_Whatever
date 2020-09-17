using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BetterBallSpawner : MonoBehaviour
{
    public GameObject ball;
    public Transform centerOfBat;
    private Transform currentBall;

    public Transform spawnPoint;
    
    private TrailRenderer ballTrail;
    
    private XRGrabInteractable _XRGrabInteractable_script;

    public float maxScale;
    
    private bool buttonHeld;

    private SphereCollider ballCollider;
    

    // Start is called before the first frame update
    void Awake()
    {

        _XRGrabInteractable_script = GetComponent<XRGrabInteractable>();
        
        _XRGrabInteractable_script.onActivate.AddListener(SpawnBall);
        _XRGrabInteractable_script.onDeactivate.AddListener(RestartSpawner);
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonHeld)
        {
            var ballScale = currentBall.localScale;
            float distance = Vector3.Distance(currentBall.position, centerOfBat.position);
            currentBall.localScale = (Vector3.one * distance) - new Vector3(0.95f, 0.95f, 0.95f);

            ballCollider.enabled = false;
            ballTrail.startWidth = ballScale.x;
        }
        else if (buttonHeld == false && ballCollider)
        {
            ballCollider.enabled = true;
        }

        if (currentBall)
        {
            // Min
            if (currentBall.localScale.x < 0.01f)
            {
                currentBall.localScale = Vector3.one * 0.01f;
            }
            
            // Max
            if (currentBall.localScale.x > maxScale)
            {
                currentBall.localScale = Vector3.one * maxScale; 
            }
        }

    }

    
    private void SpawnBall(XRBaseInteractor interactable)
    {
        buttonHeld = true;
        currentBall = Instantiate(ball, spawnPoint.position, Quaternion.identity).transform;
        ballCollider = currentBall.GetComponent<SphereCollider>();
        ballCollider.enabled = false;
        ballTrail = currentBall.gameObject.GetComponent<TrailRenderer>();
    }

    private void RestartSpawner(XRBaseInteractor interactable)
    {
        buttonHeld = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BetterBallSpawner : MonoBehaviour
{
    public GameObject ball;
    private Transform currentBall;

    public Transform spawnPoint;
    
    private TrailRenderer ballTrail;
    
    private XRGrabInteractable _XRGrabInteractable_script;

    public float maxScale;
    
    private bool buttonHeld;
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
            float distance = Vector3.Distance(currentBall.position, transform.position);
            currentBall.localScale = (Vector3.one * distance) - new Vector3(0.95f, 0.95f, 0.95f);

            ballTrail.startWidth = ballScale.x;
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
        ballTrail = currentBall.gameObject.GetComponent<TrailRenderer>();
    }

    private void RestartSpawner(XRBaseInteractor interactable)
    {
        buttonHeld = false;
    }
}

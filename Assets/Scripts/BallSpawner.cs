using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallSpawner : MonoBehaviour
{
    public GameObject ball;
    private GameObject currentBall;
    
    private XRGrabInteractable _XRGrabInteractable_script;

    public float maxScale;
    
    private bool buttonHeld;
    private bool startedCoroutine;
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
        if (buttonHeld && startedCoroutine == false && currentBall.transform.localScale.x <= maxScale)
        {
            startedCoroutine = true;
        }

        if (buttonHeld)
        {
            var ballScale = currentBall.transform.localScale;
            currentBall.transform.localScale = Vector3.Lerp(ballScale, ballScale + Vector3.one * maxScale/5, Time.deltaTime);
        }
        
        if (currentBall.transform.localScale.x > maxScale)
        {
            currentBall.transform.localScale = Vector3.one * maxScale; 
        }
    }

    
    private void SpawnBall(XRBaseInteractor interactable)
    {
        buttonHeld = true;
        currentBall = Instantiate(ball, transform.position, Quaternion.identity);
    }

    private void RestartSpawner(XRBaseInteractor interactable)
    {
        buttonHeld = false;
        startedCoroutine = false;
    }
}

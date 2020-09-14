using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PedestalMove : MonoBehaviour
{
    public XRSocketInteractor XrSocketInteractor_script;

    public float speed;
    public float yDestroyZone;
    
    private bool alreadyMovingDown;

    // Activated via XRSocketInteractor.cs 
    public void StartMovingDown()
    {
        if (alreadyMovingDown == false)
            StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        if (alreadyMovingDown == false)
        {
            alreadyMovingDown = true;
            XrSocketInteractor_script.enabled = false;
            yield return new WaitForSecondsRealtime(0.5f);
            StartCoroutine(MovingDown());
        }
    }
    
    IEnumerator MovingDown()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        transform.Translate(0,speed,0);
        if (transform.position.y < yDestroyZone)
        {
            Destroy(gameObject);
        }
        StartCoroutine(MovingDown());
    }
}

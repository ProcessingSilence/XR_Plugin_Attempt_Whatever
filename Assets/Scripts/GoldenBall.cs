using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenBall : MonoBehaviour
{
    private Timer Timer_script;


    private void Awake()
    {
        Timer_script = GameObject.FindWithTag("Timer").GetComponent<Timer>();
    }

    void Update()
    {
        if (Timer_script.start > 0)
        {
            Destroy(gameObject.GetComponent<GoldenBall>());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bat") && Timer_script.start < 1)
        {
            Debug.Log("GOLDEN BALL HIT");
            Timer_script.start = 1;
        }
    }
}

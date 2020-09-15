using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenBall : MonoBehaviour
{
    private Timer Timer_script;
    public float distance;

    private void Awake()
    {
        Timer_script = GameObject.FindWithTag("Timer").GetComponent<Timer>();
    }

    void Update()
    {
        if (Timer_script.start > 0)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            Destroy(gameObject.GetComponent<GoldenBall>());
        }

        distance = Vector3.Distance(Vector3.zero, transform.position);
        if (distance > 20)
        {
            Destroy(gameObject);
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

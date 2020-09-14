using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenBall : MonoBehaviour
{
    private Instructions Instructions_script;


    private void Awake()
    {
        Instructions_script = GameObject.FindWithTag("Instructions").GetComponent<Instructions>();
    }

    void Update()
    {
        if (Instructions_script.countdownFlag > 0)
        {
            Destroy(gameObject.GetComponent<GoldenBall>());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bat") && Instructions_script.countdownFlag < 1)
        {
            Debug.Log("GOLDEN BALL HIT");
            Instructions_script.countdownFlag = 1;
        }
    }
}

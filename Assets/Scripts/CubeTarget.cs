using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTarget : MonoBehaviour
{
    private bool sentScore;

    public int addAmount;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(DestroyOnYPos());
    }

    IEnumerator DestroyOnYPos()
    {
        if (transform.position.y < -10)
        {
            GameObject.FindWithTag("GameStats").gameObject.GetComponent<GameStats>().boxesDestroyed += addAmount;
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(DestroyOnYPos());
    }
}

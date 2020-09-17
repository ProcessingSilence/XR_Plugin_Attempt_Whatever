using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCounter : MonoBehaviour
{
    private GameStats _GameStats_script;
    // Start is called before the first frame update
    void Start()
    {
        _GameStats_script = GetComponent<GameStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            Destroy(other);
            _GameStats_script.boxesDestroyed += 1;
        }
    }
}

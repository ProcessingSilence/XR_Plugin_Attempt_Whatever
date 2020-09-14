using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Get distance from Vector3.zero to gameObject pos, send to GameStats.cs, then destroy self.

public class BallPosition : MonoBehaviour
{
    private bool _startedGettingPos;
    private bool _sentBallStats;
    private GameStats GameStats_script;

    public GameObject model;

    void Update()
    {
        if (model.activeSelf == false && _startedGettingPos == false)
        {
            _startedGettingPos = true;
            GameStats_script = GameObject.FindWithTag("GameStats").gameObject.GetComponent<GameStats>();
            var myPos = transform.position;
            myPos.y = 0;
            
            var vectorCalc = Vector3.Distance(Vector3.zero, myPos);
            
            if (vectorCalc > GameStats_script.farthestBallDist)
            {
                GameStats_script.farthestBallDist = vectorCalc;
                GameStats_script.changeBallText = true;
            }

            _sentBallStats = true;
        }
    }

    private void LateUpdate()
    {
        if (_sentBallStats )
        {
            gameObject.GetComponent<BallPosition>().enabled = false;
        }
    }
}

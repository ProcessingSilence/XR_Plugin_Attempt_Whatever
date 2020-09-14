﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour
{
    public TextMesh thardestSwing;
    public float hardestSwing;

    public TextMesh tfarthestBallDist;
    public float farthestBallDist;
    [HideInInspector] public bool changeBallText;
    
    public Rigidbody BatRB;


    // Start is called before the first frame update
    void Awake()
    {
        thardestSwing.text = "Hardest bat swing: " + hardestSwing;
        tfarthestBallDist.text = "Farthest ball: " + farthestBallDist;
    }

    // Update is called once per frame
    void Update()
    {
        HardestSwing();
    }

    void HardestSwing()
    {
        var currentVelocity = BatRB.velocity.magnitude;
        if (currentVelocity > hardestSwing)
        {
            hardestSwing = currentVelocity;
            thardestSwing.text = "Hardest bat swing: " + hardestSwing;
        }

        if (changeBallText)
        {
            changeBallText = false;
            tfarthestBallDist.text = "Farthest ball: " + farthestBallDist;
        }
    }
}

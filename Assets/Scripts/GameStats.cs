using System.Collections;
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

    public TextMesh tboxesDestroyed;
    public int boxesDestroyed;

    public TextMesh thighestScore;
    public int highestScore;
    
    public Rigidbody BatRB;

    // Start is called before the first frame update
    void Awake()
    {
        thardestSwing.text = "Hardest bat swing: " + hardestSwing;
        tfarthestBallDist.text = "Farthest ball: " + farthestBallDist;
        thighestScore.text = "Highest score: " + highestScore;
    }

    // Update is called once per frame
    void Update()
    {
        HardestSwing();
        FarthestBall();
        BoxesDestroyed();
        HighScore();
    }

    void HardestSwing()
    {
        var currentVelocity = BatRB.velocity.magnitude;
        if (currentVelocity > hardestSwing)
        {
            hardestSwing = currentVelocity;
            thardestSwing.text = "Hardest paddle swing: " + hardestSwing;
        }
    }

    void FarthestBall()
    {
        if (changeBallText)
        {
            changeBallText = false;
            tfarthestBallDist.text = "Farthest ball: " + farthestBallDist;
        }
    }

    void BoxesDestroyed()
    {
        tboxesDestroyed.text = "Boxes destroyed: " + boxesDestroyed;
    }

    void HighScore()
    {
        if (highestScore < boxesDestroyed)
        {
            highestScore = boxesDestroyed;
            thighestScore.text = "Highest score: " + highestScore;
        }
    }
}

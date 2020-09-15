using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMesh[] instructions;
    public TextMesh ttimer;

    private string stringText;
    
    [HideInInspector] 
    public int start;
    private int timer;
    public int totalTime;
    
    public bool reset;

    public GameObject mainCubeCollection;
    private GameObject currentCubeCollection;

    private string[] countdownText = new string[4] {"3", "2", "1", "GO"};

    private string[] tryAgain = new string[3] {"TIME'S UP", "FINAL SCORE: ","Smack the golden ball to play again"};

    public GameObject goldenBall;
    // Update is called once per frame
    void Update()
    {
        if (start == 1)
        {
            start = 2;
            timer = totalTime;
            StartCoroutine(threeTwoOneGo());
        }

        if (timer < 10)
        {
            stringText = "time: 0";
        }
        else
        {
            stringText = "time: ";
        }

        ttimer.text = stringText + timer;
    }
    
    void BlankText(bool increaseOrEqual, float inputNum)
    {
        for (int i = 0; i < 3; i++)
        {
            if (increaseOrEqual)
                instructions[i].transform.localScale += Vector3.one * inputNum;
            else
                instructions[i].transform.localScale = Vector3.one * inputNum;
            instructions[i].text = "";
        }
    }

    IEnumerator CountDownTimer()
    {
        for (int t = totalTime; t != -1; t-- )
        {
            yield return new WaitForSecondsRealtime(1);
            timer = t;
        }

        if (timer <= 0)
        {
            Destroy(currentCubeCollection);
            start = 0;
            BlankText(true, 1);
            instructions[0].text = tryAgain[0];
            instructions[1].text =
                tryAgain[1] + GameObject.FindWithTag("GameStats").GetComponent<GameStats>().boxesDestroyed;
            instructions[2].text = tryAgain[2];
            yield return new WaitForSecondsRealtime(1);
            Instantiate(goldenBall, new Vector3(1.25f, 1, 0), Quaternion.identity);
        }


    }

    IEnumerator threeTwoOneGo()
    { 
        BlankText(false, 1);
        yield return new WaitForSecondsRealtime(1);
        for (int j = 0; j < 3; j++)
        {
            instructions[j].text = countdownText[j];
            yield return new WaitForSecondsRealtime(1);
        }
        BlankText(false, 1);
        currentCubeCollection = Instantiate(mainCubeCollection, Vector3.zero, Quaternion.identity);
        instructions[1].text = "GO";
        StartCoroutine(CountDownTimer());
        yield return new WaitForSecondsRealtime(2);
        BlankText(false, 1);
    }
}

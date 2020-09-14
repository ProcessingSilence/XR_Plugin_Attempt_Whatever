using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public TextMesh[] instructions;

    [HideInInspector]
    public int countdownFlag;  
    
    private string[] instructionsText = new string [6] 
    {
        "Hold down trigger to generate a bomb, then whack the bomb with the bat.",
        "Destroy as many cubes as you can before time runs out.",
        "Hit the golden ball behind you to begin.",
        
        "3",
        "2",
        "1",      
    };
    // Start is called before the first frame update
    void Start()
    {
        instructions[0].text = instructionsText[0];
        instructions[1].text = instructionsText[1];
        instructions[2].text = instructionsText[2];
        
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownFlag == 1)
        {
            countdownFlag = 2;
            StartCoroutine(CountdownTimer());
        }
    }

    IEnumerator CountdownTimer()
    {
        DeleteText();
        
        yield return new WaitForSecondsRealtime(1);
        for (int j = 3; j < 6; j++)
        {
            instructions[j-3].text = instructionsText[j];
            yield return new WaitForSecondsRealtime(1);
        }
        DeleteText();
        instructions[1].text = "GO";
        yield return new WaitForSecondsRealtime(1);
        DeleteText();
    }

    void DeleteText()
    {
        for (int i = 0; i < 3; i++)
        {
            instructions[i].transform.localScale += Vector3.one;
            instructions[i].text = "";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{

    [SerializeField] TMP_Text introStoryText;
    [SerializeField] Variable_Tracker variableTracker;

    // Start is called before the first frame update
    void Start()
    {
        InitialStory(introStoryText);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void InitialStory(TMP_Text textObj)
    {
        Debug.Log("Time scale: " + Time.timeScale);
        string intro = "Welcome! You are the CEO and founder of Selene Dynamics, a space exploration corporation. You and 9 other crewmates have just launched your flagship mission to colonize the moon and mine its lucrative resources. Try to make money and survive. Good luck!";
        textObj.text = intro;
    }

    //Each funtion is called in unity; whenever the user continues to the next "stage" they call the next function and disable the current panel.
    public void Greeting1(TMP_Text greetingText)
    {
        int curLine = 1;

        string ln1 = "Welcome to the moon! I’m Gerald, and I’ll be your \"tour guide\"";
        string ln2 = "I’ve been to the moon a few times for testing, so trust me, I know what I’m doing.";


        if (curLine == 1)
        {
            greetingText.text = ln1;
        }
        else if (curLine == 2)
        {
            greetingText.text = ln2;
        }


    }



    public void StopTime()
    {
        variableTracker.speed = 0;
        Time.timeScale = 0;
    }

    public void StartTime()
    {
        variableTracker.speed = 1;
        Time.timeScale = 1;
    }

}
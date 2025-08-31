using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{

    [SerializeField] TMP_Text introStoryText;
    [SerializeField] TMP_Text mainTextObject;
    [SerializeField] Variable_Tracker variableTracker;
    [SerializeField] int curLine = 0; //The line that the user is on

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
        string intro = "Welcome! You are the CEO and founder of Selene Dynamics, a space exploration corporation. You and 9 other crewmates have just launched your flagship mission to colonize the moon and mine its lucrative resources. Try to make money and survive. Good luck!";
        textObj.text = intro;
    }

    //This funtion is called in unity whenever the user continues to the next line. It updates the text to the current index.
    public void UpdateText(int lineIndex)
    {
        mainTextObject.text = GetLine(lineIndex);
    }

    //Returns the line based on the index
    string GetLine(int index)
    {
        string[] lines = {
            /* ln0 */ "Welcome to the moon! I’m Gerald, and I’ll be your \"tour guide.\"",
            /* ln1 */ "I’ve been to the moon a few times for testing, so trust me, I know what I’m doing.",
            /* ln2 */ "Firstly, see these meters up here? These keep track of your water, food, and air supplies.",
            /* ln3 */ "If you let any of these get to zero, then we’ll have to shut down the mission, and that’s no good, is it?",
            /* ln4 */ "This one at the top shows your population. Each of those people will have a job to keep the mission running smoothly.",
            /* ln5 */ "Don't let that get too low, but keep in mind, more people means they need more food and water and all that jazz.",
            /* ln6 */ "This meter displays your energy levels. More on energy later.",
            /* ln7 */ "But enough of that boring stuff, keep your eyes on the prize!",
            /* ln8 */ "Here shows how much money you have. You can make money by exporting all of our goodies.",
            /* ln9 */ "All we are able to mine for now is regolith, which is pretty much just crusty moon rock.",
            /* ln10 */ "But we’ll be able to expand to export more kinds of materials.",
            /* ln11 */ "Speaking of regolith, this meter shows how much of it you’ve got.",
            /* ln12 */ "Now this is where you come in. Click that button that says \"Shop.\"",
            /* ln13 */ "Your main job is to manage these buildings.",
            /* ln14 */ "As you can see, for now you can place domes, hydroponic plant nurseries, solar panels, drills, and O2 plants.",
            /* ln15 */ "Try clicking on that little i button to learn more about each of the buildings."
        };


        if (index < 0 || index >= lines.Length) //if the index is invalid
        {
            Debug.LogWarning("The index for the tutorial lines is out of bounds.");
            return "";
        }
        else
        {
            return lines[index];
        }
    }


    public void IncrementCurrentLine()
    {
        Debug.Log("Incrementing Line");
        curLine += 1;
        UpdateText(curLine);
    }

    public void DecrementCurrentLine()
    {
        curLine -= 1;
        UpdateText(curLine);
    }



    //Obsolete
    /*public void StopTime()
    {
        variableTracker.speed = 0;
        Time.timeScale = 0;
    }

    public void StartTime()
    {
        variableTracker.speed = 1;
        Time.timeScale = 1;
    } */

}
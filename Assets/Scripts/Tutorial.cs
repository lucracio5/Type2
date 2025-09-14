using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [Header("Canvas Stuff")]
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject GeraldGameObject;
    [SerializeField] TMP_Text introStoryText;
    [SerializeField] TMP_Text mainTextObject;
    [SerializeField] Variable_Tracker variableTracker;
    [SerializeField] int curLine = 0; //The line that the user is on

    [Header("Misc.")]
    [SerializeField] Camera FlyDownCamera;
    [SerializeField] Imports2 imports2;

    [Header("Gerald Sprites")]
    [SerializeField] Sprite GeraldMouthOpen;
    [SerializeField] Sprite GeraldMouthClosed;
    [SerializeField] Sprite GeraldPointDownLeft;
    [SerializeField] Sprite GeraldPointDownRight;
    [SerializeField] Sprite GeraldPointUpLeft;
    [SerializeField] Sprite GeraldPointUpRight;


    private string[] lines = {
            /* ln0 */ "Welcome to the moon! I’m Gerald, and I’ll be your \"tour guide.\"",
            /* ln1 */ "I’ve been to the moon a few times for testing, so trust me, I know what I’m doing.",
            
            /* ln2 */ "Firstly, see these meters up here? These keep track of your water, food, and air supplies.",
            /* ln3 */ "If you let any of these get to zero, then we’ll have to shut down the mission, and that’s no good, is it?",
            
            /* ln4 */ "This one at the top shows your population. Each of those people will have a job to keep the mission running smoothly.",
            /* ln5 */ "Don't let that get too low, but keep in mind, more people means they need more food and water and all that jazz.",
            
            /* ln6 */ "This meter displays your energy levels. More on energy later.",
            /* ln7 */ "But enough of that boring stuff, keep your eyes on the prize!",
            /* ln8 */ "Here shows how much money you have. You can make money by exporting all of our goodies.",
            /* ln9 */ "The first thing we are able to mine is regolith, which is pretty much just crusty moon rock. It goes for about 20 dollars",
            /* ln10 */ "Speaking of regolith, this meter shows how much of it you’ve got.",
            /* ln11 */ "Speaking of regolith, this meter shows how much of it you’ve got.",
            /* ln12 */ "Now this is where you come in. Click that button that says \"Shop.\"",
            /* ln13 */ "Your main job is to manage these buildings.",
            /* ln14 */ "As you can see, for now you can place domes, hydroponic plant nurseries, solar panels, drills, and O2 plants.",
            /* ln15 */ "Try clicking on that little i button to learn more about each of the buildings."
    };


    // Start is called before the first frame update
    void Start()
    {
        //check if the tutorial has been seen before
        //if (!PlayerPrefs.HasKey("TutorialSeen") || PlayerPrefs.GetInt("TutorialSeen") == 0)
        //{
            InitialStory(introStoryText);
            UpdateText(curLine);
            PlayerPrefs.SetInt("TutorialSeen", 1); //mark tutorial as seen
            PlayerPrefs.Save();
            Debug.Log("Tutorial started for the first time.");
        //}
        //else
        //{
        //    Debug.Log("Tutorial already seen, skipping initial story.");
        //}
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

        if (lineIndex <= 1) MoveGerald(130, -80, GeraldMouthOpen); //standard position
        else if (lineIndex == 2) MoveGerald(130, -80, GeraldPointUpRight); //food, water, air meters
        else if (lineIndex == 4) MoveGerald(130, 40, GeraldPointUpRight); //population meter
        else if (lineIndex == 6) MoveGerald(130, 14, GeraldPointUpRight); //energy
        else if (lineIndex == 7) MoveGerald(130, -80, GeraldMouthOpen); //enough of that jazz
        else if (lineIndex == 8) MoveGerald(-260, 15, GeraldPointUpLeft); //money

        //else if (lineIndex == )

    }

    //Returns the line based on the index
    string GetLine(int index)
    {
        if (index < 0) return lines[0]; //if the index is too small, return the first line
        else if (index >= lines.Length) return lines[lines.Length - 1]; //if the index is too big, return the last line
        else return lines[index]; //if it is good, return the correct one
    }

    //What a beutiful, consise function. I'm proud of this one. Ts used to be 2 functions and like 8 lines of code
    public void ChangeCurLine(bool increment)
    {
        curLine = Mathf.Clamp(increment ? curLine + 1 : curLine - 1, 0, lines.Length - 1); //removes or adds 1, clamped between 0 and the amount of lines
        UpdateText(curLine);
    }

    void MoveGerald(float x, float y, Sprite GeraldSprite)
    {
        GeraldGameObject.GetComponent<Image>().sprite = GeraldSprite; //Updates geralds sprite to what it should be
        GeraldGameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y); //moves gerald to the desired position
        Debug.Log("Moving Gerald to " + x + ", " + y + " in the position " + GeraldSprite.name);
        Debug.Log("Gerald position: " + GeraldGameObject.GetComponent<RectTransform>().anchoredPosition);
        Debug.Log("Gerald image: " + GeraldGameObject.GetComponent<Image>().sprite.name);
    }


    //is called when the user clicks continue on the initial screen
    public void RunSickCameraSequence()
    {
        FlyDownCamera.enabled = true;
        Canvas.SetActive(false); //hide ui

        imports2.Arrive(); //have the rocket come down

        Invoke("DisableFlyDownCameraAndEnableCanvas", 8f);
    }

    //goofy ahh function
    void DisableFlyDownCameraAndEnableCanvas()
    {
        FlyDownCamera.enabled = false;
        Canvas.SetActive(true);
        GeraldGameObject.SetActive(true);
    }
}
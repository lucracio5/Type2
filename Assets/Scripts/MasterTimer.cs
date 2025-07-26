using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script keeps track of the total time played in the game (In real time)

public class MasterTimer : MonoBehaviour
{
    public Variable_Tracker variable_Tracker;
    public float masterTime;
    private int lastMinute = -1; // Track the last minute we triggered
    public bool autosave; //Turns on or off the autosave for working purposes MAKE SURE THIS IS TRUE BEFORE PUBLISH

    // Start is called before the first frame update
    void Start()
    {
        masterTime = variable_Tracker.masterTime; // Initialize masterTime from Variable_Tracker
    }

    // Update is called once per frame
    void Update()
    {
        masterTime += Time.deltaTime;
        variable_Tracker.masterTime = masterTime; // Update the master time in Variable_Tracker

        int currentMinute = Mathf.FloorToInt(masterTime / 60f);

        if (currentMinute > lastMinute) //if a minute has passed
        {
            if(autosave)
            {
                variable_Tracker.Save_button(); //auto-save the game
            }
            lastMinute = currentMinute;
        }

    }
}

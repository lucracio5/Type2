using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script keeps track of the total time played in the game (In real time)

public class MasterTimer : MonoBehaviour
{
    public Variable_Tracker variable_Tracker;
    public float masterTime;
    private int lastMinute = -1; // Track the last minute we triggered

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        masterTime += Time.deltaTime;

        int currentMinute = Mathf.FloorToInt(masterTime / 60f);

        if (currentMinute > lastMinute) //if a minute has passed
        {
            variable_Tracker.Save_button(); //auto-save the game
            lastMinute = currentMinute;
        }

    }
}

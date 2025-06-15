using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_script: MonoBehaviour
{
    public GameObject Gamemanager;
    private float timer;
    public int total_collected;
    void Start()
    {
        Gamemanager = GameObject.Find("Game Manager");
        this.transform.eulerAngles = new Vector3(-90, -0, 0);

    }

    // Update is called once per frame
    public int return_total()
    {
        return total_collected;
    }
    void Update()
    {
        timer += Time.deltaTime * Gamemanager.GetComponent<Variable_Tracker>().speed;
        if (GetComponent<Transparency>().placed && timer > 10)
        {
            if (Gamemanager.GetComponent<Variable_Tracker>().energy > 50 && Gamemanager.GetComponent<Variable_Tracker>().max_mining > Gamemanager.GetComponent<Variable_Tracker>().regolith)
            {
                Gamemanager.GetComponent<Variable_Tracker>().energy -= 50;

                Gamemanager.GetComponent<Variable_Tracker>().regolith += 1;
                total_collected++;
                //Debug.Log(total_collected);
            }
            timer = 0f;
            
        }

    }
}

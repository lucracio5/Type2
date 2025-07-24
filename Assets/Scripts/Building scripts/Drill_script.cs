using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
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
        if (GetComponent<Transparency>().placed && timer > 1)
        {
            int num = Random.Range(0, 100); //Random chance to get materials


            if(num <= 10) //15% change
            {
                if (Gamemanager.GetComponent<Variable_Tracker>().energy > 30 && Gamemanager.GetComponent<Variable_Tracker>().max_uranium > Gamemanager.GetComponent<Variable_Tracker>().uranium)//checks if you have space
                {
                    Gamemanager.GetComponent<Variable_Tracker>().energy -= 50;
                    Gamemanager.GetComponent<Variable_Tracker>().uranium += 1;
                    total_collected++;
                }
            }
            if(num == 11)//1% chance
            {
                if (Gamemanager.GetComponent<Variable_Tracker>().energy > 30 && Gamemanager.GetComponent<Variable_Tracker>().max_lithium > Gamemanager.GetComponent<Variable_Tracker>().lithium)//checks if you have space
                {
                    Gamemanager.GetComponent<Variable_Tracker>().energy -= 50;
                    Gamemanager.GetComponent<Variable_Tracker>().lithium += 1;
                    total_collected++;
                }
            }
            timer = 0f;
            
        }

    }
}

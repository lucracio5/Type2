using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_script: MonoBehaviour
{
    private float time;
    public float transparency = 0.2f;
    public GameObject Gamemanager;
    public bool loaded = false;
    private float timer2;
    public int total_collected;
    void Start()
    {
        Gamemanager = GameObject.Find("Game Manager");
        this.GetComponent<MeshRenderer>().material.color = new Color(transparency, transparency, transparency, transparency);
        this.transform.eulerAngles = new Vector3(-90, -0, 0);

    }

    // Update is called once per frame
    public int return_total()
    {
        return total_collected;
    }
    void Update()
    {
        time += Time.deltaTime * Gamemanager.GetComponent<Variable_Tracker>().speed;
        if (time > 0.5)
        {
            if (transparency < 1)
            {
                transparency = transparency + 0.1f;
                this.GetComponent<MeshRenderer>().material.color = new Color(transparency, transparency, transparency, transparency);
                loaded = true;
                

            }
            
            time = 0f;

        }
        
        timer2 += Time.deltaTime * Gamemanager.GetComponent<Variable_Tracker>().speed;
        if (loaded && timer2 > 10)
        {
            if (Gamemanager.GetComponent<Variable_Tracker>().energy > 50 && Gamemanager.GetComponent<Variable_Tracker>().max_mining > Gamemanager.GetComponent<Variable_Tracker>().regolith)
            {
                Gamemanager.GetComponent<Variable_Tracker>().energy -= 50;
                Gamemanager.GetComponent<Variable_Tracker>().regolith += 1;
                total_collected++;
                Debug.Log(total_collected);


            }
            timer2 = 0f;
            
        }

    }
}

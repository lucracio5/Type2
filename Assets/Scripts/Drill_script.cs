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
    void Start()
    {
        Gamemanager = GameObject.Find("Game Manager");
        this.GetComponent<MeshRenderer>().material.color = new Color(transparency, transparency, transparency, transparency);
        this.transform.eulerAngles = new Vector3(-90, -0, 0);

    }

    // Update is called once per frame
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
        Debug.Log(timer2);
        timer2 += Time.deltaTime * Gamemanager.GetComponent<Variable_Tracker>().speed;
        if (loaded && timer2 > 10)
        {
            if (Gamemanager.GetComponent<Variable_Tracker>().Energy > 50)
            {
                Gamemanager.GetComponent<Variable_Tracker>().Energy -= 50;
                Gamemanager.GetComponent<Variable_Tracker>().Regolith += 1;
                Debug.Log("test");
            }
            timer2 = 0f;
            Debug.Log("Test2"); 
        }

    }
}

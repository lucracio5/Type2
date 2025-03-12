using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solar_Pannels: MonoBehaviour
{
    private float time;
    public float transparency = 0.2f;
    public GameObject Gamemanager;

    void Start()
    {
        this.GetComponent<MeshRenderer>().material.color = new Color(transparency, transparency, transparency, transparency);
        Gamemanager = GameObject.Find("Game Manager");
        //Energy = Gamemanager.GetComponent<Variable_Tracker>().Energy;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime* Gamemanager.GetComponent<Variable_Tracker>().speed;
        if (time > 1)
        {
            if (transparency < 1)
            {
                transparency += 0.1f;
            }
            else
            {
                Gamemanager.GetComponent<Variable_Tracker>().Energy += 1;
            }
            this.GetComponent<MeshRenderer>().material.color = new Color(transparency, transparency, transparency, transparency);
            time = 0;
            

        }

    }
}

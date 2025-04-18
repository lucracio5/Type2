using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Solar_Pannels: MonoBehaviour
{
    private float time;
    public float transparency = 0.2f;
    public GameObject Gamemanager;
    public int total_collected;
    public bool placed;

    void Start()
    {
        this.GetComponent<MeshRenderer>().material.color = new Color(transparency, transparency, transparency, transparency);
        Gamemanager = GameObject.Find("Game Manager");
        //Energy = Gamemanager.GetComponent<Variable_Tracker>().Energy;
        placed = false;
    }

    // Update is called once per frame

    public int return_total()
    {
        Debug.Log("return" + total_collected.ToString());
        return total_collected;
        
    }


    void Update()
    {
        time += Time.deltaTime* Gamemanager.GetComponent<Variable_Tracker>().speed;
        if (time > 1)
        {
            if (transparency < 1)
            {
                transparency += 0.1f;
            }
            else if (Gamemanager.GetComponent<Variable_Tracker>().energy < Gamemanager.GetComponent<Variable_Tracker>().max_energy)
            {
                Gamemanager.GetComponent<Variable_Tracker>().energy += 1;
                total_collected= total_collected + 1;
                placed = true;

            }
            this.GetComponent<MeshRenderer>().material.color = new Color(transparency, transparency, transparency, transparency);
            time = 0;

            

        }

    }
}

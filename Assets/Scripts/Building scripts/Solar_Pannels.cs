using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Solar_Pannels: MonoBehaviour
{
    private float time;
    public GameObject Gamemanager;
    public int total_collected;

    void Start()
    {
        Gamemanager = GameObject.Find("Game Manager");
        //Energy = Gamemanager.GetComponent<Variable_Tracker>().Energy;
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
            if (Gamemanager.GetComponent<Variable_Tracker>().energy < Gamemanager.GetComponent<Variable_Tracker>().max_energy && GetComponent<Transparency>().placed)
            {
                Gamemanager.GetComponent<Variable_Tracker>().energy += 1;
                total_collected= total_collected + 1;
            }
            time = 0;

            

        }

    }
}

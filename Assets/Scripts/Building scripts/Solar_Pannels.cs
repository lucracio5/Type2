using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Solar_Pannels: MonoBehaviour
{
    private float time;
    public GameObject Gamemanager;
    public int total_collected;
    public int dirt_level;
    public int dirt_chance;
    public TMP_Text Solartext1;
    public TMP_Text Solartext2;

    
    void Start()
    {
        Gamemanager = GameObject.Find("Game Manager");
        dirt_chance = 1;
        Solartext1 = Gamemanager.GetComponent<Variable_Tracker>().returnSolar1();
        Solartext2 = Gamemanager.GetComponent<Variable_Tracker>().returnSolar2();

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
                int num = Random.Range(0, 1001);

                if (num > dirt_level)
                {
                    Gamemanager.GetComponent<Variable_Tracker>().energy += 1;
                    total_collected = total_collected + 1;
                }
                if (num > dirt_chance && dirt_level < 1000)
                {
                    dirt_level += 20;
                }

                time = 0;



            }
        }
        Solartext1.text = dirt_level_return();
        Solartext2.text = "total colected ="+total_collected.ToString();
    }
    public void clean()
    {
        dirt_level = 0;
    }
    public string dirt_level_return()
    {
       
        if(0 <= dirt_level && dirt_level <= 250)
        {
            return "clean";
        }
        else if (250 < dirt_level && dirt_level <= 500)
        {
            return "mostly dirty";
        }
        else if (500 < dirt_level && dirt_level <= 750)
        {
            return "dirty";
        }
        else
        {
            return "filthy";
        }
    }
}

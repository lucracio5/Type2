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
    public int collected_amount = 1;
    public bool lvl2 = false;
    public bool lvl3 = false;

    
    void Start()
    {
        Gamemanager = GameObject.Find("Game Manager");
        dirt_chance = 1;


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
                    Gamemanager.GetComponent<Variable_Tracker>().energy += collected_amount;
                    total_collected = total_collected + collected_amount;
                }
                if (num > dirt_chance && dirt_level < 1000)
                {
                    dirt_level += 20;
                }

                time = 0;



            }
        }

    }
    public void Level2Upgrade()
    {
        collected_amount = 2;
        lvl2 = true;
        Gamemanager.GetComponent<Audio_manager>().PlayUnlock();
    }
    public void Level3Upgrade()
    {
        collected_amount = 4;
        lvl3 = true;
        Gamemanager.GetComponent<Audio_manager>().PlayUnlock();
    }
    public void clean()
    {
        dirt_level = 0;
    }
    public string dirt_level_return()
    {
       
        if(0 <= dirt_level && dirt_level <= 250)
        {
            return "Dust level: clean";
        }
        else if (250 < dirt_level && dirt_level <= 500)
        {
            return "Dust level: dirty";
        }
        else if (500 < dirt_level && dirt_level <= 750)
        {
            return "Dust level: dirty";
        }
        else
        {
            return "Dust level: filthy";
        }
    }
}

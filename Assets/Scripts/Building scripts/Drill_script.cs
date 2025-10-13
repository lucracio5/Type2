using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Drill_script: MonoBehaviour
{
    public GameObject Gamemanager;
    private float timer;
    public int total_collected;
    public ParticleSystem lithium_particle_system;
    public ParticleSystem titanium_particle_system;
    void Start()
    {
        Gamemanager = GameObject.Find("Game Manager");
        this.transform.eulerAngles = new Vector3(-90, -0, 0);
        
        lithium_particle_system = this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>(); //finds the particle system based on where it is in the Hiarchy
        titanium_particle_system = this.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
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


            if(num <= 5) //10% change
            {
                if (Gamemanager.GetComponent<Variable_Tracker>().energy > 20 && Gamemanager.GetComponent<Variable_Tracker>().max_titanium > Gamemanager.GetComponent<Variable_Tracker>().titanium)//checks if you have space
                {
                    Gamemanager.GetComponent<Variable_Tracker>().energy -= 20;
                    Gamemanager.GetComponent<Variable_Tracker>().titanium += 1;
                    total_collected++;
                    TriggerOneTitanium();
                }
            }
            if(num == 11)//1% chance
            {
                if (Gamemanager.GetComponent<Variable_Tracker>().energy > 20 && Gamemanager.GetComponent<Variable_Tracker>().max_lithium > Gamemanager.GetComponent<Variable_Tracker>().lithium)//checks if you have space
                {
                    Gamemanager.GetComponent<Variable_Tracker>().energy -= 20;
                    Gamemanager.GetComponent<Variable_Tracker>().lithium += 1;
                    total_collected++;
                    TriggerOneLithium();
                }
            }
            timer = 0f;
            
        }

    }
    public void TriggerOneLithium()
    {
        if (lithium_particle_system != null)
            lithium_particle_system.Emit(1);
    }
    public void TriggerOneTitanium()
    {
        if (titanium_particle_system != null)
            titanium_particle_system.Emit(1);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
    public GameObject Gamemanager;
    Variable_Tracker tracker;
    Audio_manager Audio_manager;

    [SerializeField] private int lowEnergyThreshold = 5;//The point at which below is considered "low" energy
    [SerializeField] private int lowFoodOrWater = 10;
    [SerializeField] private int lowO2 = 50;

    private bool playingLowEnergySiren = false;

    // Start is called before the first frame update
    void Start()
    {
        Gamemanager = GameObject.Find("Game Manager");
        Audio_manager = Gamemanager.GetComponent<Audio_manager>();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckEnergy();
        //CheckO2();
        //CheckFood();
       // CheckWater();
    }

    void CheckEnergy() {

        // If energy is less than 0, then clamps the minumum value of energy to 0
        if (Gamemanager.GetComponent<Variable_Tracker>().energy <= 0) Gamemanager.GetComponent<Variable_Tracker>().energy = 0;


        if (Gamemanager.GetComponent<Variable_Tracker>().energy <= lowEnergyThreshold && playingLowEnergySiren == false) { //If you have low energy and are not already playing the SFX
            Audio_manager.PlayLowEnergySiren(); // Plays the sound effect
            playingLowEnergySiren = true;
        }
        else if (Gamemanager.GetComponent<Variable_Tracker>().energy > lowEnergyThreshold && playingLowEnergySiren == true) { // If you don't have low energy and are already playing the SFX
            Audio_manager.StopLowEnergySiren(); // Stop the sound effect
            playingLowEnergySiren = false;
        }
    }
    void CheckO2()
    {

        // If energy is less than 0, then clamps the minumum value of energy to 0
        if (Gamemanager.GetComponent<Variable_Tracker>().O2 <= 0) Gamemanager.GetComponent<Variable_Tracker>().O2 = 0;


        if (Gamemanager.GetComponent<Variable_Tracker>().O2 <= lowO2 && playingLowEnergySiren == false)
        { //If you have low energy and are not already playing the SFX
            Audio_manager.PlayLowEnergySiren(); // Plays the sound effect
            playingLowEnergySiren = true;
        }
        else if (Gamemanager.GetComponent<Variable_Tracker>().O2 > lowO2 && playingLowEnergySiren == true)
        { // If you don't have low energy and are already playing the SFX
            Audio_manager.StopLowEnergySiren(); // Stop the sound effect
            playingLowEnergySiren = false;
        }
    }
    void CheckFood()
    {

        // If energy is less than 0, then clamps the minumum value of energy to 0
        if (Gamemanager.GetComponent<Variable_Tracker>().food <= 0) Gamemanager.GetComponent<Variable_Tracker>().food = 0;


        if (Gamemanager.GetComponent<Variable_Tracker>().food <= lowFoodOrWater && playingLowEnergySiren == false)
        { //If you have low energy and are not already playing the SFX
            Audio_manager.PlayLowEnergySiren(); // Plays the sound effect
            playingLowEnergySiren = true;
        }
        else if (Gamemanager.GetComponent<Variable_Tracker>().food > lowFoodOrWater && playingLowEnergySiren == true)
        { // If you don't have low energy and are already playing the SFX
            Audio_manager.StopLowEnergySiren(); // Stop the sound effect
            playingLowEnergySiren = false;
        }
    }
    void CheckWater()
    {

        // If energy is less than 0, then clamps the minumum value of energy to 0
        if (Gamemanager.GetComponent<Variable_Tracker>().water <= 0) Gamemanager.GetComponent<Variable_Tracker>().water = 0;


        if (Gamemanager.GetComponent<Variable_Tracker>().water <= lowFoodOrWater && playingLowEnergySiren == false)
        { //If you have low energy and are not already playing the SFX
            Audio_manager.PlayLowEnergySiren(); // Plays the sound effect
            playingLowEnergySiren = true;
        }
        else if (Gamemanager.GetComponent<Variable_Tracker>().water > lowFoodOrWater && playingLowEnergySiren == true)
        { // If you don't have low energy and are already playing the SFX
            Audio_manager.StopLowEnergySiren(); // Stop the sound effect
            playingLowEnergySiren = false;
        }
    }
}

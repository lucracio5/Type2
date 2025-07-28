using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

// Manages the stats and UI display for multiple rover slots in the game.
// Handles initialization, stat retrieval, and updating the UI panel with the correct rover information.
public class RoverManager : MonoBehaviour
{
    public Variable_Tracker variableTracker; // Reference to the Variable_Tracker script for accessing game variables
    public int initialRoverCost = 100; // Cost to buy a new rover
    public float roverCostScaleFactor = 2f; // Factor by which the cost increases with each new rover
    public int upgradeCost = 20; // Cost to upgrade a rover's stats
    public Vector3 RoverSpawnPoint; // Position where new rovers will be instantiated
    public GameObject rover1Prefab; // Prefab for the rover to instantiate when a new rover is bought
    public GameObject rover2Prefab; // Prefab for the second type of rover
    public GameObject rover3Prefab; // Prefab for the third type of rover

    // Maximum possible stats for any rover (not currently used, but could be useful for upgrades or limits)
    private int maxMovementSpeed = 10;
    private int maxMiningSpeed = 10;
    private int maxBatteryLife = 10;

    // Reference to the UI panel that displays rover stats
    [SerializeField] private GameObject RoverStatsPanel;
    [SerializeField] private GameObject RoverHubPanel;
    [SerializeField] private GameObject RoverSlots;

    // UI elements for displaying the rover's image and stats
    public TMP_Text roverStatsPanelTitleText; // Text field for displaying the rover's name
    public TMP_Text roverHubBuyRoverButtonText; // Text field for the button to buy a new rover
    public Image roverDisplayImage;
    public TMP_Text movementSpeedText;
    public TMP_Text miningSpeedText;
    public TMP_Text batteryLifeText;

    // Sprites representing different rover appearances based on their overall level
    public Sprite Rover1Sprite;
    public Sprite Rover2Sprite;
    public Sprite Rover3Sprite;
    public Vector2[] roverSlotPositions; // Positions for each rover slot on the canvas


    void Start()
    {
        UpdateRoverHubPanel(); //Update the rover hub panel with current stats

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // Check if the S key is pressed
        {
            UpdateRoverHubPanel(); //Update the rover hub panel with current stats
        }


        if (RoverHubPanel.activeInHierarchy)
        {
            //UpdateRoverHubPanel(); //Update the rover hub panel if it is active
        }
    }

    /// Calculates the overall level of a rover based on its stats.
    /// The overall level is the average of movement, mining, and battery stats.
    float OverallLevel(int[] stats)
    {
        int movement = stats[0];
        int mining = stats[1];
        int battery = stats[2];

        // Calculate the average as a float to avoid integer division
        return (movement + mining + battery) / 3f;
    }



    // Opens the stats panel for a specific rover slot and updates the UI with its stats and image.
    public void OpenStatsPanel(int roverSlot)
    {
        if (OverallLevel(variableTracker.roverSlotStats[roverSlot]) < 0.001f)
        {
            return; // Exit if the rover slot has no stats
        }
        else
        {
            RoverHubPanel.SetActive(false); // Hide the Rover Hub Panel when opening the stats panel
        }
        //variableTracker.roverSlotStats = roverSlotStats; // Link the roverSlotStats to the Variable_Tracker

        // Retrieve the stats for the selected rover slot
        int[] stats = variableTracker.roverSlotStats[roverSlot];
        int movement = stats[0];
        int mining = stats[1];
        int battery = stats[2];

        // Update the UI text fields with the current stats
        roverStatsPanelTitleText.text = "Rover " + (roverSlot + 1).ToString() + " Stats:"; // Display the rover slot number
        movementSpeedText.text = "Movement Speed: " + movement.ToString();
        miningSpeedText.text = "Mining Speed: " + mining.ToString();
        batteryLifeText.text = "Battery Life: " + battery.ToString();

        // Choose the appropriate sprite based on the rover's overall level
        if (OverallLevel(stats) < 3)
        {
            roverDisplayImage.sprite = Rover1Sprite;
        }
        else if (OverallLevel(stats) < 6)
        {
            roverDisplayImage.sprite = Rover2Sprite;
        }
        else
        {
            roverDisplayImage.sprite = Rover3Sprite;
        }

        // Activate the stats panel to display the rover's stats
        RoverStatsPanel.SetActive(true);
    }


    public void UpdateRoverHubPanel()
    {
        //Loops through all children of the canvas, only destroys the rover sprites
        foreach (Transform child in RoverHubPanel.transform)
        {
            if (child.name == "DynamicRoverSprite")
            {
                Destroy(child.gameObject);
            }
        }


        //Display the rover sprites in the RoverSlots panel based on their stats
        for (int i = 0; i < variableTracker.roverSlotStats.Length; i++)
        {
            Sprite spriteToDisplay;

            if (OverallLevel(variableTracker.roverSlotStats[i]) < 3) //if it is over nothing (it has been bought) and is less than 3
            {
                spriteToDisplay = Rover1Sprite;
            }
            else if (OverallLevel(variableTracker.roverSlotStats[i]) < 6)
            {
                spriteToDisplay = Rover2Sprite;
            }
            else
            {
                spriteToDisplay = Rover3Sprite;
            }

            // Add a sprite to the canvas for each rover slot
            if (i < variableTracker.roverSlotStats.Length && OverallLevel(variableTracker.roverSlotStats[i]) > 0.001) // Check if the rover has been bought (overall level > 0) and is within bounds of how many rovers there are (5)
            {
                Debug.Log("Overall Level for Rover Slot " + (i + 1) + ": " + OverallLevel(variableTracker.roverSlotStats[i]));
                AddSpriteToCanvas(spriteToDisplay, new Vector2(0, 0), RoverSlots.transform, i);
            }
        }
    }

    void AddSpriteToCanvas(Sprite sprite, Vector2 anchoredPosition, Transform parentCanvas, int roverSlot)
    {
        //create a new GameObject with an Image component
        GameObject go = new GameObject("DynamicRoverSprite", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        //go.transform.SetParent(parentCanvas, false); // Set as child of the canvas

        Transform correctRoverSlot = RoverSlots.transform.Find("RoverSlot" + (roverSlot + 1).ToString());
        go.transform.SetParent(correctRoverSlot, false); // Set as child of the canvas

        //set the sprite
        Image img = go.GetComponent<Image>();
        img.sprite = sprite;

        // Set the anchored position (UI coordinates)
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchoredPosition = anchoredPosition;
        rt.sizeDelta = new Vector2(308, 195); // Set size as needed
        rt.localScale = new Vector3(1.2f, 1.2f, 0f); //Set scale correctly
        rt.pivot = new Vector2(0.5f, 0.5f);      // Set pivot to center

        Debug.Log("Added sprite for Rover Slot " + (roverSlot + 1) + " at position: " + anchoredPosition);
    }


    void ReloadStatsPanel(int roverSlot)
    {
        // Retrieve the stats for the selected rover slot
        int[] stats = variableTracker.roverSlotStats[roverSlot];
        int movement = stats[0];
        int mining = stats[1];
        int battery = stats[2];

        // Update the UI text fields with the current stats
        movementSpeedText.text = "Movement Speed: " + movement.ToString();
        miningSpeedText.text = "Mining Speed: " + mining.ToString();
        batteryLifeText.text = "Battery Life: " + battery.ToString();

        // Choose the appropriate sprite based on the rover's overall level
        if (OverallLevel(stats) < 3)
        {
            roverDisplayImage.sprite = Rover1Sprite;
        }
        else if (OverallLevel(stats) < 6)
        {
            roverDisplayImage.sprite = Rover2Sprite;
        }
        else
        {
            roverDisplayImage.sprite = Rover3Sprite;
        }
    }


    public void IncreaseStat(int whichStat)
    {
        // Extract the rover ID from the title text
        int roverID = int.Parse(roverStatsPanelTitleText.text.Split(' ')[1]) - 1; // Convert "Rover X" to index X-1
        //Debug.Log("Increasing stat for Rover " + (roverID + 1) + ", Stat: " + whichStat);

        if (whichStat == 0) // Movement Speed
        {
            IncreaseMovement(roverID);
        }
        else if (whichStat == 1) // Mining Speed
        {
            IncreaseMining(roverID);
        }
        else if (whichStat == 2) // Battery Life
        {
            IncreaseBattery(roverID);
        }

        ReloadStatsPanel(roverID); // Reload the stats panel to reflect changes
    }


    void IncreaseMovement(int roverID)
    {
        // Increase the movement speed of the specified rover
        if (roverID >= 0 && roverID < variableTracker.roverSlotStats.Length && variableTracker.roverSlotStats[roverID][0] < maxMovementSpeed)
        {
            variableTracker.roverSlotStats[roverID][0] = Mathf.Min(variableTracker.roverSlotStats[roverID][0] + 1, maxMovementSpeed); //Add 1 movement, but do not exceed the maximum movement speed
            variableTracker.money -= upgradeCost; // Deduct the cost of the upgrade from the player's money
        }
        else
        {
            Debug.LogError("Invalid rover ID: " + roverID + " or already at max movement speed.");
        }
    }

    void IncreaseMining(int roverID)
    {
        // Increase the mining speed of the specified rover
        if (roverID >= 0 && roverID < variableTracker.roverSlotStats.Length && variableTracker.roverSlotStats[roverID][1] < maxMovementSpeed)
        {
            variableTracker.roverSlotStats[roverID][1] = Mathf.Min(variableTracker.roverSlotStats[roverID][1] + 1, maxMovementSpeed); //Add 1 movement, but do not exceed the maximum movement speed
            variableTracker.money -= upgradeCost; // Deduct the cost of the upgrade from the player's money
        }
        else
        {
            Debug.LogError("Invalid rover ID: " + roverID + " or already at max mining speed.");
        }
    }

    void IncreaseBattery(int roverID)
    {
       // Increase the battery life speed of the specified rover
        if (roverID >= 0 && roverID < variableTracker.roverSlotStats.Length && variableTracker.roverSlotStats[roverID][2] < maxMovementSpeed)
        {
            variableTracker.roverSlotStats[roverID][2] = Mathf.Min(variableTracker.roverSlotStats[roverID][2] + 1, maxMovementSpeed); //Add 1 movement, but do not exceed the maximum movement speed
            variableTracker.money -= upgradeCost; // Deduct the cost of the upgrade from the player's money
        }
        else
        {
            Debug.LogError("Invalid rover ID: " + roverID + " or already at max battery life.");
        }
    }



    public void BuyRover()
    {
        int roverSlot = CurrentRoverSlot(); // Get the current rover slot to buy a new rover        


        if (variableTracker.money < CurrentRoverCost(roverSlot))
        {
            Debug.LogWarning("Not enough money to buy a new rover.");
            return; // Exit if the player doesn't have enough money
        }

        // Check if the rover slot is within bounds
        if (roverSlot < 0 || roverSlot >= variableTracker.roverSlotStats.Length)
        {
            Debug.LogError("Invalid rover slot: " + roverSlot); //Call for an error message to be displayed in the UI

            return;
        }

        variableTracker.money -= CurrentRoverCost(roverSlot); // Deduct the cost of the rover from the player's money

        // Initialize the stats for the new rover
        variableTracker.roverSlotStats[roverSlot] = new int[] { 1, 1, 1 }; // Set initial stats to 1 for movement, mining, and battery

        UpdateRoverHubPanel(); // Update the Rover Hub Panel to reflect the new rover purchase

        GameObject newRover = Instantiate(rover1Prefab, RoverSpawnPoint, Quaternion.identity); // Instantiate the rover prefab at the specified position
        newRover.name = "Rover_" + (roverSlot + 1).ToString(); // Set the name of the new rover to work with the GetRoverID method in Nav_test

        Debug.Log("Current Rover Slot: " + roverSlot + ", Current Rover Cost: " + CurrentRoverCost(roverSlot));
    }

    int CurrentRoverSlot()
    {
        //finds the first empty rover slot 
        int roverSlot = 0;
        foreach (int[] stats in variableTracker.roverSlotStats)
        {
            if (OverallLevel(stats) < 0.001f) // Find the first empty rover slot
            {
                break; // Found an empty slot, exit the loop
            }
            roverSlot++;
        }

        return roverSlot; // Return the index of the first empty rover slot
    }

    int CurrentRoverCost(int roverSlot)
    {
        if (roverSlot >= variableTracker.roverSlotStats.Length)
        {
            return 0; // Return 0 if the rover slot is invalid
        }

        int curRoverCost = Mathf.RoundToInt(initialRoverCost * Mathf.Pow(roverCostScaleFactor, roverSlot + 1)); // Update the cost for the next rover purchase
        roverHubBuyRoverButtonText.text = "Buy Rover: $" + curRoverCost; // Update the button text with the current cost
        return curRoverCost;
    }
}
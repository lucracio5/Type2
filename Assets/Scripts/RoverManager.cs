using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Manages the stats and UI display for multiple rover slots in the game.
// Handles initialization, stat retrieval, and updating the UI panel with the correct rover information.
public class RoverManager : MonoBehaviour
{
    public Variable_Tracker variableTracker; // Reference to the Variable_Tracker script for accessing game variables

    // Base stats for all rovers (used for initialization)
    private int baseMovementSpeed = 1;
    private int baseMiningSpeed = 1;
    private int baseBatteryLife = 1;

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
    public Image roverDisplayImage;
    public TMP_Text movementSpeedText;
    public TMP_Text miningSpeedText;
    public TMP_Text batteryLifeText;

    // Sprites representing different rover appearances based on their overall level
    public Sprite Rover1Sprite;
    public Sprite Rover2Sprite;
    public Sprite Rover3Sprite;
    public Vector2[] roverSlotPositions; // Positions for each rover slot on the canvas

    // Array holding the stats for each rover slot.
    // Each int[] contains: [movementSpeed, miningSpeed, batteryLife]
    public int[][] roverSlotStats;

    /// Unity Awake method. Initializes the roverSlotStats array with default values for each rover slot.
    /// Each slot starts with the base stats.
    void Awake()
    {
        if (variableTracker.roverSlotStats.Length > 0) //if roverSlotStats is already set in Variable_Tracker
        {
            roverSlotStats = variableTracker.roverSlotStats; // Use existing stats
        }
        else
        {
            InitializeRoverStats(); // Initialize with default stats if not set
        }
    }

    // Initialize the roverSlotStats array with default stats for each rover slot.
    // Each rover starts with base movement speed, mining speed, and battery life.
    void InitializeRoverStats()
    {
        roverSlotStats = new int[][]
        {
            new int[] { baseMovementSpeed, baseMiningSpeed, baseBatteryLife },
            new int[] { baseMovementSpeed, baseMiningSpeed, baseBatteryLife },
            new int[] { baseMovementSpeed, baseMiningSpeed, baseBatteryLife },
            new int[] { baseMovementSpeed, baseMiningSpeed, baseBatteryLife },
            new int[] { baseMovementSpeed, baseMiningSpeed, baseBatteryLife }
        };
    }


    void Start()
    {
        variableTracker.roverSlotStats = roverSlotStats; // Link the roverSlotStats to the Variable_Tracker
        UpdateRoverHubPanel(); // Initialize the rover hub panel with current stats
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // Check if the S key is pressed
        {
            for (int i = 0; i < variableTracker.roverSlotStats.Length; i++)
            {
                // Log the stats of each rover slot to the console
                Debug.Log("Rover Stats of " + i + ": Movement: " + variableTracker.roverSlotStats[i][0] + ", Mining: " + variableTracker.roverSlotStats[i][1] + ", Battery: " + variableTracker.roverSlotStats[i][2]);
            }
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
        variableTracker.roverSlotStats = roverSlotStats; // Link the roverSlotStats to the Variable_Tracker

        // Retrieve the stats for the selected rover slot
        int[] stats = roverSlotStats[roverSlot];
        int movement = stats[0];
        int mining = stats[1];
        int battery = stats[2];

        // Update the UI text fields with the current stats
        movementSpeedText.text = "Movement Speed: " + movement.ToString();
        miningSpeedText.text = "Mining Speed: " + mining.ToString();
        batteryLifeText.text = "Battery Life: " + battery.ToString();
        roverStatsPanelTitleText.text = "Rover " + (roverSlot + 1).ToString() + " Stats:"; // Display the rover slot number

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

        for (int i = 0; i < roverSlotStats.Length; i++)
        {
            // Add a sprite to the canvas for each rover slot
            if (i < roverSlotStats.Length)
            {
                AddSpriteToCanvas(roverDisplayImage.sprite, new Vector2(0, 0), RoverSlots.transform, i);
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
    }


    void ReloadStatsPanel(int roverSlot)
    {
        // Retrieve the stats for the selected rover slot
        int[] stats = roverSlotStats[roverSlot];
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
        Debug.Log("Increasing stat for Rover " + (roverID + 1) + ", Stat: " + whichStat);

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

        // Update the roverSlotStats in Variable_Tracker after modifying stats
        variableTracker.roverSlotStats = roverSlotStats;
        ReloadStatsPanel(roverID); // Reload the stats panel to reflect changes
    }


    void IncreaseMovement(int roverID)
    {
        // Increase the movement speed of the specified rover
        if (roverID >= 0 && roverID < roverSlotStats.Length)
        {
            roverSlotStats[roverID][0] = Mathf.Min(roverSlotStats[roverID][0] + 1, maxMovementSpeed);
            Debug.Log("Increased movement speed for Rover " + (roverID + 1));
        }
        else
        {
            Debug.LogError("Invalid rover ID: " + roverID);
        }
    }

    void IncreaseMining(int roverID)
    {
        // Increase the movement speed of the specified rover
        if (roverID >= 0 && roverID < roverSlotStats.Length)
        {
            roverSlotStats[roverID][1] = Mathf.Min(roverSlotStats[roverID][1] + 1, maxMiningSpeed);
            Debug.Log("Increased mining speed for Rover " + (roverID + 1));
        }
        else
        {
            Debug.LogError("Invalid rover ID: " + roverID);
        }
    }

    void IncreaseBattery(int roverID)
    {
        // Increase the movement speed of the specified rover
        if (roverID >= 0 && roverID < roverSlotStats.Length)
        {
            roverSlotStats[roverID][2] = Mathf.Min(roverSlotStats[roverID][2] + 1, maxBatteryLife);
            Debug.Log("Increased battery life for Rover " + (roverID + 1));
        }
        else
        {
            Debug.LogError("Invalid rover ID: " + roverID);
        }
    }
    
}
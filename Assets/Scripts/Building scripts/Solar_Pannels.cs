using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
    public int level;
    public int panelID; // set when placed on map
    public Material cleanPanelMaterial;
    public Material dirtyPanelMaterial;
    public Material filthyPanelMaterial;


    void Start()
    {
        Gamemanager = GameObject.Find("Game Manager");
        dirt_chance = 300;
        level = 1;


        cleanPanelMaterial = Resources.Load<Material>("SolarPanel");
        dirtyPanelMaterial = Resources.Load<Material>("SolarPanelDirty");
        filthyPanelMaterial = Resources.Load<Material>("SolarPanelFilthy");
        
        //Energy = Gamemanager.GetComponent<Variable_Tracker>().Energy;
    }
  

    // Update is called once per frame

    public string return_total()
    {
        return "Total Energy colected:" + total_collected;
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
                if (num < dirt_chance && dirt_level < 1000)
                {
                    dirt_level += 20;
                }

                time = 0;
                if (level == 2)
                {
                    collected_amount = 2;
                }
                if (level == 3)
                {
                    collected_amount = 4;
                }
            }
        }

        UpdateAppearance();
    }

    //checks each frame to change the appearance to dirty (or clean)
    void UpdateAppearance()
    {
        string dirt_level = dirt_level_return();
        Material materialToChange = cleanPanelMaterial;

        if (dirt_level == "Dust level: clean") materialToChange = cleanPanelMaterial;
        else if (dirt_level == "Dust level: dirty") materialToChange = dirtyPanelMaterial;
        else if (dirt_level == "Dust level: filthy") materialToChange = filthyPanelMaterial;

        ChangeMaterial(materialToChange);
    }

    public void Level2Upgrade()
    {
        if (Gamemanager.GetComponent<ScienceTree>().Level2_panels_unlock && !(level == 2) && (Gamemanager.GetComponent<Variable_Tracker>().money >= 20))
        {
            collected_amount = 2;
            level = 2;
            Gamemanager.GetComponent<Audio_manager>().PlayUnlock();
            Gamemanager.GetComponent<Variable_Tracker>().money -= 20;
        }
        else if(!Gamemanager.GetComponent<ScienceTree>().Level2_panels_unlock)
        {
            Gamemanager.GetComponent<ToolTips>().DisplayMessage("You haven't unlocked level 2 solar panels yet");
            Gamemanager.GetComponent<Audio_manager>().PlayFailedClick();

        }
        else if(level == 2)
        {
            Gamemanager.GetComponent<ToolTips>().DisplayMessage("This solar panel is already level 2");
            Gamemanager.GetComponent<Audio_manager>().PlayFailedClick();
        }
        else if(!(Gamemanager.GetComponent<Variable_Tracker>().money >= 20))
        {
            Gamemanager.GetComponent<ToolTips>().DisplayMessage("You do not have enough money to purchase this upgrade");
            Gamemanager.GetComponent<Audio_manager>().PlayFailedClick();
        }
        
    }
    public void Level3Upgrade()
    {
        if (Gamemanager.GetComponent<ScienceTree>().Level3_panels_unlock && !(level == 3) && (Gamemanager.GetComponent<Variable_Tracker>().money >= 20))
        {
            collected_amount = 4;
            level = 3;
            Gamemanager.GetComponent<Audio_manager>().PlayUnlock();
            Gamemanager.GetComponent<Variable_Tracker>().money -= 20;
        }
        else if (!Gamemanager.GetComponent<ScienceTree>().Level3_panels_unlock)
        {
            Gamemanager.GetComponent<ToolTips>().DisplayMessage("You haven't unlocked level 3 solar panels yet");
            Gamemanager.GetComponent<Audio_manager>().PlayFailedClick();
        }
        else if (level == 3)
        {
            Gamemanager.GetComponent<ToolTips>().DisplayMessage("This solar panel is already level 3");
            Gamemanager.GetComponent<Audio_manager>().PlayFailedClick();
        }
        else if (!(Gamemanager.GetComponent<Variable_Tracker>().money >= 20))
        {
            Gamemanager.GetComponent<ToolTips>().DisplayMessage("You do not have enough money to purchase this upgrade");
            Gamemanager.GetComponent<Audio_manager>().PlayFailedClick();
        }
    }
    public void clean()
    {
        dirt_level -= Mathf.Max(0,dirt_level-250);
    }


    //Changes the material of the solar panel, to dirty or clean depending on argument
    public void ChangeMaterial(Material material)
    {
        Renderer objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null && material != null)
        {
            //Debug.Log("Material changed to " + material.name);
            objectRenderer.material = material;
        }
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

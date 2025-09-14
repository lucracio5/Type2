using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScienceTree : MonoBehaviour
{

    Audio_manager manager;
    Variable_Tracker tracker;
    Jobs jobs;
    ToolTips toolTips;
    [SerializeField] Button hydrobutton;
    [SerializeField] Button nuclear_unlock_button;
    [SerializeField] Button Level2_panels_unlock_button;
    [SerializeField] Button Level3_panels_unlock_button;
    [SerializeField] Button Hydro_buy;
    [SerializeField] Button Nuclear_buy;

    [SerializeField] Button Level2_panels;
    [SerializeField] Button Level3_panels;


    public bool Level2_panels_unlock = false;
    public bool Level3_panels_unlock = false;
    public bool Hydro_unlock = false;
    public bool Nuclear_unlock = false;
    void Start()
    {
        manager = GetComponent<Audio_manager>();
        tracker = GetComponent<Variable_Tracker>();
        jobs = GetComponent<Jobs>();
        toolTips = GetComponent<ToolTips>();
        Hydro_buy.interactable = false;
        Nuclear_buy.interactable=false;
        Level2_panels.interactable = false;
        Level3_panels.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Hydro_unlock)
        {
            Hydro_buy.interactable = true;

        }
        else
        {
            Hydro_buy.interactable = false;
        }
        if (Nuclear_unlock)
        {
            Nuclear_buy.interactable = true;

        }
        else
        {
            Nuclear_buy.interactable = false;
        }
        if (Level2_panels_unlock)
        {
            Level2_panels.interactable = true;
        }
        else
        {
            Level2_panels.interactable = false;
        }
        if(Level3_panels_unlock)    
        {
            Level3_panels.interactable = true;
        }
        else
        {
            Level3_panels.interactable = false;
        }

    }
    public void Hydroponics()
    {
        if(jobs.science_points >= 3 && !Hydro_unlock)
        {
           manager.PlayUnlock();
           Hydro_unlock = true;
           hydrobutton.interactable = false;
           jobs.science_points -= 3;
        }
        else if(!(jobs.science_points >= 3))
        {
            manager.PlayFailedClick();
            toolTips.DisplayMessage("Not Enough Science Points to unlock this item");
        }
        else if(Hydro_unlock)
        {
            manager.PlayFailedClick();
            toolTips.DisplayMessage("This item has already been unlocked");
        }
    }
    public void Lvl2Solar()
    {
        if (jobs.science_points >= 3 && !Level2_panels_unlock)
        {
            manager.PlayUnlock();
            Level2_panels_unlock = true;
            jobs.science_points -= 3;
            Level2_panels.interactable = true;
            //Add Whatever will actually unlock the lvl 2 panels
        }
        else if (!(jobs.science_points >= 3))
        {
            manager.PlayFailedClick();
            toolTips.DisplayMessage("Not Enough Science Points to unlock this item");
        }
        else if (Level2_panels_unlock)
        {
            manager.PlayFailedClick();
            toolTips.DisplayMessage("This item has already been unlocked");
        }
    }
    public void Lvl3Solar()
    {
        if (jobs.science_points >= 5 && !Level3_panels_unlock && Level2_panels_unlock)
        {
            manager.PlayUnlock();
            Level3_panels_unlock = true;
            jobs.science_points -= 3;
            Level3_panels.interactable = true;
            //Add Whatever will actually unlock the lvl 2 panels
        }
        else if (!(jobs.science_points >= 5))
        {
            manager.PlayFailedClick();
            toolTips.DisplayMessage("Not Enough Science Points to unlock this item");
        }
        else if (Level3_panels_unlock)
        {
            manager.PlayFailedClick();
            toolTips.DisplayMessage("This item has already been unlocked");
        }
        else if (!Level2_panels_unlock)
        {
            manager.PlayFailedClick();
            toolTips.DisplayMessage("All previous items in a category must be unlocked before unlocking this item");
        }

    }
    public void NUKE()
    {
        if (jobs.science_points >= 20 && !Nuclear_unlock)
        {
            manager.PlayUnlock();
            Nuclear_unlock = true;
            nuclear_unlock_button.interactable = false;
            jobs.science_points -= 20;
        }
        else
        {
            manager.PlayFailedClick();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Jobs : MonoBehaviour
{
    public Slider xp_bar;
    public int science_jobs = 5;
    public int cleaning_jobs = 5;
    public int open_jobs;
    public Variable_Tracker tracker;
    public TMP_Text text;
    public TMP_Text clean_text;
    public int science_points;
    public int xp;
    public TMP_Text science_points_text;
    public TMP_Text other_science_points_text;
    private float timer;

    void Start()
    {
        tracker = GetComponent<Variable_Tracker>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = science_jobs + "/" + tracker.population;
        clean_text.text = cleaning_jobs + "/" + tracker.population;
        timer += Time.deltaTime * tracker.speed;
        if (timer >= 5)
        {
            timer = 0;
            xp += science_jobs*10;
            CleanDirtyPanels();

        }
        if (xp > 1000)
        {
            xp = 0;
            science_points += 1;
        }
        xp_bar.value = xp;
        science_points_text.text = science_points+" points";
        other_science_points_text.text = science_points + " points";
    }
    public void addScienceJobs(int amount)
    {

        if (science_jobs + amount + cleaning_jobs <= tracker.population)
        {
            science_jobs += amount;
            GetComponent<Audio_manager>().PlayUIclick();
        }
        else
        {
            GetComponent<Audio_manager>().PlayFailedClick();
        }
    }
    public void addCleaningJobs(int amount)
    {
        
        if (science_jobs + amount + cleaning_jobs <= tracker.population)
        {
            cleaning_jobs += amount;
            GetComponent<Audio_manager>().PlayUIclick();
        }
        else
        {
            GetComponent<Audio_manager>().PlayFailedClick();
        }
    }

    public void removeScienceJobs(int amount)
    {
        if (science_jobs - amount >= 0)
        {
           science_jobs -= amount;
           GetComponent<Audio_manager>().PlayUIclick();
        }
        else
        {
           GetComponent<Audio_manager>().PlayFailedClick();
        }
    }
    public void removeCleaningJobs(int amount)
    {
        if (cleaning_jobs - amount >= 0)
        {
           cleaning_jobs -= amount;
           GetComponent<Audio_manager>().PlayUIclick();
        }
        else
        {
          GetComponent<Audio_manager>().PlayFailedClick();
        }
    }


    void CleanDirtyPanels()
    {
        GameObject[] panels = GameObject.FindGameObjectsWithTag("Panel");

        List<Solar_Pannels> dirtyPanels = new List<Solar_Pannels>();
        

        foreach (GameObject panel in panels)
        {
            Solar_Pannels sp = panel.GetComponent<Solar_Pannels>();
            if (sp != null && sp.dirt_level > 0)
            {
                dirtyPanels.Add(sp);
            }
        }
        dirtyPanels = dirtyPanels.OrderByDescending(p => p.dirt_level).ToList();


        int cleaned = 0;

        foreach (var panel in dirtyPanels)
        {
            if (cleaned >= cleaning_jobs) break;

            panel.clean();
            cleaned++;
        }
    }
   
}




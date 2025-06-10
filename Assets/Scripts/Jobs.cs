using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Jobs : MonoBehaviour
{
    public Slider xp_bar;
    public int science_jobs;
    public int open_jobs;
    public Variable_Tracker tracker;
    public TMP_Text text;
    public int science_poins;
    public int xp;
    public TMP_Text science_points_text;
    private float timer;

    void Start()
    {
        tracker = GetComponent<Variable_Tracker>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = science_jobs + "/" + tracker.population;
        timer += Time.deltaTime * tracker.speed;
        if (timer > 2)
        {
            timer = 0;
            xp += science_jobs;
        }
        if (xp > 1000)
        {
            xp = 0;
            science_poins += 1;
        }
        xp_bar.value = xp;
        science_points_text.text = science_poins+" points";
    }
    public void addJobs(int amount)
    {
        if (science_jobs + amount <= tracker.population)
        {
            science_jobs += amount;
            GetComponent<Audio_manager>().PlayUIclick();
        }
        else
        {
            GetComponent<Audio_manager>().PlayFailedClick();
        }

        
    }
    public void removeJobs(int amount)
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

}

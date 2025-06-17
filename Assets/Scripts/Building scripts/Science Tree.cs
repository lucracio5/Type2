using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScienceTree : MonoBehaviour
{

    public Audio_manager manager;
    public Variable_Tracker tracker;
    public Jobs jobs;
    [SerializeField] Button hydrobutton;
    void Start()
    {
        manager = GetComponent<Audio_manager>();
        tracker = GetComponent<Variable_Tracker>();
        jobs = GetComponent<Jobs>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hydroponics()
    {
        if(jobs.science_points >= 3 && !tracker.Hydro_unlock)
        {
           manager.PlayUnlock();
           tracker.Hydro_unlock = true;
           hydrobutton.interactable = false;
        }
        else
        {
            manager.PlayFailedClick();
        }
    }
}

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
    [SerializeField] Button Hydro_buy;
    [SerializeField] Button Nuclear_buy;
    public bool Hydro_unlock = false;
    void Start()
    {
        manager = GetComponent<Audio_manager>();
        tracker = GetComponent<Variable_Tracker>();
        jobs = GetComponent<Jobs>();
        Hydro_buy.interactable = false;
        Nuclear_buy.interactable=false;
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
        else
        {
            manager.PlayFailedClick();
        }
    }
}

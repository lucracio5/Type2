using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Imports : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text Next_delivery;
    public TMP_Text Food_text;
    public TMP_Text Water_text;
    public float next_timer;
    public float next_import_time = 60;
    public GameObject Gamemanager;
    public void Start()
    {
        Gamemanager = GameObject.Find("Game Manager");
    }
    public void openImports()
    {
        panel.SetActive(true);
    }
    public void closeImports()
    {
        panel.SetActive(false);
    }
    public void Update()
    {
        next_timer += Time.deltaTime* Gamemanager.GetComponent<Variable_Tracker>().speed;
        Next_delivery.text = "Time Untuil Next Delivery: "+((int)(next_import_time - next_timer)).ToString();
        Food_text.text = "Food at " + Gamemanager.GetComponent<Variable_Tracker>().food + "%";
        Water_text.text = "Food at " + Gamemanager.GetComponent<Variable_Tracker>().water + "%";
        if (next_timer >= next_import_time)
        {
            next_timer = 0;
            Import();
        }
    }
    public void Import()
    {
        Debug.Log("Import time!");
    }

}

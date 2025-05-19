using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2Plant : MonoBehaviour
{
    private float time;
    public GameObject Gamemanager;
    Variable_Tracker tracker;
    public int total_collected;
    private int i;
    void Start()
    {
        Gamemanager = GameObject.Find("Game Manager");
        tracker = Gamemanager.GetComponent<Variable_Tracker>();
    }

    void Update()
    {
        time += Time.deltaTime * Gamemanager.GetComponent<Variable_Tracker>().speed;
        if (time > 0.1f)
        {
            int availableRoom = Gamemanager.GetComponent<Variable_Tracker>().max_O2 - Gamemanager.GetComponent<Variable_Tracker>().O2;
            int addedO2 = Mathf.Min(4, availableRoom); // 4 * 0.1 = 40/sec
            Gamemanager.GetComponent<Variable_Tracker>().O2 += addedO2;
            total_collected += addedO2;
            time = 0;
            if(i > 10)
            {
                Gamemanager.GetComponent<Variable_Tracker>().energy -= 1;
                i = 0;
            }
            i++;
        }


    }
}

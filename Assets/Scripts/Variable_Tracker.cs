using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Variable_Tracker : MonoBehaviour
{
    public int Energy = 0;
    public Text text;
    public int money;
    void Start()
    {
        money = 100;

    }

    // Update is called once per frame
    void Update()
    {
        
        text.text = "Energy: "+Energy.ToString()+" $"+money;
        if(Energy > 100)
        {
            Energy -= 100;
            money += 10;

        }
    }
}

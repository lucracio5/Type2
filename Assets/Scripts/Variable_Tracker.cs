using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Variable_Tracker : MonoBehaviour
{
    public int Energy = 0;
    public Text text;
    public int money;
    public int speed;
    public float rotate_speed = 0.01f;
    public int Regolith = 0;
    void Start()
    {
        money = 100;

    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", rotate_speed*Time.time*speed);
        text.text = "Energy: "+Energy.ToString()+" $"+money+" Lunar Regolith:"+ Regolith.ToString();
        if(Regolith > 2)
        {
            Regolith -= 2;
            money += 50;

        }
    }
    public void speed1()
    {
        speed = 1;
    }
    public void speed2()
    {
        speed = 2;
    }
    public void speed5()
    {
        speed = 5;
    }
}

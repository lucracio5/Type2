using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager: MonoBehaviour
{
    // Start is called before the first frame update
    public int energy = 0;
    public int money;
    public int speed;
    public float rotate_speed = 0.01f;
    public int Regolith = 0;
  
    public int max_energy = 300;
   
    public int max_mining;
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
    public void Sell_materials()
    {
        money += Regolith * 25;
        Regolith = 0;
    }
}

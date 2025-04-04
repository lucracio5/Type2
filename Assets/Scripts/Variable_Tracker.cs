using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;
using UISlider = UnityEngine.UI.Slider;

public class Variable_Tracker : MonoBehaviour
{
    public int energy = 0;
    public Text text;
    public int money;
    public int speed = 1;
    public float rotate_speed = 0.01f;
    public int Regolith = 0;
    public UISlider energy_slider;
    public UISlider mining_slider;
    public UISlider O2_slider;
    public int max_energy = 300;
    public Text energy_text;
    public Text mining_text;
    public Text O2_text;
    public int max_mining;
    public GameObject panel;
    public int O2;

    void Start()
    {
        money = 100;
        speed = 1;

    }
    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", rotate_speed * Time.time);
        text.text = money.ToString();
        energy_text.text= energy.ToString()+"/"+max_energy.ToString();
        mining_text.text = Regolith.ToString() + "/" + max_mining.ToString();
        energy_slider.value = energy;
        mining_slider.value = Regolith;

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
    public void Sell_materials()
    {
        money += Regolith * 25;
        Regolith = 0;
    }
    public void Test()
    {
       panel.SetActive(false);
    }
}

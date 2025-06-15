using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;
using UISlider = UnityEngine.UI.Slider;
using System.IO;
using TMPro;

public class Variable_Tracker : MonoBehaviour
{
    public int energy = 0;
    public int regolith = 0;
    public int water = 100;
    public int population = 10;
    public int food = 100;
    public int O2 = 100;
    public int money;
    

    public UISlider energy_slider;
    public UISlider mining_slider;
    public UISlider O2_slider;
    public UISlider water_slider;
    public UISlider food_slider;
    public UISlider population_slider;

    public Text energy_text;
    public Text mining_text;
    public Text O2_text;
    public Text water_text;
    public Text food_text;
    public Text population_text;
    public Text money_text;

    public int max_energy = 300;
    public int max_mining = 10;
    public int max_water = 100;
    public int max_food = 100;
    public int max_O2 = 100;
    public int max_population = 10;

    Audio_manager audio_manager;
    Jobs jobs;
    public GameObject GameoverText;

    public int speed;
    public float rotate_speed = 0.01f;

    public GameObject panel;
    public GameObject map;

    public bool Hydro_unlock = false;
    public UnityEngine.UI.Button HydroButton;

    public TMP_Text Solartext1;
    public TMP_Text Solartext2;

    public void Start()
    {
        Begin();
        HydroButton.interactable = false;
        jobs = GetComponent<Jobs>();

    }
    public TMP_Text returnSolar1()
    {
        return Solartext1;
    }
    public TMP_Text returnSolar2()
    {
        return Solartext2;
    }

    public void Begin()
    {
        SaveSystem.Load();
        speed = 1;
        Time.timeScale = 1;
        InvokeRepeating("Oxygen", 1f, 1f);
        InvokeRepeating("LifeSuport", 0f, 120f);
        audio_manager = GetComponent<Audio_manager>();
    }
    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", rotate_speed * Time.time);

        money_text.text = money.ToString();
        energy_text.text = energy.ToString() + "/" + max_energy.ToString();
        mining_text.text = regolith.ToString() + "/" + max_mining.ToString();
        O2_text.text = O2.ToString() + "/" + max_O2.ToString();
        water_text.text = water.ToString() + "/" + max_water.ToString();
        food_text.text = food.ToString() + "/" + max_food.ToString();
        population_text.text = population.ToString() + "/" + max_population.ToString();

        energy_slider.value = energy;
        mining_slider.value = regolith;
        water_slider.value = water;
        food_slider.value = food;
        population_slider.value = population;
        O2_slider.value = O2;

        if (O2 < 0 || food < 0 || water < 0)
        {
            Debug.Log("Game Over");
            GameoverText.SetActive(true);
            speed = 0;
        }
        if(Hydro_unlock)
        {
            HydroButton.interactable = true;
        }


    }
    public void speed1()
    {
        speed = 1;
        
    }
    public void speed2()
    {
        speed = 2;
        audio_manager.PlayUIclick();
    }
    public void speed5()
    {
        speed = 5;
        audio_manager.PlayUIclick();
    }
    public void Save_button()
    {
        SaveSystem.Save();
        audio_manager.PlayUIclick();
    }
    public void Test()
    {
        panel.SetActive(false);
    }
    public void Oxygen()
    {
        O2 -= population;
    }
    public void LifeSuport()
    {
        food -= population;
        water -= population;
    }



    public void Save(ref VariableSaveData data)
    {
        data.energy = energy;
        data.regolith = regolith;
        data.water = water;
        data.population = population;
        data.food = food;
        data.O2 = O2;
        data.money = money;
        data.Map = "";
        foreach(MapCell cell in GameObject.Find("Map").GetComponent<MoonMapMaker>().mapCells)
        {
            data.Map += cell.contents.ToString() + ",";
        }
        data.Map = data.Map.Remove(data.Map.Length - 1);
    }
        

   public void LoadData(VariableSaveData data)
   {
        MoonMapMaker map = GameObject.Find("Map").GetComponent<MoonMapMaker>(); ;
        string[] cells = data.Map.Split(',');
        for(int i = 0; i < cells.Length;i++)
        {
            if (cells[i] != "-1")// && map.mapCells[i].building == null
            {
                map.mapCells[i].Add_saved_building(Int32.Parse(cells[i]));
            }
        }
        energy = data.energy;
        regolith = data.regolith;
        water = data.water;
        population = data.population;
        food = data.food;
        O2 = data.O2;
        money = data.money;
        //jobs.xp = data.xp;
        //jobs.science_poins = data.science_points;

   }
    

    [System.Serializable]
    public struct VariableSaveData
    {
        public string Map;
        public int energy;
        public int regolith;
        public int water;
        public int population;
        public int food;
        public int O2;
        public int money;
        //public int xp;
        //public int science_points;
        //public int science_jobs;
        //public int cleaning_jobs;


    }



}
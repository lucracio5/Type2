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
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class Variable_Tracker : MonoBehaviour
{
    public bool canDie = false; //only for tetsing / debugging


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

    public List<string> crewNames = new List<string>();
    
    public int max_energy = 300;
    public int max_mining = 10;
    public int max_water = 100;
    public int max_food = 100;
    public int max_O2 = 100;
    public int max_population = 10;

    Audio_manager audio_manager;
    Jobs jobs;
    ScienceTree tree;
    public GameObject GameoverText;

    public int speed;
    public float rotate_speed = 0.01f;

    public GameObject panel;
    public GameObject map;

    
    
    public UnityEngine.UI.Button shop_button;
    public TMP_Text shop_text;
    public bool cancel = false;

    public TMP_Text Solartext1;
    public TMP_Text Solartext2;

    public int xp;
    public int science_points;
    public int science_jobs;
    public int cleaning_jobs;
    public bool Hydro_Unlock;

    public void Start()
    {
        Begin();
        
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
        jobs = GetComponent<Jobs>();
        tree = GetComponent<ScienceTree>();
        InvokeRepeating("Oxygen", 1f, 1f);
        InvokeRepeating("LifeSuport", 0f, 120f);
        audio_manager = GetComponent<Audio_manager>();
        jobs.xp = xp;
        jobs.science_points = science_points;
        jobs.science_jobs = science_jobs;
        jobs.cleaning_jobs = cleaning_jobs;
        tree.Hydro_unlock = Hydro_Unlock;


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

        population_slider.maxValue = max_population;


        if ((O2 <= 0 || food <= 0 || water <= 0) && canDie) //REMOVE CAN DIE AFTER DEBUGGING, NOT PART OF GAME
        {
            Debug.Log("Game Over");
            GameoverText.SetActive(true);
            Time.timeScale = 0;
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
        xp = jobs.xp;
        science_points = jobs.science_points;
        science_jobs = jobs.science_jobs;
        cleaning_jobs = jobs.cleaning_jobs;
        Hydro_Unlock = tree.Hydro_unlock;
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
    public void Shop_button_1()
    {
        shop_button.GetComponent<UnityEngine.UI.Image>().color = Color.blue;
        shop_text.text = "Shop";
        cancel = false;
    }
    public void Shop_button_2()
    {
        shop_button.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        shop_text.text = "Cancel";
        cancel = true;

    }
    public void map_cancel()
    {
        if (cancel)
        {
            GameObject.Find("Map").GetComponent<MapPointer>().ChangeActiveCursor(0);
            Shop_button_1();
            
        }
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
        data.xp = xp;
        data.science_points = science_points; 
        data.science_jobs = science_jobs;
        data.cleaning_jobs = cleaning_jobs;
        data.hydro_unlock = Hydro_Unlock;

        foreach (MapCell cell in GameObject.Find("Map").GetComponent<MoonMapMaker>().mapCells)
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
        xp = data.xp;
        science_points = data.science_points;
        science_jobs = data.science_jobs;
        cleaning_jobs = data.cleaning_jobs;
        Hydro_Unlock = data.hydro_unlock;
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
        public int xp;
        public int science_points;
        public int science_jobs;
        public int cleaning_jobs;
        public bool hydro_unlock;


    }



}
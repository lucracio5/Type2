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



    public int speed = 1;
    public float rotate_speed = 0.01f;

    public GameObject panel;
    public GameObject Gamemanager;

    public bool save;
    public bool load;




    void Start()
    {
        money = 100;
        speed = 1;
        Gamemanager = GameObject.Find("Game Manager");

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


    }
    public void speed1()
    {
        speed = 1;
        //Gamemanager.SaveData();
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
        money += regolith * 25;
        regolith = 0;
    }
    public void Test()
    {
        panel.SetActive(false);
    }



    public void Save(ref VariableSaveData data)
    {
        //SaveDataModel model = new SaveDataModel();
        data.Map = Gamemanager.GetComponent<MoonMapMaker>().mapCells;
        data.energy = Gamemanager.GetComponent<Variable_Tracker>().energy;
        data.regolith = Gamemanager.GetComponent<Variable_Tracker>().regolith;
        data.water = Gamemanager.GetComponent<Variable_Tracker>().water;
        data.population = Gamemanager.GetComponent<Variable_Tracker>().population;
        data.food = Gamemanager.GetComponent<Variable_Tracker>().food;
        data.O2 = Gamemanager.GetComponent<Variable_Tracker>().O2;
        data.money = Gamemanager.GetComponent<Variable_Tracker>().money;

        //string json = JsonUtility.ToJson(model);
        //File.WriteAllText(Application.persistentDataPath + "/save.json", json);
        //Debug.Log("Writing file to: " + Application.persistentDataPath);
    }
        

   public void LoadData(VariableSaveData data)
   {
            //SaveDataModel model = JsonUtility.FromJson<SaveDataModel>(File.ReadAllText(Application.persistentDataPath + "/save.json"));
        Gamemanager.GetComponent<MoonMapMaker>().mapCells = data.Map;
        Gamemanager.GetComponent<Variable_Tracker>().energy = data.energy;
        Gamemanager.GetComponent<Variable_Tracker>().regolith = data.regolith;
        Gamemanager.GetComponent<Variable_Tracker>().water = data.water;
        Gamemanager.GetComponent<Variable_Tracker>().population = data.population;
        Gamemanager.GetComponent<Variable_Tracker>().food = data.food;
        Gamemanager.GetComponent<Variable_Tracker>().O2 = data.O2;
        Gamemanager.GetComponent<Variable_Tracker>().money = data.money;
   }
    

    [System.Serializable]
    public struct VariableSaveData
    {
        public List<MapCell> Map;
        public int energy;
        public int regolith;
        public int water;
        public int population;
        public int food;
        public int O2;
        public int money;


    }



}
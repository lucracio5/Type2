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
    public TMP_Text Import1;
    public TMP_Text Import2;
    public TMP_Text Import3;
    public TMP_Text Output1;
    public TMP_Text Output2;
    public TMP_Text Output3;
    public float next_timer;
    public float next_import_time = 60;
    public GameObject Gamemanager;
    import[] imports = new import[3];
    output[] outputs = new output[3];
    

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
    public void foodImport()
    {
        import Food = new import();
        Food.text = "Food Import";
        Food.cost = 20;
        bool clear = true;
        Debug.Log("attempt to food");
        if (Gamemanager.GetComponent<Variable_Tracker>().money > Food.cost && Gamemanager.GetComponent<Variable_Tracker>().food < 100)
        {
            for (int i = 0; i < imports.Length; i++)
            {
                if (imports[i] == null && clear == true)
                {
                    imports[i] = Food;
                    clear = false;
                }
            }
            if (clear)
            {
                imports[0] = Food;
            }
        } 
    }
    public void waterImport()
    {
        import water = new import();
        bool clear = true;
        water.text = "Water Import";
        water.cost = 20;
        Debug.Log("attempt to water");
        if (Gamemanager.GetComponent<Variable_Tracker>().money > water.cost && Gamemanager.GetComponent<Variable_Tracker>().water < 100)
        {
            for (int i = 0; i < imports.Length; i++)
            {
                if (imports[i] == null && clear == true)
                {
                    imports[i] = water;
                    clear = false;
                }
            }
            if (clear)
            {
                imports[0] = water;
            }
        }
        
    }
    public void Update()
    {
        next_timer += Time.deltaTime * Gamemanager.GetComponent<Variable_Tracker>().speed;
        Next_delivery.text = "Time Untuil Next Ship: " + ((int)(next_import_time - next_timer)).ToString();
        Food_text.text = "Food at " + Gamemanager.GetComponent<Variable_Tracker>().food + "%";
        Water_text.text = "Food at " + Gamemanager.GetComponent<Variable_Tracker>().water + "%";
        if (imports[0] != null)
            Import1.text = imports[0].text + " " + imports[0].cost.ToString();
        if (imports[1] != null)
            Import2.text = imports[1].text + " " + imports[1].cost.ToString();
        if (imports[2] != null)
            Import3.text = imports[2].text + " " + imports[2].cost.ToString();
        if (outputs[0] != null)
            Output1.text = outputs[0].text + " " + outputs[0].cost.ToString();
        if (outputs[1] != null)
            Output2.text = outputs[1].text + " " + outputs[1].cost.ToString();
        if (outputs[2] != null)
            Output3.text = outputs[2].text + " " + outputs[2].cost.ToString();

        if (next_timer >= next_import_time)
        {
            next_timer = 0;
            Ship();
        }
    }
    public void Ship()
    {
        Debug.Log("Import time!");
        for (int i = 0; i < imports.Length; i++)
        {
            if (imports[i].text == "Food Import")
            {
                Gamemanager.GetComponent<Variable_Tracker>().food += 10;
                Gamemanager.GetComponent<Variable_Tracker>().money -= imports[i].cost;
            }
            else if(imports[i].text == "Water Import")
            {
                Gamemanager.GetComponent<Variable_Tracker>().water += 10;
                Gamemanager.GetComponent<Variable_Tracker>().money -= imports[i].cost;
            }
            imports[i] = null;
        }
        for (int i = 0; i < outputs.Length; i++)
        {
            outputs[i] = null;
        } 

    }

}
public class import
{
    public string text;
    public int cost;
}
public class output
{
    public string text;
    public int cost;
}


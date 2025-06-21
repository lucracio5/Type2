using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapPointer : MonoBehaviour
{
    [SerializeField]
    //GameObject pointer;
    MoonMapMaker map;
    GameObject cursor;
    MapCell current_cell;
    public GameObject[] cursors;
    public int activeCursor;
    public GameObject Gamemanager;
    public Building_Scriptable_Object[] buildings;
    public GameObject panel;
    public Text panel_text;
    public Text panel_text2;
    Audio_manager audio_manager;
    public GameObject LaunchPanel;
    public GameObject SciencePanel;
    public GameObject SolarPanelPanel;
    public GameObject DomePanel;
    public bool clickable;

    public float cursorYOffset;
    

    void Start()
    {
        map = GetComponent<MoonMapMaker>();
        activeCursor = 0;
        Gamemanager = GameObject.Find("Game Manager");
        panel_text2.text = "test";
        audio_manager = Gamemanager.GetComponent<Audio_manager>();
        clickable = true;
    }



    void Update()
    {
        cursor = cursors[activeCursor];
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            cursor.SetActive(true);
            Vector3 hitpoint = hit.point;
            MapCell cell = map.NearestCell(hitpoint);
            cursor.transform.position = cell.centerpoint + new Vector3(0, cursorYOffset, 0);
            current_cell = cell;
        }
        else
        {
            current_cell = null;
            cursor.SetActive(false);
        }


        
        if (Input.GetMouseButtonDown(0) && current_cell != null && current_cell.building == null && !EventSystem.current.IsPointerOverGameObject())
        {
            int prefabs_index = activeCursor - 1;
            Debug.Log("Build Index at click =  " + prefabs_index);
            Debug.Log(prefabs_index >= 0 && Gamemanager.GetComponent<Variable_Tracker>().money >= buildings[prefabs_index].cost);
            if (prefabs_index >= 0 && Gamemanager.GetComponent<Variable_Tracker>().money >= buildings[prefabs_index].cost)
            {
                clickable = false;
                current_cell.Add_building(prefabs_index);
                if (prefabs_index == 0)
                {
                    Gamemanager.GetComponent<Variable_Tracker>().max_population += 10;
                }
                Invoke("temp_off_ui", 0.1f);
                
                
                Gamemanager.GetComponent<Variable_Tracker>().money -= buildings[prefabs_index].cost;
                
                //current_cell.ChangeHeight(1);
            }
        }

        if (Input.GetMouseButtonDown(0) && current_cell != null && current_cell.building != null && clickable)
        {
            print(current_cell.building.tag);
            if (current_cell.building.tag == "Launchpad")
            {
                LaunchPanel.SetActive(true);
                audio_manager.PlayOpen();
            }
            else if (current_cell.building.tag == "Science")
            {
                SciencePanel.SetActive(true);
                audio_manager.PlayOpen();
            }
            else if (current_cell.building.tag == "Solar Panel")
            {
                SolarPanelPanel.SetActive(true);
                audio_manager.PlayOpen();
            }
            else if (current_cell.building.tag == "Dome")
            {
                DomePanel.SetActive(true);
                audio_manager.PlayOpen();
            }
            else
            {
                panel.gameObject.SetActive(true);
                panel_text.text = current_cell.building.name;
                if (current_cell.building.GetComponentInChildren<Drill_script>() != null)
                {
                    panel_text2.text = "Total Regolith colected: " + current_cell.building.GetComponentInChildren<Drill_script>().total_collected.ToString();
                    Debug.Log(current_cell.building.GetComponentInChildren<Drill_script>().return_total().ToString());
                }
                else if (current_cell.building.GetComponentInChildren<Solar_Pannels>() != null)
                {
                    panel_text2.text = "Total Energy colected: " + current_cell.building.GetComponentInChildren<Solar_Pannels>().total_collected.ToString();
                    Debug.Log(current_cell.building.GetComponentInChildren<Solar_Pannels>().return_total().ToString());
                }
                else
                {
                    panel_text2.gameObject.SetActive(false);
                }
            }




          

        }
    }
    public void temp_off_ui()
    {
        clickable = true;
    }
    public void ChangeActiveCursor(int cursor) //Changes the cursor by disabling other cursor first then turning on new cursor
    {
        if (0 <= cursor && cursor <= cursors.Length)
        {
            cursors[activeCursor].gameObject.SetActive(false);
            activeCursor = cursor;
            cursors[activeCursor].gameObject.SetActive(true);
        }
    }



}

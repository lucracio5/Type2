using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
    public GameObject NuclearPanel;
    public bool clickable;
    public int unplaceable_buildings;

    public float cursorYOffset;
    public UIDarkener uiDarkener; 
    

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
        if (uiDarkener.AnyPanelsOpen()) clickable = false; //if any panels are open, you will not be able to click on buildings behind it
        else clickable = true;

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


        
        if (Input.GetMouseButtonDown(0) && current_cell != null  && !EventSystem.current.IsPointerOverGameObject() && activeCursor != 0 && (current_cell.building == null || current_cell.building.tag == "Marker"))
        {
            
            int prefabs_index = (unplaceable_buildings-1)+activeCursor;
            if (prefabs_index >= 0 && Gamemanager.GetComponent<Variable_Tracker>().money >= buildings[prefabs_index].cost)
            {
 
                clickable = false;
                current_cell.Add_building(prefabs_index,true);
                Invoke("temp_off_ui", 0.1f);

                //current_cell.ChangeHeight(1);
            }
        }

        if (Input.GetMouseButtonDown(0) && current_cell != null && current_cell.building != null && clickable)
        {
            
            if (current_cell.building.tag == "Launchpad")
            {
                LaunchPanel.SetActive(true);
            }
            else if (current_cell.building.tag == "Science")
            {
                SciencePanel.SetActive(true);
            }
            else if (current_cell.building.tag == "Solar Panel")
            {
                SolarPanelPanel.SetActive(true);
            }
            else if (current_cell.building.tag == "Dome")
            {
                DomePanel.SetActive(true);
            }
            else if (current_cell.building.tag == "Power Plant")
            {
                NuclearPanel.SetActive(true);

            }
            else if (current_cell.building.tag == "Marker") 
            {
                current_cell.Add_building(unplaceable_buildings, true);
                Debug.Log("Adding via click");

            }//Dont open the else panel
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

            if(current_cell.building.tag != "Marker")
            {
                audio_manager.PlayOpen();
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
        
        if(cursor == 1)
        {
            GetComponent<MoonMapMaker>().show_markers();
            cursors[activeCursor].gameObject.SetActive(false);
            activeCursor = cursor;
            cursors[activeCursor].gameObject.SetActive(true);

        }

    }



}

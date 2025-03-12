using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class MapPointer : MonoBehaviour
{
    [SerializeField]
    GameObject pointer;
    MoonMapMaker map;
    GameObject cursor;
    MapCell current_cell;
    public GameObject[] cursors;
    int activeCursor;
    public GameObject Gamemanager;
    public Building_Scriptable_Object[] buildings;



    void Start()
    {
        map = GetComponent<MoonMapMaker>();
        activeCursor = 0;
        Gamemanager = GameObject.Find("Game Manager");
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
            cursor.transform.position = cell.centerpoint;
            current_cell = cell;
        }
        else
        {
            current_cell = null;
            cursor.SetActive(false);
        }


        
        if (Input.GetMouseButtonDown(0) && current_cell != null)
        {
            int prefabs_index = activeCursor - 1;
            if (prefabs_index >= 0 && Gamemanager.GetComponent<Variable_Tracker>().money >= buildings[prefabs_index].cost)
            {
                current_cell.Add_building(buildings[prefabs_index].prefab);
                Gamemanager.GetComponent<Variable_Tracker>().money -= buildings[prefabs_index].cost;
                
                //current_cell.ChangeHeight(1);
            }
        }

        if (Input.GetMouseButtonDown(1) && current_cell != null)
        {
            current_cell.ChangeHeight(-1);
        }
    }

    //I thinks it might be more efficient to have a way to detect the button pressed somehow I tried adding tags but I dont know how to get those tags from the button object
    public void Button()
    {
        cursors[activeCursor].gameObject.SetActive(false); 
        activeCursor = 0;
        cursors[activeCursor].gameObject.SetActive(true);
    }
    public void Button2()
    {
        cursors[activeCursor].gameObject.SetActive(false);
        activeCursor = 1;
        cursors[activeCursor].gameObject.SetActive(true);
    }
    public void Button3()
    {
        cursors[activeCursor].gameObject.SetActive(false);
        activeCursor = 2;
        cursors[activeCursor].gameObject.SetActive(true);
    }
    public void Button4()
    {
        cursors[activeCursor].gameObject.SetActive(false);
        activeCursor = 3;
        cursors[activeCursor].gameObject.SetActive(true);
    }
    public void Button5()
    {
        cursors[activeCursor].gameObject.SetActive(false);
        activeCursor = 4;
        cursors[activeCursor].gameObject.SetActive(true);
    }

}

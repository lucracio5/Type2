using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class PopulationBuilding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("pop", 0.1f);
    }

    public void pop()
    {
        GameObject Gamemanager = GameObject.Find("Gamemanager");
        Gamemanager.GetComponent<Variable_Tracker>().max_population = Gamemanager.GetComponent<Variable_Tracker>().max_population + 10;
        Debug.Log(Gamemanager.GetComponent<Variable_Tracker>().max_population);
    }
}

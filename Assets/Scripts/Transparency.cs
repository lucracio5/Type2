using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparency: MonoBehaviour
{
    // Start is called before the first frame update
    private float time;
    public float transparency = 0.2f;
    public GameObject Gamemanager;
    public bool placed;
    void Start()
    {
        Gamemanager = GameObject.Find("Game Manager");
        this.GetComponent<MeshRenderer>().material.color = new Color(transparency, transparency, transparency, transparency);
        placed = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime*Gamemanager.GetComponent<Variable_Tracker>().speed;
        if (time > 0.5)
        {
            if(transparency < 1)
            {
                transparency = transparency + 0.1f;
            }
            this.GetComponent<MeshRenderer>().material.color = new Color(transparency, transparency, transparency, transparency);
            time = 0;
   
        }

    }
}

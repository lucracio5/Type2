using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2Plant : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    private float time;
    public float transparency = 0.2f;
    public GameObject Gamemanager;
    public int total_collected;
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
        time += Time.deltaTime * Gamemanager.GetComponent<Variable_Tracker>().speed;
        if (time > 0.5)
        {
            if (transparency < 1 && !placed)
            {
                transparency = transparency + 0.1f;
            }
            else if (Gamemanager.GetComponent<Variable_Tracker>().O2 < Gamemanager.GetComponent<Variable_Tracker>().max_O2)
            {
                Gamemanager.GetComponent<Variable_Tracker>().O2 += 20;
                total_collected = total_collected + 20;
                Gamemanager.GetComponent<Variable_Tracker>().energy -= 1;
                placed = true;
                transparency = 1;

            }
            else
            {
                placed = true;
                transparency = 1;
            }
            this.GetComponent<MeshRenderer>().material.color = new Color(transparency, transparency, transparency, transparency);
            time = 0;

        }

    }
}

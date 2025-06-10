using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydroponics : MonoBehaviour
{
    private float timer;
    Variable_Tracker tracker;
    public float hydrotime = 10f;
    public GameObject Gamemanger;

    void Start()
    {
        Gamemanger = GameObject.Find("Game Manager");
        tracker = Gamemanger.GetComponent<Variable_Tracker>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * Gamemanger.GetComponent<Variable_Tracker>().speed;
        if(timer > hydrotime)
        {
            tracker.food += 2;
            tracker.water -= 1;
            timer = 0;
        }
    }
}

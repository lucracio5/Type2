using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UISlider = UnityEngine.UI.Slider;

public class NuclearPlant : MonoBehaviour
{
    Variable_Tracker tracker;
    int total_collected;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        tracker = GameObject.Find("Game Manager").GetComponent<Variable_Tracker>();
        time = 0;
        tracker.max_energy = 500;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * GameObject.Find("Game Manager").GetComponent<Variable_Tracker>().speed;
        if (time > 5)
        {
            if (tracker.energy < tracker.max_energy && GetComponent<Transparency>().placed && tracker.fuel > 0)
            {
                tracker.energy += 65;
                total_collected = total_collected + 25;
                tracker.fuel -= 1;
                time = 0;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UISlider = UnityEngine.UI.Slider;

public class NuclearPlant : MonoBehaviour
{
    Variable_Tracker Tracker;
    public UISlider energy_slider;
    // Start is called before the first frame update
    void Start()
    {
        Variable_Tracker Tracker = GameObject.Find("Game Manager").GetComponent<Variable_Tracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

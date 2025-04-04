using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using UISlider = UnityEngine.UI.Slider;

public class Bar_tracker : MonoBehaviour
{
    // Start is called before the first frame update
    public UISlider energy_slider;
    public UISlider mining_slider;
    public UISlider O2_slider;

    public Text energy_text;
    public Text mining_text;
    public Text O2_text;
    public Slider[] sliders;
    public void Start()
    {
        Debug.Log(FindObjectOfType<Slider>());
    }
    public void Update()
    {
        
        
        /*
        energy_text.text = energy.ToString() + "/" + max_energy.ToString();
        mining_text.text = Regolith.ToString() + "/" + max_mining.ToString();
        energy_slider.value = energy;
        mining_slider.value = Regolith;
        */
    }
    


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private const string VolumePrefKey = "MasterVolume";

    void Start()
    {
        //Load saved volume, or set to max if not set
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 1f);
        AudioListener.volume = savedVolume;
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }


    //Is called whenever the volume slider value changes, updates the AudioListener volume and saves the new value
    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(VolumePrefKey, value);
        PlayerPrefs.Save();
    }
}

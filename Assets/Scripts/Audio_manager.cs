using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_manager : MonoBehaviour
{
    [SerializeField] AudioClip ui_click;
    [SerializeField] AudioClip Landing;
    [SerializeField] AudioClip failed_ui_click;
    [SerializeField] AudioClip new_unlock;
    [SerializeField] AudioClip ui_open;
    [SerializeField] AudioClip ship_takeoff;
    [SerializeField] AudioClip low_energy_siren;
    public AudioSource SFX;
    public AudioSource Music;
    public AudioSource LowEnergySiren;
    void Start()
    {
        if (!Music.isPlaying)
        {
            Music.Play();
        }
    }
    // credit Alexander Nakarada (CreatorChords)
    // Update is called once per frame
    public void PlayUIclick()
    {
        SFX.PlayOneShot(ui_click);
    }
    public void PlayLanding()
    {
        SFX.Stop();
        SFX.PlayOneShot(Landing);
    }
    public void PlayFailedClick()
    {
        SFX.PlayOneShot(failed_ui_click);
    }
    public void PlayUnlock()
    {
        SFX.PlayOneShot(new_unlock);
    }
    public void PlayOpen()
    {
        SFX.PlayOneShot(ui_open);
    }
    public void PlayLowEnergySiren() {
        LowEnergySiren.loop = true;
        LowEnergySiren.Play();
    }
    public void StopLowEnergySiren() {
        LowEnergySiren.Stop();
    }
    
}

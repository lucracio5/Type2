using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_manager : MonoBehaviour
{
    public AudioClip ui_click;
    public AudioClip Landing;
    public AudioClip failed_ui_click;
    public AudioSource SFX;
    public AudioSource Music;
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
        SFX.PlayOneShot(Landing);
    }
    public void PlayFailedClick()
    {
        SFX.PlayOneShot(failed_ui_click);
    }
}

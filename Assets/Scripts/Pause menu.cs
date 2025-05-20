using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu: MonoBehaviour
{
   public static bool game_is_paused = false;
    public GameObject PauseUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (game_is_paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }


        }
    }
    public void Resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1;
        game_is_paused = false;

    }
    void Pause()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
        game_is_paused =true;
    }
    public void Back_to_menu()
    {
        game_is_paused = false;
        SceneManager.LoadScene("Start Menu");
    }
}

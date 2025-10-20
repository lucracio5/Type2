using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    private Camera mainCamera; // Declare a variable to hold the camera reference
    [SerializeField] private Vector3 CreditsCameraPosition = new Vector3(80, -9, 93);
    [SerializeField] private Vector3 CreditsCameraRotation = new Vector3(15, 45, 0);
    Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }


    void Update()
    {
        Camera.main.transform.position = Vector3.SmoothDamp(
            Camera.main.transform.position,
            CreditsCameraPosition,
            ref velocity,
            smoothTime
        );

        Camera.main.transform.rotation = Quaternion.Slerp(
            Camera.main.transform.rotation,
            Quaternion.Euler(CreditsCameraRotation),
            Time.deltaTime * (1f / smoothTime)
        );
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
        //Save data here
        Debug.Log("Exited Game");
    }



    public void MoveCameraToCredits()
    {
    }
}
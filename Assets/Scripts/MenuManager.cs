using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public bool MoveCamToCredits = false;
    public bool MoveCamToHome = false;

    [SerializeField] private Vector3 OriginalCameraPosition = new Vector3(0f, 2.55f, -8.15f);
    [SerializeField] private Vector3 OriginalCameraRotation = new Vector3(15f, 0f, 0f);

    [SerializeField] private Vector3 CreditsCameraPosition = new Vector3(17.304f, 3.067f, 30.157f);
    [SerializeField] private Vector3 CreditsCameraRotation = new Vector3(0f, 45f, 0f);
    Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Main camera: " + mainCamera.name);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) MoveCamToCredits = !MoveCamToCredits;
        if (Input.GetKeyDown(KeyCode.H)) MoveCamToHome = !MoveCamToHome;


        if (MoveCamToCredits) MoveCamera(CreditsCameraPosition, CreditsCameraRotation);
        if (MoveCamToHome) MoveCamera(OriginalCameraPosition, OriginalCameraRotation);

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


    public void ReturnCamHome()
    {
        Debug.Log("Returning Camera Home");
        MoveCamToCredits = false;
        MoveCamToHome = true;
    }

    void MoveCamera(Vector3 location, Vector3 rotation)
    {
        mainCamera.transform.position = Vector3.SmoothDamp(
            Camera.main.transform.position,
            location,
            ref velocity,
            smoothTime
        );

        mainCamera.transform.rotation = Quaternion.Slerp(
            Camera.main.transform.rotation,
            Quaternion.Euler(rotation),
            Time.deltaTime * (1f / smoothTime)
        );
    }
}
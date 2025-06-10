using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{

    [SerializeField] private Vector3 defaultPosition = new Vector3(150, 35, 0);
    [SerializeField] private Vector3 defaultRotation = new Vector3(20, 180, 0);
    [SerializeField] private float defaultFOV = 60f;

    [SerializeField] private float xMin = 10f;
    [SerializeField] private float xMax = 230f;

    [SerializeField] private float zMin = -200f;
    [SerializeField] private float zMax = 30f;

    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float zoomSpeed = 15f;
    [SerializeField] private float rotationSpeed = 5f;

    [SerializeField] private float minFOV = 12f;
    [SerializeField] private float maxFOV = 92f;

    private float lastMousePosX;
    private float lastMousePosY;

    private float curDeltaX;
    private float curDeltaY;

    // Update is called once per frame
    void Update()
    {
        TrackMouse();

        MoveCamera();
        ZoomCamera();
        RotateCamera();
    }


    void TrackMouse() {
        Vector3 mousePos = Input.mousePosition; // Get the current mouse position in screen coordinates

        //Gets the mouse position relative to the last frame
        curDeltaX = mousePos.x - lastMousePosX;
        curDeltaY = mousePos.y - lastMousePosY;

        //updates the last mouse position
        lastMousePosX = mousePos.x;
        lastMousePosY = mousePos.y;
    }

    // Moves the camera based on mouse input
    void MoveCamera()
    {
        bool cameraIsInBounds = transform.position.x > xMin && transform.position.x < xMax && transform.position.z > zMin && transform.position.z < zMax;
        if (cameraIsInBounds && Input.GetMouseButton(1)) // If the right mouse button is pressed and the camera is within bounds, move the camera
        {
            transform.position = transform.position + new Vector3(curDeltaX * movementSpeed * Time.deltaTime, 0, curDeltaY * movementSpeed * Time.deltaTime);
        }
        else if (!cameraIsInBounds) // If the camera is out of bounds, snap the camera position to within the bounds
        {
            transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, xMin + 1, xMax - 1),
            transform.position.y,
            Mathf.Clamp(transform.position.z, zMin + 1, zMax - 1)
            );
        }

    }


    void ZoomCamera()
    {
        Vector3 scrollDelta = Input.mouseScrollDelta; // Get the mouse scroll delta

        bool cameraZoomIsInBounds = Camera.main.fieldOfView > minFOV && Camera.main.fieldOfView < maxFOV; // Check if the camera zoom is within bounds
        if (cameraZoomIsInBounds)
        {
            Camera.main.fieldOfView = Camera.main.fieldOfView - scrollDelta.y * zoomSpeed; // Adjust the field of view based on the scroll delta
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - scrollDelta.y * zoomSpeed, minFOV, maxFOV); // Clamp the field of view to the specified limits
        }
    }

    void RotateCamera() {
        if (Input.GetMouseButton(2)) // If the scroll wheel button is pressed, rotate the camera
        {
            transform.Rotate(-1f * curDeltaY * rotationSpeed * Time.deltaTime, curDeltaX * rotationSpeed * Time.deltaTime, 0); //rotate the camera about the x and y axis
            //Debug.Log("curDeltaX: " + curDeltaX + ", curDeltaY: " + curDeltaY);
        }
    }

    //resets the camera to its default state
    public void ResetCamera() {
        transform.position = defaultPosition;
        Camera.main.fieldOfView = defaultFOV;
        transform.Rotate(defaultRotation);
    }
}

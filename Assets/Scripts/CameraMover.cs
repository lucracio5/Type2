using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{

    [SerializeField] private float xMin = 10f;
    [SerializeField] private float xMax = 230f;
    [SerializeField] private float zMin = -200f;
    [SerializeField] private float zMax = 30f;
    [SerializeField] private float speed = 10f;
    private float lastMousePosX;
    private float lastMousePosY;



    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition; // Get the current mouse position in screen coordinates

        //Gets the mouse position relative to the last frame
        float deltaX = mousePos.x - lastMousePosX;
        float deltaY = mousePos.y - lastMousePosY;

        bool cameraIsInBounds = transform.position.x > xMin && transform.position.x < xMax && transform.position.z > zMin && transform.position.z < zMax;
        if (cameraIsInBounds && Input.GetMouseButton(1)) // If the right mouse button is pressed and the camera is within bounds, move the camera
        {
            transform.position = transform.position + new Vector3(deltaX * speed * Time.deltaTime, 0, deltaY * speed * Time.deltaTime);
        }
        else if (!cameraIsInBounds) // If the camera is out of bounds, snap the camera position to within the bounds
        {
            transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, xMin + 1, xMax - 1),
            transform.position.y,
            Mathf.Clamp(transform.position.z, zMin + 1, zMax - 1)
            );
        } 

        //updates the last mouse position
        lastMousePosX = mousePos.x;
        lastMousePosY = mousePos.y;
    }
}

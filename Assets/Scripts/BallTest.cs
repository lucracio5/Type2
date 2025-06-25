using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTest : MonoBehaviour
{
    public GameObject ballPrefab;     // Assign your ball prefab in the Inspector
    public float shootForce = 1000f;  // Adjust to taste
    public Vector3 spawnOffset = new Vector3(0, -3, -3);

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Left click
        {
            ShootBall();
        }
    }

    void ShootBall()
    {
        // Generate ray from camera through mouse position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // Instantiate ball at camera position
        GameObject ball = Instantiate(ballPrefab, cam.transform.position + spawnOffset, Quaternion.identity);

        // Ensure the ball has a Rigidbody
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = ball.AddComponent<Rigidbody>();
        }

        // Apply force in ray direction
        rb.AddForce(ray.direction * shootForce);
    }
}
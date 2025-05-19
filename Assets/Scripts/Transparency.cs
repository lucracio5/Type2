using System.Collections;
using UnityEngine;

public class Transparency : MonoBehaviour
{
    private float time;
    public float transparency = 0.2f;
    public bool placed;
    private MeshRenderer meshRenderer;
    private GameObject Gamemanager;



    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        Gamemanager = GameObject.Find("Game Manager");

        if (!placed)
        {
            transparency = 0.2f;
            SetGrayscaleColor(transparency);
        }
        else
        {
            transparency = 1f;
            SetGrayscaleColor(1f);
        }
    }

    void Update()
    {
        time += Time.deltaTime * Gamemanager.GetComponent<Variable_Tracker>().speed;

        if (time > 0.5f)
        {
            if (transparency < 1f && !placed)
            {
                transparency += 0.1f;
            }
            else
            {
                placed = true;
                transparency = 1f;
            }

            SetGrayscaleColor(transparency);
            time = 0f;
        }
    }
    void SetGrayscaleColor(float alpha)
    {
        // Lazily grab the renderer if it hasn't been set yet
        if (meshRenderer == null)
            meshRenderer = GetComponentInChildren<MeshRenderer>();

        if (meshRenderer != null)
        {
            Color c = new Color(alpha, alpha, alpha, alpha);
            meshRenderer.material.color = c;
        }
        else
        {
            Debug.LogError($"Transparency on {gameObject.name} couldn't find a MeshRenderer!");
        }
    }

    public void ForceOpaque()
    {
        // Make sure meshRenderer is initialized here, too
        if (meshRenderer == null)
            meshRenderer = GetComponentInChildren<MeshRenderer>();

        transparency = 1f;
        placed = true;
        SetGrayscaleColor(1f);
    }



    /*

    void SetGrayscaleColor(float alpha)
    {
        Color c = new Color(alpha, alpha, alpha, alpha); // Match drill's grayscale fade
        meshRenderer.material.color = c;
    }

    public void ForceOpaque()
    {
        transparency = 1f;
        placed = true;
        SetGrayscaleColor(1f);
    }

    */
}

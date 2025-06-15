using UnityEngine;

public class UI : MonoBehaviour
{
    public GUISkin customSkin;

    void OnGUI()
    {
        GUI.skin = customSkin;

      /*
        GUI.Box(new Rect(10, 10, 200, 30), "Styled Box");
        if (GUI.Button(new Rect(10, 50, 200, 30), "Styled Button"))
        {
            Debug.Log("Clicked!");
        }
        */
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Threading;

public class ToolTips : MonoBehaviour
{
    public TMP_Text messageText;

    public void displayMessage(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        float alpha = 1f;
        Debug.Log("Pre Sleep");
        Thread.Sleep(2000); //2 Seconds
        Debug.Log("Post Sleep");
        while (alpha != 0)
        {
            messageText.color = new Color(255, 255, 255, alpha-2);
            Thread.Sleep(100); // wait for .1 Seconds
        }
    }
}

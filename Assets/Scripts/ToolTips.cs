using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTips : MonoBehaviour
{
    public TMP_Text messageText;

    public void displayMessage(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        for(int i = 0; i < )
        messageText.color = new Color(255, 255, 255, alpha);
    }
}

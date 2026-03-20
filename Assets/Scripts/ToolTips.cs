using System.Collections;
using TMPro;
using UnityEngine;

public class ToolTips : MonoBehaviour
{
    public TMP_Text messageText;

    public void DisplayMessage(string message)
    {
        StopAllCoroutines(); // stop any running fades
        StartCoroutine(ShowAndFade(message));
    }

    private IEnumerator ShowAndFade(string message)
    {
        // Set initial message
        messageText.text = message;
        messageText.gameObject.SetActive(true);

        // Fully visible
        Color c = messageText.color;
        c.a = 1f;
        messageText.color = c;

        // Wait 2 seconds
        Debug.Log("Pre Wait");
        yield return new WaitForSeconds(2f);
        Debug.Log("Post Wait");

        // Fade out over 1 second
        float duration = 1f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / duration);
            c.a = alpha;
            messageText.color = c;
            yield return null; // wait a frame
        }

        // Hide after fade
        messageText.gameObject.SetActive(false);
    }
}
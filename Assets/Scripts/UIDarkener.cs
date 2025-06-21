//Applies the UI darkener anytime a panel is selected

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDarkener : MonoBehaviour
{
    [SerializeField] private GameObject UiOverlayDarkener;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(UiOverlayDarkener.activeSelf);
        UiOverlayDarkener.SetActive(AnyPanelsOpen());
    }
    
    bool AnyPanelsOpen() {
        GameObject[] allPanels = GameObject.FindGameObjectsWithTag("UIPanel");
        return (allPanels.Length > 0); //if there are any open panels, they will be detected.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imports: MonoBehaviour
{
    public GameObject panel;
    public void openImports()
    {
        panel.SetActive(true);
    }
    public void closeImports()
    {
        panel.SetActive(false);
    }

}

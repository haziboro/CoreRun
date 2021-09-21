using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSettings : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject settings;

    //Toggles between settings and menu
    public void Toggle()
    {
        menu.SetActive(!menu.activeSelf);
        settings.SetActive(!settings.activeSelf);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleHatSelection : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject hatSelection;

    //Toggles between hatSelection and menu
    public void Toggle()
    {
        menu.SetActive(!menu.activeSelf);
        hatSelection.SetActive(!hatSelection.activeSelf);
    }
}

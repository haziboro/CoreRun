using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages enabling/disabling of renderers activating/deactivating Eye objects
public class PlayerGraphicsController : MonoBehaviour
{
    //public GameObject playerGraphic { get; private set; }
    private Renderer[] playerGraphicRenderers;
    [SerializeField] GameObject openEyes;
    [SerializeField] GameObject happyEyes;
    [SerializeField] GameObject squintedEyes;

    //private bool eyesClosed = false;

    // Start is called before the first frame update
    private void Start()
    {
        playerGraphicRenderers = GetComponentsInChildren<Renderer>();
    }

    //Changes the graphic's color
    public void SetGraphicColor(Color color)
    {
        GetComponent<Renderer>().material.SetColor(
                    "_Color", color);
    }

    //Toggles renderer visibility from visible to non visible and vice versa
    public void ToggleRendererVisibility()
    {
        foreach (Renderer renderer in playerGraphicRenderers)
        {
            bool vis = !renderer.isVisible;
            renderer.enabled = vis;
            if (vis) { ToggleAll(); }
            else { ToggleSquint(); }
        }
    }

    //ToggleRendererVisibility overload, sets visibility to the given parameter
    public void ToggleRendererVisibility(bool givenVisibility)
    {
        foreach (Renderer renderer in playerGraphicRenderers)
        {
            renderer.enabled = givenVisibility;
        }
    }

    //Toggles which set of eyes are visible
    private void ToggleAll()
    {
        squintedEyes.SetActive(false);
        openEyes.SetActive(false);
        happyEyes.SetActive(false);
    }
    public void ToggleSquint(bool visible = true)
    {
        squintedEyes.SetActive(visible);
        openEyes.SetActive(false);
        happyEyes.SetActive(false);
    }
    public void ToggleHappy(bool visible = true)
    {
        squintedEyes.SetActive(false);
        openEyes.SetActive(false);
        happyEyes.SetActive(visible);
    }
    public void ToggleOpen(bool visible = true)
    {
        squintedEyes.SetActive(false);
        openEyes.SetActive(visible);
        happyEyes.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShrinkPowerBarUI : MonoBehaviour
{
    private Image shrinkBarImage;
    private Vector3 shrinkBarScale;

    [SerializeField] GameObject shrinkBar;
    [SerializeField] ShrinkPower shrinkPower;
    [SerializeField] Color shrinkPowerBarHigh = Color.green;
    [SerializeField] Color shrinkPowerBarMedium = Color.yellow;
    [SerializeField] Color shrinkPowerBarLow = Color.red;
    [SerializeField] Color shrinkPowerBarOnCooldown = Color.black;

    private void Awake()
    {
        shrinkPower.value = 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        shrinkBarImage = shrinkBar.GetComponent<Image>();

        //Initialize shrinkBar's scaling
        shrinkBarScale = new Vector3(shrinkBar.transform.localScale.x,
            shrinkBar.transform.localScale.y,
            shrinkBar.transform.localScale.z);
        shrinkBar.GetComponent<Image>().color = shrinkPowerBarHigh;
    }

    // Update is called once per frame
    void Update()
    {
        ModifyShrinkBar();
    }

    //Changes scale of shrink bar and its' color according to parameter
    public void ModifyShrinkBar()
    {
        shrinkBarScale.x = shrinkPower.value / 100;
        shrinkBar.transform.localScale = shrinkBarScale;

        ChangePowerBarColor(shrinkPower.value / 100);
    }

    //Changes shrink power bar's color based on given Input
    public void ChangePowerBarColor(float barValue)
    {
        if (!shrinkPower.onCooldown)
        {
            if (barValue < 0.3f)
            {
                shrinkBarImage.color = shrinkPowerBarLow;
            }
            else if (barValue < 0.67f)
            {
                shrinkBarImage.color = shrinkPowerBarMedium;
            }
            else
            {
                shrinkBarImage.color = shrinkPowerBarHigh;
            }//endelse
        }//endif
        else
        {
            shrinkBarImage.color = shrinkPowerBarOnCooldown;
        }//endelse
    }
}

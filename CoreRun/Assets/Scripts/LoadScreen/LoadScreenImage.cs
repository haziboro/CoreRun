using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreenImage : MonoBehaviour
{
    private Image loadScreen;

    [SerializeField] GameEvent colorLoadText;
    [SerializeField] GameObject loadScreenObj;
    [SerializeField] Sprite loadScreenImage;
    [SerializeField] Color loadScreenColor;
    [SerializeField] int imageHeight;
    [SerializeField] int imageWidth;

    // Start is called before the first frame update
    void Awake()
    {
        loadScreen = loadScreenObj.GetComponent<Image>();

        loadScreen.sprite = loadScreenImage;
        loadScreen.color = loadScreenColor;
        loadScreen.GetComponent<RectTransform>().sizeDelta =
            new Vector2(imageHeight, imageWidth);
    }

    private void Start()
    {
        colorLoadText.Raise();
    }

}

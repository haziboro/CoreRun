using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreenController : MonoBehaviour
{
    private Vector3 loadScreenOrigin;
    private float loadScreenHeight;
    private bool moving = false;
    private Vector3 currentPos;

    [SerializeField] ScriptableBool loadScreen;
    [SerializeField] GameObject loadScreenObj;
    [SerializeField] float loadScreenSpeed;



    // Start is called before the first frame update
    void Awake()
    {
        loadScreenObj.SetActive(true);
        loadScreen.active = true;

        //Set movement points
        loadScreenOrigin = loadScreenObj.transform.position;
        loadScreenHeight = loadScreenObj.transform.
            GetComponent<RectTransform>().sizeDelta.y;

        UnfillLoadScreen();
    }

    public void FillLoadScreen()
    {
        if (!moving)
        {
            StartCoroutine(FillTimer());
        }
    }

    public void UnfillLoadScreen()
    {
        if (!moving)
        {
            StartCoroutine(UnfillTimer());
        }
    }

    IEnumerator FillTimer()
    {
        moving = true;
        while (!loadScreen.active)
        {
            currentPos = loadScreenObj.transform.position;
            currentPos.y -= (loadScreenSpeed * Time.deltaTime);

            //move from off screen to current position
            loadScreenObj.transform.position = currentPos;

            if (currentPos.y <=
                loadScreenOrigin.y)
            {
                loadScreen.active = true;
            }
            yield return null;
        }
        moving = false;
    }

    IEnumerator UnfillTimer()
    {
        moving = true;
        while (loadScreen.active)
        {
            currentPos = loadScreenObj.transform.position;
            currentPos.y += (loadScreenSpeed * Time.deltaTime);

            //move from current position to off screen
            loadScreenObj.transform.position = currentPos;

            if (currentPos.y >=
                loadScreenOrigin.y + loadScreenHeight)
            {
                loadScreen.active = false;
            }
            yield return null;
        }
        moving = false;
    }



    

}

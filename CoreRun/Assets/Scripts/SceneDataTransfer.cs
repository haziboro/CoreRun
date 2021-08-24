using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDataTransfer : MonoBehaviour
{
    public static SceneDataTransfer instance;
    //Default Values
    public float backgroundMusicVolume = 0.25f;
    public float soundEffectsVolume = 0.25f;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

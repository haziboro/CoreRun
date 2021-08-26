using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SceneDataTransfer : MonoBehaviour
{
    public static SceneDataTransfer instance;
    //Default Values
    public float backgroundMusicVolume = 0.25f;
    public float soundEffectsVolume = 0.25f;
    public int highScore = 0;

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
        LoadScore();
    }

    public void CheckHighScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            SaveScore();
        }
    }

    //Stores the player score
    [System.Serializable]
    public class playerData
    {
        public int highScore;
    }

    //Saves the player's high score
    public void SaveScore()
    {
        playerData data = new playerData();
        data.highScore = highScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    //Loads the player's highest score
    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            playerData data = JsonUtility.FromJson<playerData>(json);
            highScore = data.highScore;
        }
        else
        {
            highScore = 0;
        }
    }

}

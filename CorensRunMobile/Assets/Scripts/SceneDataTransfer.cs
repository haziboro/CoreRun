using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SceneDataTransfer : MonoBehaviour
{
    [SerializeField] ScoreAndLayer score;
    [SerializeField] HighScore savedScore;

    // Start is called before the first frame update
    void Awake()
    {
        LoadScore();
        score.score = 0;
        score.layer = 1;
    }

    public void CheckHighScore()
    {
        if (score.score > savedScore.highScore)
        {
            savedScore.highScore = score.score;
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
        data.highScore = savedScore.highScore;
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
            savedScore.highScore = data.highScore;
        }
        else
        {
            savedScore.highScore = 0;
        }
    }

}

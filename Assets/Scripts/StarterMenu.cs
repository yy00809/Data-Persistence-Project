using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarterMenu : MonoBehaviour
{
    public static StarterMenu Instance;
    public TMP_InputField inputField;
    public string playerName;
    public int playerScore;
    public int bestScore;
    public string bestPlayerName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadScore();
       
    }

    private void Start()
    {
        inputField.onEndEdit.AddListener(SaveName);
    }


    public void StartNew()
    {
        SceneManager.LoadScene(1);
    } 

    public void SaveName(string input)
    {
        playerName = input;
    }


    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int score;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        if (bestPlayerName==null)
        {
            data.playerName = playerName;
            data.score = playerScore;
        } else
        {
            if (playerScore >= bestScore)
            {
                data.playerName = playerName;
                data.score = playerScore;
            } else
            {
                return;
            }
        }

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerName = data.playerName;
            bestScore = data.score;
        }
    }

}

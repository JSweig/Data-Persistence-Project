using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public static DataManager Instance;

    public string currentPlayer;
    public string[] playerNames = { "name", "name", "name" };
    public int[] highScores = { 0, 0, 0 };
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScores();
    }

    [System.Serializable]
    class SaveData
    {
        public string[] names = new string[3];
        public int[] highScores = new int[3];
    }

    public void SaveScores()
    {
        SaveData data = new SaveData();
        data.highScores = highScores;
        data.names = playerNames;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScores = data.highScores;
            playerNames = data.names;
        }
    }

    public void ClearScores()
    {
        highScores[0] = 0;
        highScores[1] = 0;
        highScores[2] = 0;
        playerNames[0] = "name";
        playerNames[1] = "name";
        playerNames[2] = "name";
        SaveScores();
    }

}

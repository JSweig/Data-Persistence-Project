using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuHandler : MonoBehaviour
{
    public TMP_InputField playerName;
    private void Start()
    {

    }

    public void StartNew()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "main menu")
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }

    }

    public void NewGame()
    {
        DataManager.Instance.currentPlayer = playerName.GetComponent<TMP_InputField>().text;
        SceneManager.LoadScene(1);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    public void HighScores()
    {
            SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        DataManager.Instance.SaveScores();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void ResetScores()
    {
        DataManager.Instance.ClearScores();
    }
}

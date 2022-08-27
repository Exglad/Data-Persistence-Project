using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;
using System.IO;

public class MainUIManager : MonoBehaviour
{

    //public static MainUIManager Instance;
    //public static MainUIManager Instance;
    public static string playerName;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        //MainManager.Instance.SaveColor();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
    public void PlayerName(string value)
    {
        MainManager2.Instance.playerName = value;
    }

}
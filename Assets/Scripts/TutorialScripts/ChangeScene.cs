using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string UIScene = "UI&Handlers";
    public void ChangeToScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
        if(nextScene != "Main_Menu")
            SceneManager.LoadScene(UIScene, LoadSceneMode.Additive);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeRandomScene : MonoBehaviour
{  
    public int levelGenerate; 
    public string[] LevelsToGo;
    public string UIScene = "UI&Handlers";

    public void LoadRandomLevel()
    {
        levelGenerate = Random.Range(0, LevelsToGo.Length);
        SceneManager.LoadScene(LevelsToGo[levelGenerate]);
        SceneManager.LoadScene(UIScene, LoadSceneMode.Additive);
    }
}

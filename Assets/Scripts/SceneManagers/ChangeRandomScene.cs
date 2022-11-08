using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeRandomScene : MonoBehaviour
{  
    public int levelGenerate; 
    public string[] LevelsToGo;
    public string UIScene = "UI&Handlers";
    Scene scene = SceneManager.GetActiveScene();
    public void LoadRandomLevel()
    {
        while(LevelsToGo[levelGenerate] == scene.name){
            levelGenerate = Random.Range(0,LevelsToGo.Length);
        }
        SceneManager.LoadScene(LevelsToGo[levelGenerate]);
        SceneManager.LoadScene(UIScene, LoadSceneMode.Additive);
    }
}

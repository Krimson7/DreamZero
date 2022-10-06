using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class backToMainMenu : MonoBehaviour
{
    public Options options;
    public string mainMenuSceneName = "Main_Menu";
    public void toMainMenu(){
        options.unPause();
        SceneManager.LoadScene(mainMenuSceneName);
    }
}

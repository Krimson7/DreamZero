using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        //Called when play button is pressed
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        //Load scene (build index can be customized in the build settings). this line loads next index scene.

        SceneManager.LoadScene("Scenes/SandSlashScene");
        // can also use string name of the scene
    }

    public void ExitGame(){
        Debug.Log("Exit game");
        Application.Quit(); // this only works on builded game
    }
}

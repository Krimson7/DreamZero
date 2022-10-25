using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeRandomScene : MonoBehaviour
{  
    public int levelGenerate; 

    public void LoadRandomLevel()
    {
        levelGenerate = Random.Range(1, 5);
        SceneManager.LoadScene(levelGenerate);
    }
}

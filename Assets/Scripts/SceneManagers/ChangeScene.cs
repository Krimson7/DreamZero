
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public SceneCounter sceneCounter;
    int randomIndex;

    public void ChangeToScene(int nextScene)
    {
        SceneManager.LoadScene(nextScene);
        
    }
    
    public void LoadRandomLevelFromPool()
    {
        sceneCounter.LoadRandomLevelFromPool();
    }
    public void LoadBossStage()
    {
        sceneCounter.loadBossStage();

    }
}

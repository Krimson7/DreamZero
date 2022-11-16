using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "DreamZero/SceneCounter")]
public class SceneCounter : ScriptableObject
{
    public List<string> levelList;
    public int randomIndex;
    public List<string> levelsToGo;
    public string UIScene = "UI&Handlers";
    public string bossStage = "BossStage";

    public void loadScene(int levelIndex)
    {
        if(levelIndex >= levelList.Count)
        {
            Debug.Log("Level index out of range");
            return;
        }
        SceneManager.LoadScene(levelList[levelIndex]);
        SceneManager.LoadScene(UIScene, LoadSceneMode.Additive);
    }

    public void loadRandomLevel()
    {
        randomIndex = Random.Range(0, levelList.Count);
        SceneManager.LoadScene(levelList[randomIndex]);
        SceneManager.LoadScene(UIScene, LoadSceneMode.Additive);
    }

    public void LoadRandomLevelFromPool()
    {
        if (levelsToGo.Count == 0)
        {
            loadBossStage();
            return;
        }
        randomIndex = Random.Range(0, levelsToGo.Count);
        Debug.Log("Loading level: " + levelsToGo[randomIndex]);
        Debug.Log("Levels to go: " + randomIndex);
        SceneManager.LoadScene(levelsToGo[randomIndex]);
        SceneManager.LoadScene(UIScene, LoadSceneMode.Additive);
        levelsToGo.RemoveAt(randomIndex);

        
    }

    public void resetScenePool()
    {
        levelsToGo = new List<string>(levelList);
    }

    public void addScenePool(string sceneName)
    {
        levelsToGo.Add(sceneName);
    }

    public void loadBossStage()
    {
        resetScenePool();
        SceneManager.LoadScene(bossStage);
        SceneManager.LoadScene(UIScene, LoadSceneMode.Additive);
    }

}

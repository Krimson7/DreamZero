using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour, I_GameOverListener
{
    public GameObject gameOverUI;
    // Start is called before the first frame update
    public PlayerStats ps;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    { 
        ps.AddGameOverListener(this); 
    }

    public void OnDisable()
    { 
        ps.RemoveGameOverListener(this); 
    }

    public void OnGameOver()
    {
        gameOver();
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("UI&Handlers", LoadSceneMode.Additive);
        ps.Reset();
    }

    public void mainMenu(){
        SceneManager.LoadScene("Main_Menu");
    }
}

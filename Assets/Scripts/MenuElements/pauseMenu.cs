using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    PlayerInput _playerInput;
    public GameObject pausePage;
    public string sceneName;
    // public bool paused = false;
    void onPauseKeyDown (InputAction.CallbackContext context){
        // paused = context.ReadValueAsButton();
        pause();
    }
    
    void Awake(){
        _playerInput = new PlayerInput();
        _playerInput.CharacterControls.Pause.started += onPauseKeyDown;
    }

    public void pause(){
        pausePage.SetActive(!pausePage.activeSelf);
    }

    public void back2mainMenu(){
        SceneManager.LoadScene(sceneName);
    }

    void OnEnable()
    {
        _playerInput.Enable();
    }

    void OnDisable()
    {
        _playerInput.Disable();
    }
}

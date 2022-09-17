using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    PlayerInput _playerInput;

    public Options options;

    // public bool paused = false;
    void onPauseKeyDown (InputAction.CallbackContext context){
        pause();
    }
    
    void Awake(){
        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }

    public void pause(){
        if(options != null)
            options.pause();
    }


    void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.CharacterControls.Pause.started += onPauseKeyDown;
    }

    void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.CharacterControls.Pause.started -= onPauseKeyDown;
    }
    private void OnApplicationFocus(bool focusStatus) {
        _playerInput.Enable();
        _playerInput.CharacterControls.Pause.started += onPauseKeyDown;
    }
    private void OnApplicationPause(bool pauseStatus) {
        _playerInput.Disable();
        _playerInput.CharacterControls.Pause.started -= onPauseKeyDown;
    }

    private void OnLostFocus() {
        _playerInput.Disable();
        _playerInput.CharacterControls.Pause.started -= onPauseKeyDown;
    }

    private void OnFocus() {
        _playerInput.Enable();
        _playerInput.CharacterControls.Pause.started += onPauseKeyDown;
    }
}

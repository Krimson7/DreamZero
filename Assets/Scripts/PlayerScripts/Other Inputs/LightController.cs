using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;


public class LightController : MonoBehaviour
{
    PlayerInput _playerInput;

    void toggleLights(InputAction.CallbackContext context){
        GetComponent<Light2D>().enabled = !GetComponent<Light2D>().enabled;
    }

    void Awake()
    {
        
        _playerInput = new PlayerInput();   
        _playerInput.CharacterControls.Lights.started += toggleLights; 
    }

}

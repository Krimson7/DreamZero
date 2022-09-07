using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;


public class LightController : MonoBehaviour
{
    PlayerInput playerInput;
    GameObject light2D;

    void toggleLights(InputAction.CallbackContext context){
        light2D.SetActive(!light2D.activeSelf);
        // print("toggled lights");
    }

    void Awake()
    {
        light2D = transform.Find("PlayerLight").gameObject;
        playerInput = new PlayerInput();   
        playerInput.CharacterControls.Lights.started += toggleLights; 
    }

    void OnEnable()
    {
        playerInput.Enable();
    }

    void OnDisable()
    {
        playerInput.Disable();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    PlayerInput _playerInput;
    public bool interactKeyDown;
    public bool requireNewInteractPress=true;
    // public bool interactKeyPressed;
    // public bool isInRange = false;
    // public UnityEvent interactAction;
    public Collider2D playerCollider;


    void onInteractKeyDown(InputAction.CallbackContext context){
        interactKeyDown = true;
    }
    void onInteractKeyUp(InputAction.CallbackContext context){
        interactKeyDown = false;
        requireNewInteractPress = false;
    }
    

    void awake(){
        _playerInput = new PlayerInput();
        _playerInput.CharacterControls.Interact.started += onInteractKeyDown;
        _playerInput.CharacterControls.Interact.canceled += onInteractKeyUp;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other) {
        requireNewInteractPress = true;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(interactKeyDown && requireNewInteractPress){
            var interactable = other.GetComponent<I_interactable>();
            if(interactable == null) return;
            // interactable.Interact(this);
        }
    }
}

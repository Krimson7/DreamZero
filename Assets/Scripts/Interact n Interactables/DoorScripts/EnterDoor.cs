using UnityEngine;
using UnityEngine.Events;

public class EnterDoor : MonoBehaviour, I_interactable
{
    public UnityEvent changeScene;

    public void Interact(playerStateMachine psm){
        changeScene.Invoke();
    }


}

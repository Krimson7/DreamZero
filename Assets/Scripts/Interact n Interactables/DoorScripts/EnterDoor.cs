using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour, I_interactable
{
    public string sceneName;

    public void Interact(playerStateMachine psm){
        SceneManager.LoadScene(sceneName);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritChangeStatue : MonoBehaviour, I_interactable
{
    public string spiritName;
    public Animator animator;
    public Player spirit;
    // Start is called before the first frame update
    void Start()
    {
        spiritName = spirit.spiritName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            animator.SetBool("IsClose", true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            animator.SetBool("IsClose", false);
        }
    }

    public void Interact(playerStateMachine psm){
        if(spirit != psm.characterHolder.GetComponent<playerUseSpirit>().player){
            Change(psm);
        }
    }

    public void Change(playerStateMachine psm){
        playerUseSpirit pus = psm.characterHolder.GetComponent<playerUseSpirit>();
        pus.changeInto(spirit);
        // Debug.Log("Changed into " + spiritName);
    }
}

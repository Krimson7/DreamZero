using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritChangeStatue : MonoBehaviour, I_interactable
{
    public Player spirit;
    // [Header("Below are automatically matched to Spirit object")]
    private string spiritName;
    private Animator animator;
    public PlayerStats stats;
    
    // Start is called before the first frame update
    void Start()
    {
        spiritName = spirit.spiritName;
        animator = this.GetComponent<Animator>();
        if(animator != null)
            animator.runtimeAnimatorController = spirit.animatorController;
    }

    // Update is called once per frame
    void Update()
    {
        // animator.runtimeAnimatorController = spirit.AnimatorPrefab.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            // animator.SetBool("IsClose", true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            // animator.SetBool("IsClose", false);
        }
    }

    public void Interact(playerStateMachine psm){
        if(spirit.name != stats.playerForm.name){
            stats.changeFormTo(spirit);
        }
    }

    public void Change(playerStateMachine psm){
        playerUseSpirit pus = psm.characterHolder.GetComponent<playerUseSpirit>();
        pus.changeInto(spirit);
        // Debug.Log("Changed into " + spiritName);
    }
}

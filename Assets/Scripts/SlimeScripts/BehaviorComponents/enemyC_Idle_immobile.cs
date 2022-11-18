using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyC_Idle_immobile : MonoBehaviour, I_enemyIdle
{

    Rigidbody2D rb;
    public AnimationClip Anim;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    
    public string Idle(Animator animator, bool playerDetected)
    {
        if(playerDetected){
            return "Go Attack";
        }
        // Debug.Log("Idle of peashooter");
        //idle
        animator.Play(Anim.name);
        rb.velocity = new Vector2(0, rb.velocity.y);
        return "No changes";
    }
}
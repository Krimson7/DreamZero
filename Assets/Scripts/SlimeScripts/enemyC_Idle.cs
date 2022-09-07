using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyC_Idle : MonoBehaviour, I_enemyIdle
{

    float timer = 0f;
    public float idleTime = 10f;
    Rigidbody2D rb;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    public string Idle(Animator animator, bool playerInFront)
    {
        if(playerInFront){
            timer = 0f;
            return "Go Attack";
        }
        if(timer >= idleTime){
            timer = 0f;
            return "Go Wander";
        } 
        //idle
        timer += Time.fixedDeltaTime;
        animator.Play("Slime_Idle");
        rb.velocity = new Vector2(0, rb.velocity.y);
        return "No changes";
    }
}
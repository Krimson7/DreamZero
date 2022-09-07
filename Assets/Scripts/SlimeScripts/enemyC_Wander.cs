using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyC_Wander : MonoBehaviour, I_enemyWander
{
    Rigidbody2D rb;
    int fcr;
    public float walkSpeed =50f;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    public string Wander(Animator animator, bool playerInFront, bool checkWall, bool checkPitfall)
    {
        if(playerInFront) return "Go Attack";
        if(checkWall) Flip();
        if(checkPitfall){
            return "Go Idle, q flip";
        }
        
        //wander
        animator.Play("Slime_Walk");
        fcr = transform.localScale.x > 0 ? 1 : -1;
        rb.velocity = new Vector2(fcr * walkSpeed * Time.fixedDeltaTime, rb.velocity.y); 
        return "No changes";
    }

    void Flip(){
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
}
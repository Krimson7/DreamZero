using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyC_Knockedback : MonoBehaviour, I_enemyKnockedback
{

    float timer = 0f;
    public float knockedTime = 1f;
    Rigidbody2D rb;
    bool isKnocked = false;
    public float hitForce = 10f;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    public string Knocked_back(Animator animator, Vector2 HitDirection)
    {
        if(!isKnocked){
            timer = 0f;
            rb.velocity = new Vector2(0f, 0f);
            rb.AddForce(hitForce * ((Vector2)transform.position - HitDirection).normalized, ForceMode2D.Impulse);
            rb.AddForce(new Vector2(0f, hitForce), ForceMode2D.Impulse);
            isKnocked = true;
        }
        if(timer >= knockedTime){
            timer = 0f;
            isKnocked = false;
            return "Go Idle";
        } 
        //idle
        timer += Time.fixedDeltaTime;
        animator.Play("Slime_Jump");
        // rb.velocity = new Vector2(0, rb.velocity.y);
        return "No changes";
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyC_MeleeAttack : MonoBehaviour, I_enemyAttack
{
    Rigidbody2D rb;
    public float atk;
    float timer;
    public float startTime;
    float speed;
    public float maxSpeed;
    public float acceleration;

    public int facingRight = 1;
    public AnimationClip Anim;


    enum attackingState {start, attack, end};
    attackingState state;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    public string Attack(Animator animator, Collider2D playerInRange)
    {
        if(playerInRange == null){
            timer = 0;
            speed = 0;
            state = attackingState.start;
            return "Go Idle";
        }
        // print("atking");
        
        facingRight = transform.localScale.x > 0 ? 1 : -1;

        if((playerInRange.transform.position.x > transform.position.x) ^ (transform.localScale.x > 0)){
            print("will charge");
        }
        switch(state){
            case attackingState.start:
                if(timer <= startTime){
                    timer += Time.fixedDeltaTime;
                    animator.Play("Slime_Landing");
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    return "No changes";
                }
                else{
                    timer = 0f;
                    speed = 0f;
                    state = attackingState.attack;
                    return "No changes";
                }
            case attackingState.attack:
                animator.Play("Slime_Jump");
                if(speed < maxSpeed){
                    speed += acceleration * Time.fixedDeltaTime;
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                return "No changes";
            case attackingState.end:
                // state = attackingState.start;
                // return "Go Idle";
            default:
                return "No changes";
        }
    }
}
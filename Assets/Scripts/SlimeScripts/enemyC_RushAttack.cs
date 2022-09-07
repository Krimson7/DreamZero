using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyC_RushAttack : MonoBehaviour, I_enemyAttack
{
    Rigidbody2D rb;
    Collider2D playerCharged;
    Transform chargeHitPoint;
    public Vector2 chargeHitSize;
    public LayerMask playerLayer;
    bool playerInChargeZone = false;
    
    public float atk;
    float timer;
    public float startTime;
    float speed;
    public float maxSpeed;
    public float acceleration;
    public float recoil = 100f;

    public bool hitplayer = false;

    int facingRight = 1;


    enum attackingState {start, attack, end};
    attackingState state;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        chargeHitPoint = transform.Find("ChargePoint");
        if(chargeHitPoint == null){
            Debug.LogError("ChargePoint not found, please add one to use rush behavior");
        }
    }
    void Update()
    {
        
        playerCharged = Physics2D.OverlapBox(chargeHitPoint.position, chargeHitSize, 0f, playerLayer);
        playerInChargeZone = playerCharged? true:false;    
    }

    public string Attack(Animator animator, Collider2D playerInRange)
    {
        if(playerInRange == null){
            reset();
            return "Go Idle";
        }
        // print("atking");
        
        facingRight = transform.localScale.x > 0 ? 1 : -1;

        if((playerInRange.transform.position.x > transform.position.x) ^ (transform.localScale.x > 0)){
            // print("will charge");
            reset();
            return "Go Idle";
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
                animator.Play("Slime_Walk");
                if(speed < maxSpeed){
                    speed += facingRight * acceleration * Time.fixedDeltaTime;
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                if(playerInChargeZone){
                    state = attackingState.end;
                    return "No changes";
                }
                return "No changes";

            case attackingState.end:
                animator.Play("Slime_Jump");
                // rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
                if(!hitplayer){
                    playerCharged.gameObject.GetComponent<playerStateMachine>().checkTakeDamage(atk, transform.position - playerCharged.transform.position);
                    rb.velocity = new Vector2(0, rb.velocity.y); 
                    rb.AddForce(new Vector2(recoil, recoil*2), ForceMode2D.Impulse);
                    hitplayer = true;
                }
                Invoke("reset", 0.5f);
                return "No changes";
            default:
                return "No changes";
        }
    }

    void reset(){
        timer = 0;
        speed = 0;
        hitplayer = false;
        state = attackingState.start;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = playerCharged? Color.green : Color.red;
        Gizmos.DrawWireCube((Vector3)chargeHitPoint.position, chargeHitSize);
    }
}
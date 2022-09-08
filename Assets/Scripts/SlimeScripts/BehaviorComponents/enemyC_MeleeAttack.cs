using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyC_MeleeAttack : MonoBehaviour, I_enemyAttack
{
    Rigidbody2D rb;
    Collider2D playerCharged;
    Transform hitPoint;
    public Vector2 chargeHitSize;
    public LayerMask playerLayer;
    bool playerInChargeZone = false;
    public AnimationClip chargeAnim;
    public AnimationClip rushAnim;
    public AnimationClip recoilAnim;
    
    
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
        hitPoint = transform.Find("ChargePoint");
        if(hitPoint == null){
            Debug.LogError("ChargePoint not found, please add one to use rush behavior");
        }
    }
    void Update()
    {
        
        playerCharged = Physics2D.OverlapBox(hitPoint.position, chargeHitSize, 0f, playerLayer);
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
                    animator.Play(chargeAnim.name);
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
                animator.Play(rushAnim.name);
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
                animator.Play(recoilAnim.name);
                // rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
                if(!hitplayer){
                    playerCharged.gameObject.GetComponent<playerStateMachine>().checkTakeDamage(atk, transform.position - playerCharged.transform.position);
                    rb.velocity = new Vector2(0, rb.velocity.y); 
                    rb.AddForce(new Vector2(recoil * -facingRight, recoil*2), ForceMode2D.Impulse);
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
        Gizmos.DrawWireCube((Vector3)hitPoint.position, chargeHitSize);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyC_MeleeAttack : MonoBehaviour, I_enemyAttack
{
    Rigidbody2D rb;
    Collider2D playerCharged;
    public Transform chargeHitPoint;
    public Vector2 chargeHitSize;
    public LayerMask playerLayer;
    bool playerInChargeZone = false;
    public AnimationClip chargeAnim;
    public AnimationClip AttackAnim;
    
    
    public float atk;
    float timer;
    public float startTime = 3f;
    public float attackDelay = 10f;
    public float recoil = 100f;

    public bool hitplayer = false;

    int facingRight = 1;


    public enum attackingState {start, attack, end};
    public attackingState state;

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
            return "Go Wander";
        }
        // print("atking");
        
        facingRight = transform.localScale.x > 0 ? 1 : -1;

        if((playerInRange.transform.position.x > transform.position.x) ^ (transform.localScale.x > 0)){
            reset();
            return "Go Wander";
        }
        switch(state){
            case attackingState.start:
                // Debug.Log("start");
                animator.Play(chargeAnim.name);
                if(timer <= startTime){
                    timer += Time.fixedDeltaTime;
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    return "No changes";
                }
                else{
                    timer = 0f;
                    state = attackingState.attack;
                    return "No changes";
                }
            case attackingState.attack:
                // Debug.Log("attacking");
                animator.Play(AttackAnim.name);
                if(playerInChargeZone){
                    
                    state = attackingState.end;
                    return "No changes";
                }
                return "No changes";

            case attackingState.end:
                // Debug.Log("end");
                // animator.Play(recoilAnim.name);
                // rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
                if(!hitplayer){
                    if(playerInRange != null){
                        playerCharged.GetComponent<playerStateMachine>().checkTakeDamage(atk, transform.position - playerCharged.transform.position);
                        Debug.Log("hit player");
                    }
                    timer=0;
                    hitplayer = true;
                }
                // Debug.Log("delay:" + timer);
                if(timer <= attackDelay){
                    timer += Time.fixedDeltaTime;
                }
                else{
                    reset();
                    Debug.Log("reset");
                    return "Go Wander";
                }
                return "No changes";
            default:
                return "No changes";
        }
    }

    void reset(){
        timer = 0;
        hitplayer = false;
        state = attackingState.start;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = playerCharged? Color.green : Color.red;
        Gizmos.DrawWireCube((Vector3)chargeHitPoint.position, chargeHitSize);
    }
}
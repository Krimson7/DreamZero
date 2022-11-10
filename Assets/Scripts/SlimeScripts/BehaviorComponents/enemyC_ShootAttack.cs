using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyC_ShootAttack : MonoBehaviour, I_enemyAttack
{
    Rigidbody2D rb;
    Collider2D playerCharged;
    public Transform chargeHitPoint;
    public Vector2 chargeHitSize;
    public LayerMask playerLayer;
    // public AnimationClip chargeAnim;
    public AnimationClip AttackAnim;
    
    
    public float atk;

    public bool hitplayer = false;

    int facingRight = 1;


    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        chargeHitPoint = transform.Find("ChargePoint");
        if(chargeHitPoint == null){
            Debug.LogError("ChargePoint not found, please add one to use rush behavior");
        }
    }
    // void Update()
    // {
    //     playerCharged = Physics2D.OverlapBox(chargeHitPoint.position, chargeHitSize, 0f, playerLayer);
    //     playerInChargeZone = playerCharged? true:false;    
    // }

    public string Attack(Animator animator, Collider2D playerInRange)
    {
        if(playerInRange == null){
            reset();
            return "Go Idle";
        }
        print("atking (shoot)");
        
        facingRight = transform.localScale.x > 0 ? 1 : -1;

        if((playerInRange.transform.position.x > transform.position.x) ^ (transform.localScale.x > 0)){
            print("flipping now");
            reset();
            //flip
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            return "No changes";
        }

        return "No changes";
    }

    void reset(){
        hitplayer = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = playerCharged? Color.green : Color.red;
        Gizmos.DrawWireCube((Vector3)chargeHitPoint.position, chargeHitSize);
    }
}
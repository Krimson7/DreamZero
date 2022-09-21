using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Idle01 : MonoBehaviour, I_BossIdle
{
    //first boss idle state
    Rigidbody2D rb;
    public AnimationClip Anim;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    public string Idle(Animator animator, Collider2D PlayerInRange)
    {
        if(PlayerInRange != null){
            return "Go Attack";
        }
        animator.Play(Anim.name);
        rb.velocity = new Vector2(0, rb.velocity.y);
        return "No changes";
    }
}

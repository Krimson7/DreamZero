using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Baldy : MonoBehaviour, IplayerAttackState, IplayerAirAttackState, IAnimatorControl
{
    public GameObject A1_hitbox;
    public ContactFilter2D CF2;
    public Animator animator;
    void start(){
        CF2.SetLayerMask(LayerMask.GetMask("Enemy"));
    }

    public void Attack(float damage){
        animator.Play("Player_Attack");
        A1_hitbox.SetActive(true);

        List<Collider2D> hitEnemies = new List<Collider2D>();
        Physics2D.OverlapCollider(A1_hitbox.GetComponent<BoxCollider2D>(), CF2 , hitEnemies);
        
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<enemyHp>().takeDamage(damage);
        }
        A1_hitbox.SetActive(false);
    }
    public void AirAttack(float damage){
        animator.Play("Player_Attack");
        A1_hitbox.SetActive(true);

        List<Collider2D> hitEnemies = new List<Collider2D>();
        Physics2D.OverlapCollider(A1_hitbox.GetComponent<BoxCollider2D>(), CF2 , hitEnemies);
        
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<enemyHp>().takeDamage(damage);
        }
        A1_hitbox.SetActive(false);
    }
    public void animate(string ani){
        switch(ani){
            default:
            case("Idle"):
                animator.Play("Player_Idle");
                break;
            case("Run"):
                animator.Play("Player_Running");
                break;
            case("Jump"):
                animator.Play("Player_Jump");
                break;
            case("Fall"):
                animator.Play("Player_Fall");
                break;
            case("WallSlide"):
                animator.Play("Player_WallSlide");
                break;
            // case("WallJump"):
            //     animator.Play("Player_WallJump");
            //     break;
        }
    }

}

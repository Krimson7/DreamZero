using System.ComponentModel.Design.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUseSpirit : MonoBehaviour, IplayerAttackState, IplayerAirAttackState, IAnimatorControl, IplayerParryState
{
    public Player player;
    public Player player2;
    public GameObject A1_hitbox;
    public ContactFilter2D CF2;


    public GameObject characterAnimator;

    [Header("Animations")]
    public Animator animator;

    // void awake(){

    // }

    void start(){
        
        CF2.SetLayerMask(LayerMask.GetMask("Enemy"));
        // animator = player.animator;
        // A1_hitbox = player.MeleeHitBox;
    }

    public void Attack(float damage){
        // Debug.Log(player.attack.name);
        animator.Play(player.attack.name);
        A1_hitbox.SetActive(true);

        List<Collider2D> hitEnemies = new List<Collider2D>();
        Physics2D.OverlapCollider(A1_hitbox.GetComponent<BoxCollider2D>(), CF2 , hitEnemies);
        
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<enemyHp>().takeDamage(damage);
        }
        A1_hitbox.SetActive(false);
    }
    public void AirAttack(float damage){
        animator.Play(player.airAttack.name);
        A1_hitbox.SetActive(true);

        List<Collider2D> hitEnemies = new List<Collider2D>();
        Physics2D.OverlapCollider(A1_hitbox.GetComponent<BoxCollider2D>(), CF2 , hitEnemies);
        
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<enemyHp>().takeDamage(damage);
        }
        A1_hitbox.SetActive(false);
    }

    public void Parry(){
        animator.Play(player.parry.name);
    }

    public void animate(string ani){
        switch(ani){
            default:
            case("Idle"):
                animator.Play(player.idle.name);
                break;
            case("Run"):
                animator.Play(player.run.name);
                break;
            case("Jump"):
                animator.Play(player.jump.name);
                break;
            case("Fall"):
                animator.Play(player.fall.name);
                break;
            case("WallSlide"):
                animator.Play(player.wallSlide.name);
                break;
            case("WallJump"):
                animator.Play(player.wallJump.name);
                break;
        }
    }

    public void devChangeChar(){
        Destroy(characterAnimator);
        // Debug.Log("Destroyed");
        var charAnimator = Instantiate(player2.AnimatorPrefab, transform.position, transform.rotation, transform);
        // Debug.Log("instantiated");
        // preventing data loss by assigning instantiated object instead of the actual prefab
        characterAnimator = charAnimator;
        animator = charAnimator.GetComponent<Animator>();
        Player temp = player2;
        player2 = player;
        player = temp;
    }

    public void changeInto(Player input){
        Destroy(characterAnimator);
        // Debug.Log("Destroyed");
        player = input;
        var charAnimator = Instantiate(input.AnimatorPrefab, transform.position, transform.rotation, transform);
        // Debug.Log("instantiated");
        // preventing data loss by assigning instantiated object instead of the actual prefab
        characterAnimator = charAnimator;
        animator = charAnimator.GetComponent<Animator>();
    }
}

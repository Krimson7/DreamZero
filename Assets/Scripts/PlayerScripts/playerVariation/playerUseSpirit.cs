using System.ComponentModel.Design.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUseSpirit : MonoBehaviour, IplayerAttackState, IplayerAirAttackState, IAnimatorControl, IplayerParryState, I_PlayerFormListener
{
    public Player player;
    public PlayerStats ps;
    // public Player player2;
    public GameObject A1_hitbox;
    public GameObject A2_hitbox;
    public ContactFilter2D CF2;
    public playerEffectController effectController;
    public int specialCost;
    GameObject effectInstance;


    public GameObject characterAnimator;

    [Header("Animations")]
    public Animator animator;

    void Awake(){
        effectController = GetComponent<playerEffectController>();
        specialCost = player.specialCost;
        animator = transform.Find("Animator").GetComponent<Animator>();
    }

    void start(){
        
        CF2.SetLayerMask(LayerMask.GetMask("Enemy"));
        specialCost = player.specialCost;
        // animator = player.animator;
        // A1_hitbox = player.MeleeHitBox;
    }

    public void Attack(float damage, int direction, Rigidbody2D rb){
        if(player.notDefault == true){
            player.Attack(this, direction, rb);
            return;
        }
        print("Attack not found");
        
    }
    public void AirAttack(float damage, int direction, Rigidbody2D rb){
        if(player.notDefault == true){
            player.Attack(this, direction, rb);
            return;
        }
        print("AirAttack not found");
    }

    public void Special(Vector3 spawnPoint, int direction, Rigidbody2D rb, GameObject go){

        if(player.notDefault == true){
            player.Special(this, spawnPoint, direction, rb);
            return;
        }
        print("Special not found");
    }

    public void Parry(int direction, Rigidbody2D rb){
        if(player.notDefault == true){
            player.Parry(this, direction, rb);
            return;
        }

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
            case("Parry"):
                animator.Play(player.parry.name);
                break;
        }
    }

    // public void devChangeChar(){
    //     Destroy(characterAnimator);
    //     // Debug.Log("Destroyed");
    //     var charAnimator = Instantiate(player2.AnimatorPrefab, transform.position, transform.rotation, transform);
    //     // Debug.Log("instantiated");
    //     // preventing data loss by assigning instantiated object instead of the actual prefab
    //     characterAnimator = charAnimator;
    //     animator = charAnimator.GetComponent<Animator>();
    //     Player temp = player2;
    //     player2 = player;
    //     player = temp;
    //     specialCost = player.specialCost;
    // }

    public void changeInto(Player input){
        // Destroy(characterAnimator);
        // Debug.Log("Destroyed");
        player = input;
        // var charAnimator = Instantiate(input.AnimatorPrefab, transform.position, transform.rotation, transform);
        // Debug.Log("changed");
        // preventing data loss by assigning instantiated object instead of the actual prefab
        // characterAnimator = charAnimator;
        // animator = charAnimator.GetComponent<Animator>();
        animator.runtimeAnimatorController = input.animatorController;
    }

    public IEnumerator specialBoost(float time, Vector3 direction){
        yield return new WaitForSeconds(time);
    }

    public void spawnEffect(GameObject effect, Vector3 spawnPoint){
        effectInstance = Instantiate(effect, spawnPoint, Quaternion.identity, this.transform);
    }
    public void destroyEffect(){
        Destroy(effectInstance);
    }

    public void OnEnable(){
        ps.AddPlayerFormListener(this);
        changeInto(ps.getPlayerForm());
    }

    public void OnDisable(){
        ps.RemovePlayerFormListener(this);
    }

    public void OnPlayerFormChange(Player player){
        changeInto(player);
        // Debug.Log("PlayerFormChange");
    }

}

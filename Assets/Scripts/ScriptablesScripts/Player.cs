using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "DreamZero/Player", order = 0)]
public class Player : ScriptableObject {
    [Header("Character Property")]
    public string spiritName;
    public float maxHp;
    public float atkValue;
    public int specialCost;
    public float specialAtkValue;
    public float specialSpeed;
    // public GameObject AnimatorPrefab;
    public RuntimeAnimatorController animatorController;
    public GameObject specialPrefab;

    public bool meleeNormalAtk;
    public bool meleeSpecialAtk;
    public bool notDefault = false;
    // public float meleeKnockback;

    [Header("Component")]
    public Sprite sprite;
    // public GameObject CharHolder;
    // public GameObject MeleeHitBox;

    [Header("animations")]
    // public Animator animator;
    public AnimationClip attack;
    public AnimationClip airAttack;
    public AnimationClip idle;
    public AnimationClip run;
    public AnimationClip jump;
    public AnimationClip fall;
    public AnimationClip wallSlide;
    public AnimationClip wallJump;
    public AnimationClip parry;
    public AnimationClip special;

    public virtual void Attack(playerUseSpirit pus, int direction, Rigidbody2D rb){
        Debug.Log("Player Attack");
    }

    public virtual void AirAttack(playerUseSpirit pus, int direction, Rigidbody2D rb){
        Debug.Log("Player AirAttack");
    }

    public virtual bool Parry(playerUseSpirit pus, int direction, Rigidbody2D rb){
        Debug.Log("Player Parry");
        return false;
    }   

    public virtual void Special(playerUseSpirit pus, Vector3 spawnPoint, int direction, Rigidbody2D rb){
        Debug.Log("Player Special");
    }

    // public virtual void Special(playerUseSpirit pus){
    //     Debug.Log("Player Special");
    // }
    

    [Serializable] public class spirits{
        public virtual void Attack(){
            Debug.Log("Player Attack");
        }
    };


}




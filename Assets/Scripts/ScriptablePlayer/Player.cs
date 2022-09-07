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
    public GameObject AnimatorPrefab;
    public RuntimeAnimatorController animator;
    public GameObject specialPrefab;

    public bool meleeNormalAtk;
    public bool meleeSpecialAtk;
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
    
}


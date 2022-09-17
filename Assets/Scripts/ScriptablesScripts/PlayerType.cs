using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerType", menuName = "DreamZero/PlayerType", order = 1)]
public class PlayerType : ScriptableObject
{
    [Header("Character Property")]
    public string spiritName;
    public float maxHp;
    public float atkValue;
    public float specialAtkValue;
    public float specialConst1;
    public float specialConst2;
    public float specialConst3;
    public RuntimeAnimatorController animatorController;
    public GameObject specialPrefab;

    public bool isNormalAtkMelee;
    public bool isSpecialAtkMelee;

    [Header("Component")]
    public Sprite sprite;

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

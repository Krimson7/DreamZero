using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_BossAttack
{
    string Attack(Animator animator, Collider2D playerInRange);
}

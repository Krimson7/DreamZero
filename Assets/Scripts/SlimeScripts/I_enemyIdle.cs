using System;
using UnityEngine;
public interface I_enemyIdle
{
    string Idle(Animator animator, Collider2D playerInRange);
}

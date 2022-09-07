using System;
using UnityEngine;
public interface I_enemyKnockedback
{
    string Knocked_back(float idleTime, Animator animator, Rigidbody2D rb, Collider2D playerInRange);
}

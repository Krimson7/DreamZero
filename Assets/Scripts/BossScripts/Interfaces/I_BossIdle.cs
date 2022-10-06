using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_BossIdle
{
    string Idle(Animator animator, Collider2D playerInRange);
}

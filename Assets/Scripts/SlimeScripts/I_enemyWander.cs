using UnityEngine;
public interface I_enemyWander
{
    string Wander(Animator animator, bool playerInFront, bool checkWall, bool checkPitfall);
}


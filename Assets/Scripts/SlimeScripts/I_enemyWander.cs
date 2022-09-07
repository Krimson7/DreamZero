using UnityEngine;
public interface I_enemyWander
{
    string Wander(Animator animator, bool playerDetected, bool checkWall, bool checkPitfall);
}


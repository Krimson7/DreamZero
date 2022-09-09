using UnityEngine;
public interface I_enemyPlayerDetection
{
    Collider2D Detect();
    Collider2D Detect(float playerDetectionRange);
}


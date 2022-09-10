using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyC_Detect_Melee : MonoBehaviour, I_enemyPlayerDetection
{

    public LayerMask playerLayer;
    public Collider2D playerInRange;
    public float range = 0.5f;

    public Collider2D Detect(){
        return Detect(2f);
    }

    public Collider2D Detect( float playerDetectionRange ){
        playerInRange = Physics2D.OverlapCircle(transform.position, range, playerLayer);
        return playerInRange;
    }

        void OnDrawGizmos()
    {
        // Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere((Vector3)groundCheck.transform.position, 0.1f);
        // Gizmos.DrawWireSphere((Vector3)wallCheck.transform.position, 0.1f);
        Gizmos.color = playerInRange != null? Color.green : Color.red;
        Gizmos.DrawWireSphere((Vector3)transform.position, range);
    }
}

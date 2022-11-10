using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyC_Detect_Shoot : MonoBehaviour, I_enemyPlayerDetection
{

    public LayerMask playerLayer;

    public Collider2D playerInRange;

    public Vector2 range = new Vector2(8f, 1.5f);

    public Collider2D Detect(){
        return Detect(2f);
    }

    public Collider2D Detect( float playerDetectionRange ){
        playerInRange = Physics2D.OverlapBox(transform.position, range, 0, playerLayer);
        return playerInRange;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = playerInRange != null? Color.green : Color.blue;
        Gizmos.DrawWireCube(transform.position, range);
    }
}

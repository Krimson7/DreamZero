using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyC_Detect_Rush : MonoBehaviour, I_enemyPlayerDetection
{

    public LayerMask playerLayer;

    public Collider2D playerInRange;

    public Collider2D Detect(){
        return Detect(2f);
    }

    public Collider2D Detect( float playerDetectionRange ){
        playerInRange = Physics2D.OverlapCircle(transform.position, playerDetectionRange, playerLayer);
        return playerInRange;
    }
}

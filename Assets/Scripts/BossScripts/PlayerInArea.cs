using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInArea : MonoBehaviour
{
    public BossBehavior BossB;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            BossB.playerInRange = other;
        }
    }

    // void OnTriggerStay2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         BossB.playerInRange = other;
    //     }
    // }
    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         BossB.playerInRange = null;
    //     }
    // }
}

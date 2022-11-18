using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_Projectile : MonoBehaviour
{
    public float atk;
    public GameObject hitEffect;
    public LayerMask hitLayers;

    // Collider2D playerCharged;
    // Transform chargeHitPoint;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(other.gameObject.GetComponent<playerStateMachine>() != null){
                other.gameObject.GetComponent<playerStateMachine>().checkTakeDamage(atk, transform.position - other.transform.position);
                Debug.Log("hit enemy for:"+ atk);
            }
            // playerCharged.GetComponent<playerStateMachine>().checkTakeDamage(atk, transform.position - playerCharged.transform.position);
            // Debug.Log("Touched an enemy");
        }
        // if(hitEffect != null){
        //     GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //     Destroy(effect, 1);
        // }
        
        if(hitLayers == (hitLayers | (1<<other.gameObject.layer))){
            print("Bullet hit player");
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1);
            print("Destroyed by contact "+ other.gameObject.name);
            Destroy(gameObject);
        }
        // if(hitLayers.Exists()){
        //     Destroy(gameObject);
        // }
    }
}
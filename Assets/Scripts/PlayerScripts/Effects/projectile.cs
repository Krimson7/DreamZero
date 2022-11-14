using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float atk;
    public GameObject hitEffect;
    public LayerMask hitLayers;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            if(other.gameObject.GetComponent<I_damageable>() != null){
                other.gameObject.GetComponent<I_damageable>().TakeDamage(atk);
                Debug.Log("hit enemy for:"+ atk);
            }
            
            // Debug.Log("Touched an enemy");
        }
        // if(hitEffect != null){
        //     GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //     Destroy(effect, 1);
        // }
        
        if(hitLayers == (hitLayers | (1<<other.gameObject.layer))){
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

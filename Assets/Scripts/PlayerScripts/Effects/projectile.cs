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
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(other.gameObject.GetComponent<enemyHp>() != null){
                other.gameObject.GetComponent<enemyHp>().takeDamage(atk);
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

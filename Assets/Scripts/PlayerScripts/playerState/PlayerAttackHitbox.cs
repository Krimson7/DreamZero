using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    public bool enemyDetected = false;
    // public playerStateMachine playerStateMachine;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemyDetected = true;


            // Debug.Log("Touched an enemy");
        }
    }

    void dealDamage(GameObject enemy,float atk){
        // enemy.GetComponent<enemyHp>.takeDamage(atk);
    }
}

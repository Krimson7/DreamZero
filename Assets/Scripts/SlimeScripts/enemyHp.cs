using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHp : MonoBehaviour
{
    public float MaxHealth = 100f;
    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
    }

    public void takeDamage(float atk){
        currentHealth -= atk;

        if(currentHealth <= 0){
            die();
        }
    }

    void die(){
        Destroy(this.gameObject);
    }

}

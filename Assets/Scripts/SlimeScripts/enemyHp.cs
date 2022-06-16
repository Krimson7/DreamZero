using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHp : MonoBehaviour
{
    public float MaxHealth = 100f;
    public float currentHealth;
    public EnemyHealthBar enemyHealthBar;

    enemyHp(float max){
        MaxHealth = max;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        enemyHealthBar.setMaxHealth(MaxHealth);
    }

    public void takeDamage(float atk){
        currentHealth -= atk;
        enemyHealthBar.setHealth(currentHealth);
        if(currentHealth <= 0){
            die();
        }
    }

    void die(){
        Destroy(this.gameObject);
    }

}

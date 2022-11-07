using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHp : MonoBehaviour, I_damageable
{
    public float MaxHealth = 100f;
    public float currentHealth;
    public EnemyHealthBar enemyHealthBar;
    public BossBehavior BossBehavior;
    
    void Awake()
    {
        currentHealth = MaxHealth;
        enemyHealthBar.setMaxHealth(MaxHealth);
        enemyHealthBar.setHealth(currentHealth);
        BossBehavior = GetComponent<BossBehavior>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TakeDamage(float atk, Vector3 hitDirection){  
        TakeDamage(atk);
    }

    public void TakeDamage(float atk){
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

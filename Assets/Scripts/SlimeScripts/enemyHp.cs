using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHp : MonoBehaviour, I_damageable
{
    Rigidbody2D rb;
    public float MaxHealth = 100f;
    public float currentHealth;
    public EnemyHealthBar enemyHealthBar;
    public enemyBehavior1 enemyBehavior1;
    public enemyBehaviorShooter enemyBehavior2;
    public float knockbackForce = 10f;

    enemyHp(float max){
        MaxHealth = max;
        
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = MaxHealth;
        enemyBehavior1 = GetComponent<enemyBehavior1>();
        // enemyHealthBar.SetMaxHealth(MaxHealth);
    }
    // Start is called before the first frame update
    void Start()
    {
        // currentHealth = MaxHealth;
        enemyHealthBar.setMaxHealth(MaxHealth);
    }
    public void TakeDamage(float atk, Vector3 hitDirection){
        // print("hit");
        if(enemyBehavior1 != null){
            enemyBehavior1.getKnockback(knockbackForce, hitDirection);
        }
        else{
            enemyBehavior2=GetComponent<enemyBehaviorShooter>();
            enemyBehavior2.getKnockback(knockbackForce, hitDirection);
        }
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

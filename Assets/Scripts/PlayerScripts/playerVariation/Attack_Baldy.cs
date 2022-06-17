using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Baldy : MonoBehaviour, IplayerAttackState
{
    public GameObject A1_hitbox;
    private ContactFilter2D CF2;
    void awake(){
        CF2.SetLayerMask(LayerMask.GetMask("Enemy"));
    }

    public void Attack(float damage){
        A1_hitbox.SetActive(true);

        List<Collider2D> hitEnemies = new List<Collider2D>();
        Physics2D.OverlapCollider(A1_hitbox.GetComponent<BoxCollider2D>(), CF2 , hitEnemies);
        
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<enemyHp>().takeDamage(damage);
        }

        
    }

}

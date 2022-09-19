using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Kitsune", menuName = "DreamZero/Kitsune", order = 6)]

public class Kitsune : Player{
    // public bool notDefault = true;

    public Kitsune(){
        // Debug.Log("Kitsune");
    }

    public override void Attack(playerUseSpirit pus, int direction, Rigidbody2D rb){
        pus.animator.Play(attack.name);
        pus.A1_hitbox.SetActive(true);

        List<Collider2D> hitEnemies = new List<Collider2D>();
        Physics2D.OverlapCollider(pus.A1_hitbox.GetComponent<BoxCollider2D>(), pus.CF2 , hitEnemies);
        
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<enemyHp>().takeDamage(atkValue, pus.transform.position);
            pus.effectController.playAttackEffect(enemy.transform.position);
        }
        pus.A1_hitbox.SetActive(false);
    }

    public override void AirAttack(playerUseSpirit pus, int direction, Rigidbody2D rb){
        // Debug.Log("Kitsune AirAttack");
        pus.animator.Play(airAttack.name);
        pus.A2_hitbox.SetActive(true);

        List<Collider2D> hitEnemies = new List<Collider2D>();
        Physics2D.OverlapCollider(pus.A2_hitbox.GetComponent<BoxCollider2D>(), pus.CF2 , hitEnemies);
        
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<enemyHp>().takeDamage(atkValue);
            pus.effectController.playAttackEffect(enemy.transform.position);
        }
        pus.A2_hitbox.SetActive(false);
        
    }

    public override void Parry(playerUseSpirit pus, int direction, Rigidbody2D rb){
        Debug.Log("Kitsune Parry");
        pus.animator.Play(parry.name);
    }   

    public override void Special(playerUseSpirit pus, Vector3 spawnPoint, int direction, Rigidbody2D rb){
        Debug.Log("Kitsune Special");
        pus.animator.Play(special.name);
        GameObject bullet = Instantiate(specialPrefab, spawnPoint, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * direction * specialSpeed, ForceMode2D.Impulse);
        if(bullet.GetComponent<projectile>() != null){
            bullet.GetComponent<projectile>().atk = specialAtkValue;
        }
        pus.effectController.playSpecialEffect(spawnPoint);
        
    }
    
}
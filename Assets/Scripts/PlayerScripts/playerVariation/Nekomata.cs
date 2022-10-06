using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Nekomata", menuName = "DreamZero/Nekomata", order = 7)]

public class Nekomata : Player{
    public AnimationClip ParryRecoil;
    public float specialDetectEnemyRange = 4f;
    public LayerMask rayCastLayer;
    // public bool notDefault = true;
    bool foundEnemy = false;

    public Nekomata(){
        // Debug.Log("Nekomata");
    }

    public override void Attack(playerUseSpirit pus, int direction, Rigidbody2D rb){
        pus.animator.Play(attack.name);
        rb.AddForce(Vector2.right * direction * 10, ForceMode2D.Impulse);
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
        // Debug.Log("Nekomata AirAttack");
        pus.animator.Play(airAttack.name);
        rb.AddForce(Vector2.right * direction * 10, ForceMode2D.Impulse);
        pus.A2_hitbox.SetActive(true);

        List<Collider2D> hitEnemies = new List<Collider2D>();
        Physics2D.OverlapCollider(pus.A2_hitbox.GetComponent<BoxCollider2D>(), pus.CF2 , hitEnemies);
        
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<enemyHp>().takeDamage(atkValue);
            pus.effectController.playAttackEffect(enemy.transform.position);
        }
        pus.A2_hitbox.SetActive(false);
        
    }

    public override bool Parry(playerUseSpirit pus, int direction, Rigidbody2D rb){
        Debug.Log("Nekomata Parry");
        pus.animator.Play(parry.name);
        rb.AddForce(Vector2.right * direction * 10, ForceMode2D.Impulse);
        return true;
            //should add knockback recoil here ?
    }   

    public override void Special(playerUseSpirit pus, Vector3 spawnPoint, int direction, Rigidbody2D rb){
        Debug.Log("Nekomata Special");
        pus.animator.Play(special.name);
        pus.spawnEffect(specialPrefab, pus.transform.position);
        foundEnemy = false;

        // rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);

        List<Collider2D> foundEnemies = new List<Collider2D>();
        Physics2D.OverlapCircle(pus.transform.position, specialDetectEnemyRange, pus.CF2 , foundEnemies);

        foreach(Collider2D enemy in foundEnemies){
            Vector3 detectEnemyDirect = enemy.transform.position - pus.transform.position;
            // Ray ray = new Ray(pus.transform.position, detectEnemyDirect.normalized);
            RaycastHit2D rh = Physics2D.Raycast(pus.transform.position, detectEnemyDirect, 4f, rayCastLayer);
            Debug.Log(rh.collider.name);
            if(rh.transform.tag == "Enemy"){
                //if wait possible put here
                var dashDir = ((Vector2)detectEnemyDirect.normalized)* specialSpeed;
                Debug.Log(dashDir);
                rb.AddForce(dashDir, ForceMode2D.Impulse);
                foundEnemy = true;
                break;
            }
        }
        if(!foundEnemy)
            rb.AddForce(Vector2.right * direction * specialSpeed/2, ForceMode2D.Impulse);

    }
    

}
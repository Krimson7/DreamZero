using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackHitbox : MonoBehaviour
{
    public int damage;
    public GameObject boss;
    public Vector3 offset;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Debug.Log("Player hit");
            int fr = boss.transform.localScale.x > 0 ? 1 : -1;
            other.gameObject.GetComponent<playerStateMachine>()?.checkTakeDamage(damage, transform.position - offset*fr - other.transform.position);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTool : MonoBehaviour
{
    public playerStateMachine psm;
    public PlayerHp php;

    public float damage = 10f; 

    public void damagePlayer(){
        psm.checkTakeDamage(damage);
    }

    public void healPlayer(){
        php.takeHeals(damage);
    }

    public void godMode(){
        php.godMode();
    }

}

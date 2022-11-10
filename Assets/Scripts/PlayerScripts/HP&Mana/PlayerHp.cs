using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour, I_HpListener
{
    public float Hp;
    bool godModeBool = false;
    // public float maxHp = 100f;
    // public HealthBar playerHealthBar;

    public PlayerStats ps;
    // public playerStateMachine PSM;

    public void takeDamage(float damageDone){
        if(!godModeBool){
            ps.loseHp(damageDone);
        }
    }

    public void takeHeals(float healAmount){
        ps.gainHp(healAmount);
    }

    // Start is called before the first frame update
    void Start()
    {
        OnHpChanged();
    }

    public void OnEnable()
    { 
        ps.AddHpListener(this); 
    }

    public void OnDisable()
    { 
        ps.RemoveHpListener(this); 
    }

    public void OnHpChanged()
    {
        Hp = ps.getHp();
    }

    public void godMode(){
        godModeBool = !godModeBool;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    public float Hp;
    public float maxHp = 100f;
    public HealthBar playerHealthBar;
    // public playerStateMachine PSM;

    public void takeDamage(float damageDone){
        Hp -= damageDone;
        playerHealthBar.setHealth(Hp);
        UnityEngine.Debug.Log("player took damage");
    }
    // Start is called before the first frame update
    void Start()
    {
        playerHealthBar.setMaxHealth(maxHp);
        Hp = maxHp;
        playerHealthBar.setHealth(Hp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

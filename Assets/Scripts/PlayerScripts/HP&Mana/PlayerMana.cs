using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public float Mana;
    public float maxMana = 100f;
    public ManaBar playerManaBar;
    // public playerStateMachine PSM;

    public void UseMana(float damageDone){
        Mana -= damageDone;
        playerManaBar.setMana(Mana);
        Debug.Log("player took damage");
    }

    public void GetMana(float GetAmount){
        if(maxMana-Mana <= GetAmount){
            Mana=maxMana;
            playerManaBar.setMana(Mana);
            Debug.Log("player mana full");
        } else if(Mana < maxMana){
            Mana+=GetAmount;
            playerManaBar.setMana(Mana);
            Debug.Log("player Get mana");
        } else{
            Debug.Log("Cannot Get mana");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerManaBar.setMaxMana(maxMana);
        Mana = maxMana;
        playerManaBar.setMana(Mana);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

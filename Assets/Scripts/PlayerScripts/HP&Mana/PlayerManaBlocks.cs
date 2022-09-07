using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManaBlocks : MonoBehaviour
{
    // Start is called before the first frame update
    public int mana;
    public int MaxMana;
    public ManaBlocks CurrentMana;

    public void UseSpecial(int SkillCost){
        if(mana >= SkillCost){
            mana -= SkillCost;
        }
        CurrentMana.SetMana(mana);
    }

    public void GainMana(int get){
        if(mana < MaxMana){
        mana += get;
        }
        CurrentMana.SetMana(mana);
    }
    
    void Start()
    {
        mana = 0;
        CurrentMana.SetMana(mana);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
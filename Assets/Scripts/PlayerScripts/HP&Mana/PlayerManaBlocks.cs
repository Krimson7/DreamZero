using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManaBlocks : MonoBehaviour, I_ManaListener
{
    // Start is called before the first frame update
    public int mana;
    public int MaxMana;
    public PlayerStats ps;

    public void UseSpecial(int SkillCost){
        ps.loseMana(SkillCost);
        // updateMana();
    }

    public void GainMana(int get){
        ps.gainMana(get);
    }
    
    

    public void OnEnable()
    { 
        ps.AddManaListener(this); 
    }

    public void OnDisable()
    { 
        ps.RemoveManaListener(this); 
    }

    public void OnManaChanged()
    {
        mana = ps.getMana();
    }

    void Start()
    {
        OnManaChanged();
    }
}

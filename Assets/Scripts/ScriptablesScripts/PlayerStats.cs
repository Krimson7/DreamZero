using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "DreamZero/PlayerStat", order = 4)]
public class PlayerStats : ScriptableObject
{
    public float Hp;
    public float MaxHp;
    public int Mana;
    public int MaxMana;
    public Player playerForm;

    public List<I_ManaListener> manaListener = new List<I_ManaListener>();
    public List<I_HpListener> hpListener = new List<I_HpListener>();

    public void SetPlayerForm(Player player){
        playerForm = player;
    }

    public void SetMaxHp(float maxHp){
        MaxHp = maxHp;
    }

    public void SetMaxMana(int maxMana){
        MaxMana = maxMana;
    }

    public void SetHp(float hp){
        if(hp > MaxHp){
            Hp = MaxHp;
        } else if(hp >= 0){
            Hp = hp;
        } else {
            Hp = 0;
            Debug.Log("Hp cant be less than 0 ");
        }
        OnHpChange();
    }

    public void SetMana(int mana){
        if(mana > MaxMana){
            Mana = MaxMana;
        }else if(mana >= 0){
            Mana = mana;
        }else{
            Mana = 0;
            Debug.Log("mana cant be less than 0 ");
        }
        OnManaChange();
    }

    

    public void gainMana(int mana){
        if(Mana + mana > MaxMana){
            Mana = MaxMana;
        }else{
            Mana += mana;
        }
        OnManaChange();
    }

    public void loseMana(int mana){
        if(Mana - mana <= 0){
            Mana = 0;
        }else{
            Mana -= mana;
        }
        OnManaChange();
    }

    public int getMana(){
        return Mana;
    }

    public void gainHp(float hp){
        if(Hp + hp > MaxHp){
            Hp = MaxHp;
        }else{
            Hp += hp;
        }
        OnHpChange();
    }

    public void loseHp(float hp){
        if(Hp - hp <= 0){
            Hp = 0;
        }else{
            Hp -= hp;
        }
        OnHpChange();
    }

    public float getHp(){
        return Hp;
    }

    public float getMaxHp(){
        return MaxHp;
    }

    public void AddManaListener(I_ManaListener listener){
        manaListener.Add(listener);
    }

    public void RemoveManaListener(I_ManaListener listener){
        manaListener.Remove(listener);
    }

    public void AddHpListener(I_HpListener listener){
        hpListener.Add(listener);
    }

    public void RemoveHpListener(I_HpListener listener){
        hpListener.Remove(listener);
    }

    public void OnHpChange(){
        foreach(I_HpListener listener in hpListener){
            listener.OnHpChanged();
        }
    }

    public void OnManaChange(){
        foreach(I_ManaListener listener in manaListener){
            listener.OnManaChanged();
        }
    }

    // public void OnPlayerFormChange(){
    //     foreach(ManaBlocks listener in listeners){
    //         listener.OnPlayerFormChange();
    //     }
    // }

    private void OnEnable() {
        SetHp(MaxHp);
        SetMana(0);
        
    }
}

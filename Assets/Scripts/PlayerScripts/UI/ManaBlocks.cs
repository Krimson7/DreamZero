using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBlocks : MonoBehaviour, I_ManaListener
{
    public GameObject[] Blocks;
    public PlayerStats ps;


    public void NoMana(){
        for(int i = 0 ; i < Blocks.Length -1 ; i++){
            Blocks[i].SetActive(false);
        }
    }

    public void SetMana(int set){
        for(int i = 0 ; i < Blocks.Length ; i++){
            if(i < set){
                Blocks[i].SetActive(true);
            } else {
                Blocks[i].SetActive(false);
            }
        }
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
        SetMana(ps.getMana());
    }

    void Start()
    {
        OnManaChanged();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBlocks : MonoBehaviour
{
    public GameObject[] Blocks;

    public void NoMana(){
        for(int i = 0 ; i < Blocks.Length -1 ; i++){
            Blocks[i].SetActive(false);
        }
    }

    public void GainMana(int gain){
        for(int i = Blocks.Length -1 ; i< Blocks.Length +gain -1 ; i++){
            Blocks[i].SetActive(true);
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTool : MonoBehaviour
{
    public playerStateMachine psm;
    public float damage = 10f; 

    public void damagePlayer(){
        psm.checkTakeDamage(damage);
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

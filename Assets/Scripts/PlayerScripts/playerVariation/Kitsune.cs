using UnityEngine;

public class Kitsune : Player{

    public Kitsune(){
        Debug.Log("Kitsune");
    }

    public override void Attack(){
        Debug.Log("Kitsune Attack");
    }

    public void AirAttack(){
        Debug.Log("Kitsune AirAttack");
    }

    public void Parry(){
        Debug.Log("Kitsune Parry");
    }   

    public void Special(){
        Debug.Log("Kitsune Special");
    }
    
}
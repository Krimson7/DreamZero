using UnityEngine;
[CreateAssetMenu(fileName = "Nekomata", menuName = "DreamZero/Nekomata", order = 2)]
public class Nekomata : Player{
    public Nekomata(){
        Debug.Log("Nekomata");
    }

    public override void Attack(){
        Debug.Log("Nekomata Attack");
    }

    public void AirAttack(){
        Debug.Log("Nekomata AirAttack");
    }

    public void Parry(){
        Debug.Log("Nekomata Parry");
    }   

    public void Special(){
        Debug.Log("Nekomata Special");
    }
    

}
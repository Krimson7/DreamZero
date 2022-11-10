using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTool_PlayerStats : MonoBehaviour
{
    public PlayerStats ps;

    public void TakeDamage() {
        ps.loseHp(10f);
    }

    public void HealPlayer() {
        ps.gainHp(10f);
    }

    public void godMode(){
        ps.godMode();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public List<GameObject> ChangeList;
    public GameObject DayGlobal;
    public GameObject NightGlobal;
    

    public void ToggleDayNight(){
        foreach (GameObject i in ChangeList){
            i.SetActive(!i.activeSelf);
        }
        if(DayGlobal.activeSelf){
            DayGlobal.SetActive(false);
            NightGlobal.SetActive(true);
        }else{
            NightGlobal.SetActive(false);
            DayGlobal.SetActive(true);
        }
    }
}

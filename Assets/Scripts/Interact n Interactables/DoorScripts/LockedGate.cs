using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedGate : MonoBehaviour, I_interactable
{
    public GameObject gate;
    public bool isLocked = true;

    public GameObject key;
    private enum condition{
        destroyed,
        collected,
        none,
    }
    [SerializeField] private condition keyCondition;

    public void unlockGate()
    {
        isLocked = false;
        gate.SetActive(false);
    }

    public void lockGate()
    {
        isLocked = true;
        gate.SetActive(true);
    }

    public void toggleGate()
    {
        if (isLocked)
            unlockGate();
        else
            lockGate();
    }

    public void Interact(playerStateMachine psm)
    {
        switch(keyCondition){
            case condition.destroyed:
                if(key == null){
                    unlockGate();
                }
                break;
            default :
                break;
        }
    }

}

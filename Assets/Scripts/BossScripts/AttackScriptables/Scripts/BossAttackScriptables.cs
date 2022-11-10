using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossAttackScriptables : ScriptableObject
{
    public float atk;
    public virtual void Reset() {
        Debug.Log("Reset default");
    }
    public virtual string Attack(BossBehavior boss)
    {
        return "No changes";
    }
}

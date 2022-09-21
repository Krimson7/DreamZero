using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossAttackScriptables : ScriptableObject
{
    public virtual string Attack(BossBehavior boss)
    {
        return "No changes";
    }
}

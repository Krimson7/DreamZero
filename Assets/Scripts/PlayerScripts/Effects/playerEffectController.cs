using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerEffectController : MonoBehaviour
{
    public GameObject parryHitEffect;
    public GameObject gainManaEffect;
    public GameObject attackEffect;
    // public GameObject gotHitEffect;

    public void playParryHitEffect(Vector3 position)
    {
        GameObject effect = Instantiate(parryHitEffect, position, Quaternion.identity);
        Destroy(effect, 1);
    }

    public void playGainManaEffect(Vector3 position)
    {
        GameObject effect = Instantiate(gainManaEffect, position, Quaternion.identity);
        Destroy(effect, 1);
    }

    public void playAttackEffect(Vector3 position)
    {
        GameObject effect = Instantiate(attackEffect, position, Quaternion.identity);
        Destroy(effect, 1);
    }
}

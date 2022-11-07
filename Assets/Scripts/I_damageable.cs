using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_damageable
{
    void TakeDamage(float atk);
    void TakeDamage(float atk, Vector3 hitDirection);
}


using UnityEngine;
public interface IplayerAirAttackState{
    void AirAttack(float damage, int direction, Rigidbody2D rb);
}

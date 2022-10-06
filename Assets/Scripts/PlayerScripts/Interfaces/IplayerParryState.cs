using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IplayerParryState{
    bool Parry(int direction, Rigidbody2D rb);
}

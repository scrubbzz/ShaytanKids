using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAffectPlayer //  This will be an interface that all the enemy types implement.
{
    void LocatePlayer();
    void DamagePlayer();
}

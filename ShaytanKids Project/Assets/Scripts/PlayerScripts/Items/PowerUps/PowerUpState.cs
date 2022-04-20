using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerupState
{
    public abstract void EnterState(PotionPowerupHandler manager);
    public abstract void UpdateState(PotionPowerupHandler manager);

}

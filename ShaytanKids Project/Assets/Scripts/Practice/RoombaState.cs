using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoombaState
{
    public abstract void UpdateState(RoombaStateManager roomba);

    public abstract void BatteryDrain(RoombaStateManager roomba);

}

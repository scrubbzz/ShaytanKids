using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingRoombaState : RoombaState
{
    public override void UpdateState(RoombaStateManager roomba)
    {
        while (roomba.batteryPercent < 100)
        {
            roomba.batteryPercent += 0.5f;
        }

        Debug.Log("Roomba fully charged, now resuming cleaning.");
        roomba.SwitchState(roomba.patrolState);

    }

    public override void BatteryDrain(RoombaStateManager roomba)
    {

    }
}

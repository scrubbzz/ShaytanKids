using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekChargerRoombaState : RoombaState
{
    public override void UpdateState(RoombaStateManager roomba)
    {
        while (roomba.transform.position != roomba.chargingPoint)
        {
            // move to the charging station.
            Vector3.MoveTowards(roomba.transform.position, roomba.chargingPoint, 0);
        }

        Debug.Log("Charging point reached, now charging.");
        roomba.SwitchState(roomba.chargingState);

    }

    public override void BatteryDrain(RoombaStateManager roomba)
    {
        roomba.batteryPercent -= 0.001f;
    }
}

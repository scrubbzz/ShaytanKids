using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingRoombaState : RoombaState
{

    public override void UpdateState(RoombaStateManager roomba)
    {
        while (roomba.batteryPercent > 15)
        {
            MoveToCleaningPosition(roomba);
        }

        Debug.Log("Battery low! Now moving to the charging station.");
        roomba.SwitchState(roomba.seekState);
        
    }
    void MoveToCleaningPosition(RoombaStateManager roomba)
    {
        for (int i = 0; i < roomba.patrolPoints.Count; i++)
        {
            Vector3 currentPoint = roomba.patrolPoints[i];

            while (roomba.transform.position != currentPoint)
                Vector3.MoveTowards(roomba.transform.position, currentPoint, 0);
            
            // move to the patrol point.
            // this should be done with a function that repeats movement, while waiting for movement to complete before looping again.
        }
    }
    public override void BatteryDrain(RoombaStateManager roomba)
    {
        roomba.batteryPercent -= 0.001f;
    }

    //IEnumerator MoveToPatrolPoint(float placeholder) { yield return new WaitUntil(farted)}
}

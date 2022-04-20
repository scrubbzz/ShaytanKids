using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaStateManager : MonoBehaviour
{
    RoombaState roombaState;

    public PatrollingRoombaState patrolState = new PatrollingRoombaState();
    public SeekChargerRoombaState seekState = new SeekChargerRoombaState();
    public ChargingRoombaState chargingState = new ChargingRoombaState();

    public float batteryPercent;
    public Vector3 chargingPoint;
    public List<Vector3> patrolPoints = new List<Vector3>();


    void Start()
    {
        roombaState = patrolState;
    }

    void Update()
    {
        roombaState.UpdateState(this);
        roombaState.BatteryDrain(this);
    }

    public void SwitchState(RoombaState nextState)
    {
        roombaState = nextState;
    }
}

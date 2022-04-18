using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterValues : MonoBehaviour
{
    public int currentTrust;
    public int currentGreed;
    public int meterChangeAmount;
    void Start()
    {
        currentTrust = 50;
        currentGreed = 50;
        meterChangeAmount = 10;
    }



    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterManager : MonoBehaviour
{
    public int maxTrust = 100;
    public int maxGreed = 100;
    public int currentTrust;
    public int currentGreed;
    public GameObject TheTrustManager;
    public GameObject TheGreedManager;
    public TrustManager trustManager;
    public GreedManager greedManager;
    public int meterChangeAmount;
    public GameObject EnemySpawner;
    //public EnemySpawner enemySpawner;
    public GameObject Light2DObject;
    public TrustBar TrustBar;
    void Start()
    {
        
        SetTrustAndGreedMeters();
        currentGreed = 50;
        currentTrust = 50;
        meterChangeAmount = 10;
        TheTrustManager = GameObject.Find("TrustMeter");
        TheGreedManager = GameObject.Find("GreedMeter");
        trustManager = TheTrustManager.GetComponent<TrustManager>();
        greedManager = TheGreedManager.GetComponent<GreedManager>();
        Light2DObject = GameObject.Find("Light 2D");
        EnemySpawner = GameObject.Find("EnemySpawner");
       
    }

   
    void Update()
    {
        CheckEachTheKids();
        
        
    }


    private void SetTrustAndGreedMeters()
    {
        if (trustManager && greedManager != null)
        {
            trustManager.SetMaxBar(100);
            greedManager.SetMaxBar(100);
        }
    }

    void IncreaseTrust(int saveKids)
    {
        currentTrust += saveKids;
        if (currentTrust > maxTrust)
        {
            currentTrust = maxTrust;
        }
        trustManager.SetBar(currentTrust);

    }

    void DecreaseTrust(int saveKids)
    {
        currentTrust -= saveKids;
        if (currentTrust < 0)
        {
            currentTrust = 0;
        }
        trustManager.SetBar(currentTrust);

    }


    void CheckEachTheKids()
    {
        foreach(GameObject kid in EnemySpawner.GetComponent<EnemySpawner>().shaytanKids)
        {
            if (kid.GetComponent<TrustBar>().killKid == true)
            {
                IncreaseGreed(meterChangeAmount);
                DecreaseTrust(meterChangeAmount);
                Debug.Log("Kid Killed");
                
            }

            if (kid.GetComponent<TrustBar>().saveKid == true)
            {

                DecreaseGreed(meterChangeAmount);
                IncreaseTrust(meterChangeAmount);
                Debug.Log("Kid Saved");
            }

        }


    }
    void IncreaseGreed(int killKids)
    {
        currentGreed += killKids;
        if (currentGreed > maxGreed)
        {
            currentGreed = maxGreed;
        }
        greedManager.SetBar(currentGreed);
        var thePlayerLight = this.Light2DObject.GetComponent<TheLightManager>();
        thePlayerLight.light.pointLightInnerRadius += thePlayerLight.lightAddAmount;
        thePlayerLight.light.pointLightOuterRadius += thePlayerLight.lightAddAmount * 2;


    }
    void DecreaseGreed(int killKids)
    {
        currentGreed -= killKids;
        if (currentGreed < 0)
        {
            currentGreed = 0;
        }
        greedManager.SetBar(currentGreed);

    }


}

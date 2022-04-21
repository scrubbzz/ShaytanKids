using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterValues : MonoBehaviour
{
    public GameObject[] shaytanKids;
    public EnemyDestroyer enemyDestroyer;
    public GameObject theTrustManager;
    public GameObject theGreedManager;
    public TrustManager trustManager;
    public GreedManager greedManager;
    public int currentTrust;
    public int currentGreed;
    public int maxTrust = 100;
    public int maxGreed = 100;
    public int meterChangeAmount;
    public GameObject Light2DObject;
    public TheLightManager thePlayerLight;

    void Start()
    {
        theTrustManager = GameObject.Find("TrustMeter");
        theGreedManager = GameObject.Find("GreedMeter");
        enemyDestroyer = GameObject.Find("EnemyDestroyer").GetComponent<EnemyDestroyer>();
        trustManager = theTrustManager.GetComponent<TrustManager>();
        greedManager = theGreedManager.GetComponent<GreedManager>();
        SetTrustAndGreedMeters();
        currentTrust = 50;
        currentGreed = 50;
        meterChangeAmount = 10;
        Light2DObject = GameObject.Find("Light 2D");
        thePlayerLight = Light2DObject.GetComponent<TheLightManager>();
    }



    void Update()
    {
        shaytanKids = GameObject.FindGameObjectsWithTag("ShaytanKid");

        for (int i = 0; i < shaytanKids.Length; i++)
        {
            if(shaytanKids[i].GetComponent<TrustBar>().saveKid == true)
            {
                IncreaseTrust(meterChangeAmount);
                DecreaseGreed(meterChangeAmount);
            }

        }
        
        if(enemyDestroyer.kidDied == true)
        {
            IncreaseGreed(meterChangeAmount);
            DecreaseTrust(meterChangeAmount);
        }
    }


    private void SetTrustAndGreedMeters()
    {
        if (trustManager && greedManager != null)
        {
            trustManager.SetMaxBar(100);
            greedManager.SetMaxBar(100);
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
       
        thePlayerLight.light.pointLightInnerRadius += thePlayerLight.lightAddAmount;
        thePlayerLight.light.pointLightOuterRadius += thePlayerLight.lightAddAmount * 2;

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

    void IncreaseTrust(int saveKids)
    {
        currentTrust += saveKids;
        if (currentTrust > maxTrust)
        {
            currentTrust = maxTrust;
        }
        trustManager.SetBar(currentTrust);

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

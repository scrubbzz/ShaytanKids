using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;

public class TrustBar : MonoBehaviour
{
    private float distToPlayer;
    public int maxTrust = 100;
    public int maxGreed = 100;
   public int meterValuescurrentTrust;
    public int meterValuescurrentGreed;
    public GameObject Light2DObject;
    public float range;
    public GameObject TheTrustManager;
    public GameObject TheGreedManager;
    public TrustManager trustManager;
    public GreedManager greedManager;
    public GameObject MeterValueHolder;
    public MeterValues meterValues;
    public bool killKid;
    public bool saveKid;
  
    private void Awake()
    {
        /*TheTrustManager = GameObject.Find("TrustManager");
        TheGreedManager = GameObject.Find("GreedManager");*/
    }
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
       /* trustManager = TheTrustManager.GetComponent<TrustManager>();
        greedManager = TheGreedManager.GetComponent<GreedManager>();*/
        //currentTrust = 50;
        //currentGreed = 50;
       /* trustManager.SetMaxBar(100);
        greedManager.SetMaxBar(100);*/
      //  meterChangeAmount = 10;
    }

 
    void Update()
    {
        MeterValueHolder = GameObject.Find("Main Camera");
        meterValues = MeterValueHolder.GetComponent<MeterValues>();
        FindRequiredComponents();
        SetTrustAndGreedMeters();
        distToPlayer = Vector2.Distance(transform.position, Light2DObject.transform.position);
        if (distToPlayer <= range && Input.GetKeyDown(KeyCode.L))
        {
            saveKid = true;
            DecreaseGreed(meterValues.meterChangeAmount);
            IncreaseTrust(meterValues.meterChangeAmount);
            Destroy(this.gameObject);
        }
        else
        {
            saveKid = false;
        }
        /*if (distToPlayer <= range && Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.O))
        {
            killKid = true;
            DecreaseTrust(meterChangeAmount);
            IncreaseGreed(meterChangeAmount);
        }*/
        if(this.gameObject.GetComponent<EnemyHealthManager>().health <= 25 || Input.GetKeyDown(KeyCode.O))
        {
           DecreaseTrust(meterValues.meterChangeAmount);
            IncreaseGreed(meterValues.meterChangeAmount);

        }
        else
        {
            killKid = false;
        }

    }

    private void FindRequiredComponents()
    {
        Light2DObject = GameObject.Find("Light 2D");
        TheTrustManager = GameObject.Find("TrustMeter");
        TheGreedManager = GameObject.Find("GreedMeter");
        trustManager = TheTrustManager.GetComponent<TrustManager>();
        greedManager = TheGreedManager.GetComponent<GreedManager>();
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
        meterValues.currentTrust += saveKids;
        if (meterValues.currentTrust > maxTrust)
        {
            meterValues.currentTrust = maxTrust;
        }
        trustManager.SetBar(meterValues.currentTrust);
       
    }

    void DecreaseTrust(int saveKids)
    {
        meterValues.currentTrust -= saveKids;
        if (meterValues.currentTrust < 0)
        {
            meterValues.currentTrust = 0;
        }
        trustManager.SetBar(meterValues.currentTrust);
  
    }


    void IncreaseGreed(int killKids)
    {
        meterValues.currentGreed += killKids;
        if (meterValues.currentGreed > maxGreed)
        {
            meterValues.currentGreed = maxGreed;
        }
        greedManager.SetBar(meterValues.currentGreed);
        var thePlayerLight = this.Light2DObject.GetComponent<TheLightManager>();
        thePlayerLight.light.pointLightInnerRadius += thePlayerLight.lightAddAmount;
        thePlayerLight.light.pointLightOuterRadius += thePlayerLight.lightAddAmount * 2;
       
    }
    void DecreaseGreed(int killKids)
    {
        meterValues.currentGreed -= killKids;
        if (meterValuescurrentGreed < 0)
        {
            meterValues.currentGreed = 0;
        }
        greedManager.SetBar(meterValues.currentGreed);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

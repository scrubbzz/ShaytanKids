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
    public int currentTrust;
    public int currentGreed;
    public GameObject Light2DObject;
    public float range;
  /*  public GameObject TheTrustManager;
    public GameObject TheGreedManager;
    public TrustManager trustManager;
    public GreedManager greedManager;*/
    public bool killKid;
    public bool saveKid;
    public int meterChangeAmount;
    
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
        //urrentTrust = 50;
        //currentGreed = 50;
       /* trustManager.SetMaxBar(100);
        greedManager.SetMaxBar(100);*/
       // meterChangeAmount = 10;
    }

 
    void Update()
    {
      FindRequiredComponents();
      //  SetTrustAndGreedMeters();
        distToPlayer = Vector2.Distance(transform.position, Light2DObject.transform.position);
        if (distToPlayer <= range && Input.GetKeyDown(KeyCode.L))
        {
            saveKid = true;
            Debug.Log("Saved");
            // DecreaseGreed(meterChangeAmount);
            //  IncreaseTrust(meterChangeAmount);
        }
        else
        {
            saveKid = false;
        }
        if (distToPlayer <= range && Input.GetKeyDown(KeyCode.K))
        {
            killKid = true;
            Debug.Log("killed");
           // DecreaseTrust(meterChangeAmount);
            //IncreaseGreed(meterChangeAmount);
        }
        else
        {
            killKid = false;
        }

    }

    private void FindRequiredComponents()
    {
        Light2DObject = GameObject.Find("Light 2D");
        /*TheTrustManager = GameObject.Find("TrustMeter");
        TheGreedManager = GameObject.Find("GreedMeter");
        trustManager = TheTrustManager.GetComponent<TrustManager>();
        greedManager = TheGreedManager.GetComponent<GreedManager>();*/
    }

   /* private void SetTrustAndGreedMeters()
    {
        if (trustManager && greedManager != null)
        {
            trustManager.SetMaxBar(100);
            greedManager.SetMaxBar(100);
        }
    } */

  /*  void IncreaseTrust(int saveKids)
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
*/
}

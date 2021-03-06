using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;

public class TheLightManager : MonoBehaviour
{
    public Light2D light;
    public float lightLossAmount;
    public float lightAddAmount;
    public GameObject theEnemySpawner;
    public EnemySpawner enemySpawner;
    //public TrustBar trustBar;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
        theEnemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        enemySpawner = theEnemySpawner.GetComponent<EnemySpawner>();
    }
   
    // Update is called once per frame
    void Update()
    {
        //playerLight = GetComponent<Light2D>();
        IncreaseLight();
        DecreaseLight();
        MaintainLight();
        Addlight();
    }
    public void Addlight()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            light.pointLightInnerRadius += lightAddAmount * Time.deltaTime;
            light.pointLightOuterRadius += lightAddAmount * 2 * Time.deltaTime;
        }
    }
    public void IncreaseLight()
    {
       /* foreach (GameObject kid in enemySpawner.shaytanKids)
        {
            trustBar = kid.GetComponent<TrustBar>();
            Debug.Log(trustBar.currentTrust);
            //var theKid = kid.GetComponent<TrustBar>();
           *//* if (kid.GetComponent<TrustBar>().killKid == true)
            {
                playerLight.pointLightInnerRadius += lightAddAmount;
                playerLight.pointLightOuterRadius += lightAddAmount * 2;
            
                Debug.Log("You Retard");
            }*//*
            Debug.Log("You Moron");
           *//* playerLight.pointLightInnerRadius += lightAddAmount;
            playerLight.pointLightOuterRadius += lightAddAmount * 2;*//*
        }*/
        
    }
    public void DecreaseLight()
    {
        light.pointLightInnerRadius -= lightLossAmount * Time.deltaTime;
        light.pointLightOuterRadius -= lightLossAmount * 2 * Time.deltaTime;
    }
    public void MaintainLight()
    {
        if (light.pointLightInnerRadius > light.pointLightOuterRadius)
        {
            light.pointLightInnerRadius = light.pointLightOuterRadius;
        }
        if(light.pointLightInnerRadius <= 0)
        {
            light.pointLightInnerRadius = 0;
        }
        if(light.pointLightOuterRadius <= light.pointLightInnerRadius)
        {
            light.pointLightOuterRadius = light.pointLightInnerRadius;
        }
    }

}

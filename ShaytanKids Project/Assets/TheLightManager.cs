using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TheLightManager : MonoBehaviour
{
    Light2D playerLight;
    public float lightLossAmount;
    public TrustBar trustBar;
    // Start is called before the first frame update
    void Start()
    {
        playerLight = GetComponent<Light2D>();
      
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseLight();
        DecreaseLight();
    }
    public void IncreaseLight()
    {
        if(trustBar.killKid == true)
        {
            playerLight.pointLightInnerRadius += 5;
        }
    }
    public void DecreaseLight()
    {
        playerLight.pointLightInnerRadius -= lightLossAmount * Time.deltaTime;
        playerLight.pointLightOuterRadius -= lightLossAmount * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TheLightManager : MonoBehaviour
{
    Light2D playerLight;
    public float lightLossAmount;
    // Start is called before the first frame update
    void Start()
    {
        playerLight = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseLight();
    }
    public void IncreaseLight()
    {

    }
    public void DecreaseLight()
    {
        playerLight.pointLightInnerRadius -= lightLossAmount * Time.deltaTime;
        playerLight.pointLightOuterRadius -= lightLossAmount * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TheLightManager : MonoBehaviour
{
    Light2D playerLight;
    // Start is called before the first frame update
    void Start()
    {
        playerLight = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

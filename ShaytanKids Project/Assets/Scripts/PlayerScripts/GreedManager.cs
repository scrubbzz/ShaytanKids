using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreedManager : MonoBehaviour
{
    public Slider slider;
    private void Start()
    {
        slider = this.GetComponent<Slider>();
    }
    public void SetMaxBar(int bar)
    {
        slider.maxValue = bar;

    }
    public void SetBar(int bar)
    {
        slider.value = bar;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameSett : MonoBehaviour
{
    public Slider VolumeSlider;
    public Slider mouseSlider;
    public Slider camSensSlider;
    public settings settings;


    public void ChangeVolume()
    {
        AudioListener.volume = VolumeSlider.value;
    }
    public void ChangeSensitivity()
    {
        settings.sensitivity = mouseSlider.value;
    }
}

public class settings
{
    internal float sensitivity;
}
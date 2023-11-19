using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour 
{
    //Gia metafora plhroforias sto game scene
    float volume = 100;
    
    //Gia allagh tou hxou sto menu
    //volume = AudioSource.volume

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void VolumeChange(Slider volumeSlider) 
    { 
        volume = volumeSlider.value;
        Debug.Log(volume);
    }
}

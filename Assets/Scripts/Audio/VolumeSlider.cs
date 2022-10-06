using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour, I_AudioSourceListener
{
    public Slider slider;
    public Options options;
    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }
    void Start()
    {
        OnVolumeChanged();
    }

    public void SetVolume(float Volume){
        options.setVolume(Volume);
    }

    public void OnEnable(){
        options.AddAudioListener(this);
        OnVolumeChanged();
    }
    public void OnDisable(){
        options.RemoveAudioListener(this);
    }
    public void OnVolumeChanged(){
        slider.value = options.Volume;
    }
}

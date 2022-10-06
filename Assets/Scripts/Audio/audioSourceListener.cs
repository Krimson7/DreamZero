using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioSourceListener : MonoBehaviour, I_AudioSourceListener
{
    public AudioSource audioSource;
    public Options options;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        audioSource.Play();
    }

    public void OnEnable(){
        options.AddAudioListener(this);
        OnVolumeChanged();
    }
    public void OnDisable(){
        options.RemoveAudioListener(this);
    }
    public void OnVolumeChanged(){
        audioSource.volume = options.Volume;
    }
}

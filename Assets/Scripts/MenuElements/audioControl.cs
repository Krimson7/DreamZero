using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioControl : MonoBehaviour
{
    public AudioSource audioSource;
    public float volume = 1f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setVolume(float vol){
        volume = vol;
        audioSource.volume = volume;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioControl : MonoBehaviour
{
    // public AudioSource audioSource;
    public Options options;
    // Start is called before the first frame update
    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
        // audioSource.Play();
    }

    public void SetVolume(float Volume){
        options.setVolume(Volume);
    }
}

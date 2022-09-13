using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialAreas : MonoBehaviour
{
    public LayerMask playerLayer;
    // public GameObject player;

    public UnityEvent onPlayerEnter;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("Player entered tutorial area");
            onPlayerEnter.Invoke();
        }
    }

}

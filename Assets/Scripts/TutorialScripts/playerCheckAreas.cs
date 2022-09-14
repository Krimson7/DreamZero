using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerCheckAreas : MonoBehaviour
{
    public UnityEvent onPlayerEnter;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // print("Player entered area");
            onPlayerEnter.Invoke();
        }
    }

}

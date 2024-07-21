using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikesControllerScript : MonoBehaviour
{
    SpriteRenderer[] spikes; 
    // Start is called before the first frame update
    void Start()
    {
        spikes = gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (var spike in spikes)
        {
            spike.enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            foreach (var spike in spikes)
        {
            spike.enabled = true;
        }
        }
    }
}

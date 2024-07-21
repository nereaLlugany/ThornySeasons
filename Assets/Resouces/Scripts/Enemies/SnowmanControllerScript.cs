using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnowmanControllerScript : MonoBehaviour
{

    SpriteRenderer goodSnowman;
    SpriteRenderer evilSnowman;
    bool flowerTriggered;
    // Start is called before the first frame update
    void Start()
    {
        goodSnowman = GameObject.Find("snowmanGood").GetComponent<SpriteRenderer>();
        evilSnowman = GameObject.Find("snowmanEvil").GetComponent<SpriteRenderer>();
        goodSnowman.gameObject.SetActive(false);
        evilSnowman.gameObject.SetActive(true);
        flowerTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getFlowerTrigger()
    {
        return flowerTriggered;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Character"))
        {
            goodSnowman.gameObject.SetActive(true);
            evilSnowman.gameObject.SetActive(false);
            flowerTriggered = true;
        }
    }
}

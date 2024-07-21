using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitsControllerScript : MonoBehaviour
{
    GameObject startDoor;
    GameObject characterGO;
    // Start is called before the first frame update
    void Start()
    {
        startDoor = GameObject.FindGameObjectWithTag("doorStart");
        characterGO = GameObject.FindGameObjectWithTag("Character");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            characterGO.GetComponent<CharacterControllerScript>().DecreaseLives();
            characterGO.transform.position = startDoor.transform.position;
            
        }
    }
}

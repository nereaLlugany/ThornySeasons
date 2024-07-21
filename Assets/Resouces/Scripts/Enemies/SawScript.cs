using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{
    private float moveSpeed = 10.0f; 
    private float rotationSpeed = 100.0f; 
    private bool turnRight = false; 

    void Update()
    {
        
        float currentRotationSpeed ;
        
        if( turnRight){

            currentRotationSpeed= -rotationSpeed;
        }
        else{

            currentRotationSpeed= rotationSpeed;
        }


        transform.Rotate(Vector3.forward * currentRotationSpeed * Time.deltaTime);

        
        Vector3 movement = (turnRight ? Vector3.right : Vector3.left) * moveSpeed * Time.deltaTime;
        transform.position += movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("TurnRight"))
        {
            turnRight = true;
        }
        
        else if (collision.gameObject.CompareTag("TurnLeft"))
        {
            turnRight = false;
        } else if (collision.gameObject.CompareTag("Character"))
        {
            GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterControllerScript>().DecreaseLives();
        }
    }
}





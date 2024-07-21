using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DoorLevel1Script : MonoBehaviour
{
    CharacterControllerScript ccs;
    bool isPlayerInside;
    [SerializeField] GameObject e;


    // Start is called before the first frame update
    void Start()
    {
        ccs = GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterControllerScript>();
        isPlayerInside = false;
        e.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && isPlayerInside)
        {

            if(PlayerPrefs.GetInt("LastLevel")>1){

                SceneManager.LoadScene(2);

            }
            else{

                PlayerPrefs.SetInt("LastLevel", 1);
                 SceneManager.LoadScene(2);

            }
            
            
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            e.SetActive(true);

            isPlayerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            e.SetActive(false);
            isPlayerInside = false;
        }
    }
}

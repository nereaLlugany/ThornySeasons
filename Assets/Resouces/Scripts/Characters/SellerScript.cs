using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject square;
    int lives;

    int coins;

    public Text speak;

    bool ableToBuy;

    CharacterControllerScript ccs;
    GameObject cco;




    void Start()
    {
        square.SetActive(false);
        lives = PlayerPrefs.GetInt("CurrentLifeHearts");
        coins = PlayerPrefs.GetInt("CurrentCoins");

        cco = GameObject.FindGameObjectWithTag("Character");
        ccs = cco.GetComponent<CharacterControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ableToBuy)
        {
            square.transform.localScale = new Vector3(1, 5, 1);
            speak.text = "Do you want to buy a life?  Press B to buy/ Press S to canceal";

            if (Input.GetKeyDown(KeyCode.B))
            {
                if (coins >= 2)
                {
                    ccs.Comprar();
                    square.transform.localScale = new Vector3(1, 2, 1);
                    ableToBuy= false;
                    speak.text = "Thank you!";

                    lives= PlayerPrefs.GetInt("CurrentLiveHearts");

                    coins= PlayerPrefs.GetInt("CurrentCoins");
                    
                   
                }
                
                
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {

                square.transform.localScale = new Vector3(1, 2, 1);
                speak.text = "See you soon!";
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        square.SetActive(true);

        if (lives == 3)
        {
            ableToBuy = false;
            speak.text = "You have all the lives";

        }
        else if (lives < 3 && coins < 2)
        {
            ableToBuy = false;
            square.transform.localScale = new Vector3(1, 2, 1);
            speak.text = "To buy a heart you need two coins";
        }
        else if (coins < 2)
        {
            speak.text = "You need at least two coins to buy a life.";
        }
        else
        {

            ableToBuy = true;
        }


    }


    private void OnTriggerExit2D(Collider2D collision){


        square.SetActive(false);
        speak.text= "";
    }



}

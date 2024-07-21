using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagerLevel4Script : MonoBehaviour
{

    public GameObject[] readhearts;
    public GameObject[] greyhearts;
    int lives;

    CharacterControllerScript ccs;
    GameObject cco;

    GameObject startDoor;
    GameObject endDoor; 
    GameObject bossGameObject;

    int coins;

    public Text textCoins;

    public Text timerText;


    float timer;



    private void Update()
    {
        lives = PlayerPrefs.GetInt("CurrentLifeHearts");
        coins = PlayerPrefs.GetInt("CurrentCoins");


        if (ccs.getDamaged())
        {
            if (lives > 0)
            {
                readhearts[lives].SetActive(false);
                greyhearts[lives].SetActive(true);
            }
            else
            {
                 PlayerPrefs.SetInt("LastLevel", 0);
                PlayerPrefs.SetInt("CurrentLevel",0);
                PlayerPrefs.SetInt("CurrentLifeHearts",3);
                PlayerPrefs.SetInt("CurrentCoins",0);
                PlayerPrefs.SetFloat("Timer",0.0f);
                SceneManager.LoadScene(7);
            }
        }


        if (coins > 0)
        {

            textCoins.text = "X " + coins.ToString();

        }


        timer+= Time.deltaTime;
        PlayerPrefs.SetFloat("Timer", timer);


        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        
        timerText.text = timerString;
    }


    private void Start()
    {


        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            
            PlayerPrefs.SetInt("CurrentLevel", 4);
            PlayerPrefs.SetInt("CurrentLifeHearts", 3);
            PlayerPrefs.SetInt("CurrentCoins", 0);
        }


        lives = PlayerPrefs.GetInt("CurrentLifeHearts");
        DisplayLives();

        coins = PlayerPrefs.GetInt("CurrentCoins");
        
        cco = GameObject.FindGameObjectWithTag("Character");
        ccs = cco.GetComponent<CharacterControllerScript>();

        startDoor = GameObject.FindGameObjectWithTag("doorStart");
        cco.transform.position = startDoor.transform.position;

        endDoor = GameObject.FindGameObjectWithTag("doorEnd");
        endDoor.GetComponent<DoorEndScript>().enabled = false;

        bossGameObject = GameObject.FindGameObjectWithTag("Boss");

        ccs.setCharcaterJump(0.25f);


        timer = PlayerPrefs.GetFloat("Timer", 0f);

        PlayerPrefs.SetInt("CurrentLevel", 4);

    }

    void DisplayLives()
    {
        for(int i = 0; i < readhearts.Length; i++) 
        {
            greyhearts[i].SetActive(true);
            readhearts[i].SetActive(true);
        } 
        for (int i = 0; i < lives && i < greyhearts.Length; i++)
        {
            greyhearts[i].SetActive(false);
        }

        for (int i = readhearts.Length - 1; i >= lives; i--)
        {
            readhearts[i].SetActive(false);
        }

    }
}

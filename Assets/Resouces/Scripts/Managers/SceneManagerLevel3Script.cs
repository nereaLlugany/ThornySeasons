using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagerLevel3Script : MonoBehaviour
{
    GameObject[] crystalIce;
    GameObject[] iceBoxes;
    GameObject firstIceBox;
    GameObject secondIceBox;

    GameObject firstCrystal;
    GameObject secondCrystal;
    BoxCollider2D[] SecondIceBC;


    public GameObject[] readhearts;
    public GameObject[] greyhearts;
    int lives;

    CharacterControllerScript ccs;
    GameObject cco;

    GameObject startDoor;

    int coins;

    public Text textCoins;

    public Text timerText;


    float timer;



    // Start is called before the first frame update
    void Start()
    {

        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            
            PlayerPrefs.SetInt("CurrentLevel", 3);
            PlayerPrefs.SetInt("CurrentLifeHearts", 3);
            PlayerPrefs.SetInt("CurrentCoins", 0);
        }

        lives = PlayerPrefs.GetInt("CurrentLifeHearts");
        DisplayLives();

        coins = PlayerPrefs.GetInt("CurrentCoins");

        crystalIce = GameObject.FindGameObjectsWithTag("triggerIce");
        firstCrystal = crystalIce[0].transform.position.x <= 31 ? crystalIce[0] : crystalIce[1];
        secondCrystal = crystalIce[0].transform.position.x >= 32 && crystalIce[0].transform.position.x <= 68 ? crystalIce[0] : crystalIce[1];

        iceBoxes = GameObject.FindGameObjectsWithTag("iceCrystal");
        firstIceBox = iceBoxes[0].transform.position.x <= 7 ? iceBoxes[0] : iceBoxes[1];
        firstIceBox.GetComponent<SpriteRenderer>().enabled = true;

        firstIceBox.transform.Find("Brown_Chest").GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 195 / 255f);
        firstIceBox.transform.Find("Brown_Chest").GetComponent<ChestControllerScript>().enabled = false;

        secondIceBox = iceBoxes[0].transform.position.x >= 7 && iceBoxes[0].transform.position.x <= 110 ? iceBoxes[0] : iceBoxes[1];
        secondIceBox.GetComponent<SpriteRenderer>().enabled = false;
        SecondIceBC = secondIceBox.GetComponents<BoxCollider2D>();
        Debug.Log(SecondIceBC.Length);
        for (int i = 0; i < SecondIceBC.Length; i++)
        {
            SecondIceBC[i].enabled = false;
        }

        GameObject.Find("Snowman").transform.Find("Brown_Chest_1").GetComponent<ChestControllerScript>().enabled = false;

        cco = GameObject.FindGameObjectWithTag("Character");
        ccs = cco.GetComponent<CharacterControllerScript>();

        startDoor = GameObject.FindGameObjectWithTag("doorStart");
        cco.transform.position = startDoor.transform.position;


        timer = PlayerPrefs.GetFloat("Timer", 0f);

    }

    // Update is called once per frame
    void Update()
    {
        lives = PlayerPrefs.GetInt("CurrentLifeHearts");
        coins = PlayerPrefs.GetInt("CurrentCoins");

        if (firstCrystal.GetComponent<IceCrystallControllerScript>().GetHit())
        {
            firstIceBox.GetComponent<SpriteRenderer>().enabled = false;
            firstIceBox.transform.Find("Brown_Chest").GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            firstIceBox.transform.Find("Brown_Chest").GetComponent<ChestControllerScript>().enabled = true;

        }
        
        if (secondCrystal.GetComponent<IceCrystallControllerScript>().GetHit())
        {
            secondIceBox.GetComponent<SpriteRenderer>().enabled = true;
            for (int i = 0; i < SecondIceBC.Length; i++)
            {
                SecondIceBC[i].enabled = true;
            }
        }
        
        if (GameObject.Find("Snowman").GetComponent<SnowmanControllerScript>().getFlowerTrigger())
        {
            GameObject.Find("Snowman").transform.Find("Brown_Chest_1").GetComponent<ChestControllerScript>().enabled = true;

        }


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


        timer += Time.deltaTime;
        PlayerPrefs.SetFloat("Timer", timer);


        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);


        timerText.text = timerString;

        PlayerPrefs.SetInt("CurrentLevel", 3);
    }

    void DisplayLives()
    {
        for (int i = 0; i < readhearts.Length; i++)
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

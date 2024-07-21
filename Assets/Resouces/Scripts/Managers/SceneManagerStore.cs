using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagerStore : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] readhearts;
    public GameObject[] greyhearts;
    int lives;
    int coins;
    public Text textCoins;



    private void Update()
    {

        lives = PlayerPrefs.GetInt("CurrentLifeHearts");
        DisplayLives();
        coins = PlayerPrefs.GetInt("CurrentCoins");
        


        if (coins >= 0)
        {

            textCoins.text = "X " + coins.ToString();

        }

        

    }


    private void Start()
    {


        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            
            PlayerPrefs.SetInt("CurrentLevel", 2);
            PlayerPrefs.SetInt("CurrentLifeHearts", 3);
            PlayerPrefs.SetInt("CurrentCoins", 0);
        }


        lives = PlayerPrefs.GetInt("CurrentLifeHearts");
        DisplayLives();

        coins = PlayerPrefs.GetInt("CurrentCoins");

        PlayerPrefs.SetInt("Shoop", 1);

        PlayerPrefs.SetInt("CurrentLevel", 5);

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

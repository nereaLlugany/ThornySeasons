using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DoorEndScript : MonoBehaviour
{
    // Start is called before the first frame update

    
    CharacterControllerScript characterCS;
    int currentUnitLifes;
    int currentCoinsUnits;
    

    void Start()
    {
        characterCS = GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterControllerScript>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character") && PlayerPrefs.GetInt("CurrentLevel") != 4)
        {
            currentCoinsUnits = characterCS.GetCoins();
            currentUnitLifes = characterCS.GetCharacterLives();
            PlayerPrefs.SetInt("CurrentLifeHearts", currentUnitLifes);
            PlayerPrefs.SetInt("CurrentCoins", currentCoinsUnits);
            SceneManager.LoadScene(1);

        } 
        else if (collision.gameObject.CompareTag("Character") && PlayerPrefs.GetInt("CurrentLevel") == 4 && GameObject.FindGameObjectWithTag("BossDeath"))
        {
            currentCoinsUnits = PlayerPrefs.GetInt("CurrentCoins");
            currentUnitLifes = PlayerPrefs.GetInt("CurrentLifeHearts");
            PlayerPrefs.SetInt("CurrentLifeHearts", currentUnitLifes);
            PlayerPrefs.SetInt("CurrentCoins", currentCoinsUnits);
            SceneManager.LoadScene(7);
        }
    }
}

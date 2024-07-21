using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    GameObject doorLevel1;
    GameObject doorLevel2;
    GameObject doorLevel3;
    GameObject doorLevel4;

    GameObject doorStore;


    [SerializeField] GameObject[] upDoorLevels;

    [SerializeField] GameObject[] doorFences;

    Rigidbody2D characterRigidBody;
    int currentLevel;
    bool characterAtDoor;

    int coins;

    public Text textCoins;
    public GameObject[] readhearts;
    public GameObject[] greyhearts;
    int lives;

    CharacterControllerScript ccs;
    GameObject cco;

    Vector3 storeDoorPosition;




    // Start is called before the first frame update
    void Start()
    {

        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            
            PlayerPrefs.SetInt("CurrentLevel", 2);
            PlayerPrefs.SetInt("CurrentLifeHearts", 3);
            PlayerPrefs.SetInt("CurrentCoins", 0);
        }

        
        lives = PlayerPrefs.GetInt("CurrentLifeHearts");
        Debug.Log(lives);
        DisplayLives();

        coins = PlayerPrefs.GetInt("CurrentCoins");

        doorLevel1 = GameObject.FindGameObjectWithTag("door1");
        doorLevel2 = GameObject.FindGameObjectWithTag("door2");
        doorLevel3 = GameObject.FindGameObjectWithTag("door3");
        doorLevel4 = GameObject.FindGameObjectWithTag("door4");
        doorStore = GameObject.FindGameObjectWithTag("shoop");


        characterRigidBody = GameObject.FindGameObjectWithTag("Character").GetComponent<Rigidbody2D>();
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");

        characterAtDoor = false;

        cco = GameObject.FindGameObjectWithTag("Character");
        ccs = cco.GetComponent<CharacterControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!characterAtDoor)
        {
            UpdateCharacterPosition();
        }

        if (coins > 0)
        {

            textCoins.text = "X " + coins.ToString();

        }
    }
    void UpdateCharacterPosition()
    {

        if (currentLevel == 0)
        {
            StartingPoint();

        }
        if (currentLevel == 1)
        {
            MoveCharacterToDoorPosition(doorLevel1.transform.position);
            ChangeColorDoor(currentLevel);
            CompletedLevelFence(currentLevel);
        }
        else if (currentLevel == 2)
        {
            MoveCharacterToDoorPosition(doorLevel2.transform.position);
            ChangeColorDoor(currentLevel);
            CompletedLevelFence(currentLevel);
        }
        else if (currentLevel == 3)
        {
            MoveCharacterToDoorPosition(doorLevel3.transform.position);
            ChangeColorDoor(currentLevel);
            CompletedLevelFence(currentLevel);
        }
        else if (currentLevel == 4)
        {
            MoveCharacterToDoorPosition(doorLevel4.transform.position);
            ChangeColorDoor(currentLevel);
        }
        else if (currentLevel == 5)
        {
            MoveCharacterToDoorPosition(doorStore.transform.position);
            ChangeColorDoor(currentLevel);
        }



    }

    void MoveCharacterToDoorPosition(Vector3 doorPosition)
    {
        characterRigidBody.velocity = Vector2.zero;
        characterRigidBody.transform.position = doorPosition;
        characterAtDoor = true;
    }

    void ChangeColorDoor(int level)
    {

        if (level == 5)
        {
            int lastLevelCompleted = PlayerPrefs.GetInt("LastLevel", 0);

            for (int i = 0; i < lastLevelCompleted; i++)
            {
                upDoorLevels[i].GetComponent<Animator>().enabled = false;
                upDoorLevels[i].GetComponent<SpriteRenderer>().color = Color.green;
                doorFences[i].SetActive(false);
            }

            if (lastLevelCompleted == 4)
            {
                doorStore.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                doorStore.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        else if (level >= 1 && level <= 4)
        {
            for (int i = 0; i < level - 1; i++)
            {
                upDoorLevels[i].GetComponent<Animator>().enabled = false;
                upDoorLevels[i].GetComponent<SpriteRenderer>().color = Color.green;
            }

            upDoorLevels[level - 1].GetComponent<Animator>().SetBool("CompletedLevel", true);
        }

    }

void CompletedLevelFence(int level)
{
    if (level == 5)
    {
        int lastLevelCompleted = PlayerPrefs.GetInt("lastLevel", 0);

        for (int i = 0; i < lastLevelCompleted; i++)
        {
            doorFences[i].GetComponent<Animator>().enabled = false;
            doorFences[i].SetActive(false);
        }
    }
    else if (level >= 1 && level <= 4)
    {
        for (int i = 0; i < level - 1; i++)
        {
            doorFences[i].GetComponent<Animator>().enabled = false;
            doorFences[i].SetActive(false);
        }

        doorFences[level - 1].GetComponent<Animator>().SetBool("CompletedLevelFence", true);
    }
}


    void StartingPoint()
    {
        for (int i = 0; i < upDoorLevels.Length; i++)
        {
            upDoorLevels[i].GetComponent<Animator>().enabled = true;
        }

        for (int i = 0; i < doorFences.Length; i++)
        {
            doorFences[i].GetComponent<Animator>().enabled = false;
        }
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


    public void SaveStoreDoorPosition(Vector3 position)
    {
        storeDoorPosition = position;
    }
}

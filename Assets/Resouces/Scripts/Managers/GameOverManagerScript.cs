using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManagerScript : MonoBehaviour
{
    Animator characterAnimator;
    SpriteRenderer characterSprite;
    Animator spikieAnimator;
    float characterShootTimer;
    bool characterShooting;
    float timerBeforeShoot;

    [SerializeField] GameObject bulletPrefab;
    GameObject rightEmtyGun;

    bool isSpikieJumping = false;
    float spikieJumpDuration;

    float firstPlace;
    float secondPlace;
    float thirdPlace;

    [SerializeField] Text firstPlaceText;
    [SerializeField] Text secondPlaceText;
    [SerializeField] Text thirdPlaceText;


    void Start()
    {
        characterAnimator = GameObject.FindGameObjectWithTag("Character").GetComponent<Animator>();
        characterSprite = GameObject.FindGameObjectWithTag("Character").GetComponent<SpriteRenderer>();
        spikieAnimator = GameObject.Find("Spiky").GetComponent<Animator>();

        characterAnimator.SetInteger("State", 1);

        characterShootTimer = Random.Range(5.0f, 10.0f);
        characterShooting = true;
        timerBeforeShoot = 0.5f;
        spikieJumpDuration = 0.8f;

        rightEmtyGun = GameObject.FindGameObjectWithTag("RightGun");

        if (!PlayerPrefs.HasKey("FirstPlace"))
        {
            PlayerPrefs.SetFloat("FirstPlace", float.MaxValue);
        }

        if (!PlayerPrefs.HasKey("SecondPlace"))
        {
            PlayerPrefs.SetFloat("SecondPlace", float.MaxValue);
        }

        if (!PlayerPrefs.HasKey("ThirdPlace"))
        {
            PlayerPrefs.SetFloat("ThirdPlace", float.MaxValue);
        }

        firstPlace = PlayerPrefs.GetFloat("FirstPlace");
        secondPlace = PlayerPrefs.GetFloat("SecondPlace");
        thirdPlace = PlayerPrefs.GetFloat("ThirdPlace");

        if (firstPlace != float.MaxValue)
        {
            firstPlaceText.text = ConvertTime(firstPlace);
        }
        else
        {
            firstPlaceText.text = "-";
        }

        if (secondPlace != float.MaxValue)
        {
            secondPlaceText.text = ConvertTime(secondPlace);
        }
        else
        {
            secondPlaceText.text = "-";
        }

        if (thirdPlace != float.MaxValue)
        {
            thirdPlaceText.text = ConvertTime(thirdPlace);
        }
        else
        {
            thirdPlaceText.text = "-";
        }

        if (PlayerPrefs.GetInt("CurrentLevel") == 4)
        {
            float totalTimer = PlayerPrefs.GetFloat("Timer");
            string timerString = ConvertTime(PlayerPrefs.GetFloat("Timer"));

            if (totalTimer < firstPlace)
            {
                thirdPlace = secondPlace;
                PlayerPrefs.SetFloat("ThirdPlace", thirdPlace);

                secondPlace = firstPlace;
                PlayerPrefs.SetFloat("SecondPlace", secondPlace);

                firstPlace = totalTimer;
                PlayerPrefs.SetFloat("FirstPlace", firstPlace);
                
                if (firstPlace != float.MaxValue)
                {

                    if (thirdPlace != float.MaxValue)
                    {
                        thirdPlaceText.text = ConvertTime(secondPlace);
                    }
                    else
                    {
                        thirdPlaceText.text = "-";
                    }

                    if (secondPlace != float.MaxValue)
                    {
                        secondPlaceText.text = ConvertTime(firstPlace);
                    }
                    else
                    {
                        secondPlaceText.text = "-";
                    }
                    firstPlaceText.text = timerString;
                }
                else
                {
                    firstPlaceText.text = "-";
                }

            }
            else if (totalTimer < secondPlace)
            {
                thirdPlace = secondPlace;
                PlayerPrefs.SetFloat("ThirdPlace", thirdPlace);

                secondPlace = totalTimer;
                PlayerPrefs.SetFloat("SecondPlace", secondPlace);

                if (secondPlace != float.MaxValue)
                {

                    if (thirdPlace != float.MaxValue)
                    {
                        thirdPlaceText.text = ConvertTime(secondPlace);
                    }
                    else
                    {
                        thirdPlaceText.text = "-";
                    }
                    secondPlaceText.text = timerString;
                }
                else
                {
                    secondPlaceText.text = "-";
                }
            }
            else if (totalTimer < thirdPlace)
            {
                thirdPlace = totalTimer;
                PlayerPrefs.SetFloat("ThirdPlace", thirdPlace);

                if (thirdPlace != float.MaxValue)
                {
                    thirdPlaceText.text = timerString;
                }
                else
                {
                    thirdPlaceText.text = "-";
                }
            }
        }

    }

    void Update()
    {
        if (characterShooting)
        {
            characterShootTimer -= Time.deltaTime;

            if (characterShootTimer <= 0)
            {
                characterAnimator.SetInteger("State", 3);
                GameObject bulletClone = Instantiate(bulletPrefab, rightEmtyGun.transform.position, transform.rotation) as GameObject;
                bulletClone.GetComponent<BulletControllerScript>().SetDirection(characterSprite.flipX);
                characterShooting = false;
            }
        }
        else
        {
            timerBeforeShoot -= Time.deltaTime;

            if (timerBeforeShoot <= 0)
            {
                characterAnimator.SetInteger("State", 1);
                characterShooting = true;
                characterShootTimer = Random.Range(5.0f, 10.0f);
                timerBeforeShoot = 0.5f;
            }
        }

        if (isSpikieJumping)
        {
            spikieJumpDuration -= Time.deltaTime;
            if (spikieJumpDuration <= 0)
            {
                spikieAnimator.SetBool("IsJumping", false);
                isSpikieJumping = false;
                spikieJumpDuration = 0.8f;
            }
        }
    }

    string ConvertTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        return timerString;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            spikieAnimator.SetBool("IsJumping", true);
            isSpikieJumping = true;
        }
    }

    public void restartButtonClick()
    {
        SceneManager.LoadScene(0);
        firstPlace = PlayerPrefs.GetFloat("FirstPlace");
        secondPlace = PlayerPrefs.GetFloat("SecondPlace");
        thirdPlace = PlayerPrefs.GetFloat("ThirdPlace");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("ThirdPlace", thirdPlace);
        PlayerPrefs.SetFloat("SecondPlace", secondPlace);
        PlayerPrefs.SetFloat("FirstPlace", firstPlace);



    }

    public void quitButtonClick()
    {
        Application.Quit();
    }
}

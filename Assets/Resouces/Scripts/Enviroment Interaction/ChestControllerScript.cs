using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestControllerScript : MonoBehaviour
{
    private Animator anim;
    private bool isPlayerInside; 

    public GameObject e;

    [SerializeField] GameObject coin;

    int numberCoins;

    private bool getCoin;



    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        isPlayerInside=false;
        e.SetActive(false);
        getCoin=false;
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.E) && isPlayerInside && getCoin==false)
        {
            Open();
            getCoin= true;
            GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterControllerScript>().IncremeantCoins();
        
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        e.SetActive(true);
        isPlayerInside = true;
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        
        e.SetActive(false);
        isPlayerInside = false;
        
        
    }

    void Open()
    {
        
        anim.enabled = true;
        ShowCoin();
        getCoin = false;
    }


    void ShowCoin()
    {
        Vector3 positionCoin = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
        Instantiate(coin, positionCoin, transform.rotation);
    }



}


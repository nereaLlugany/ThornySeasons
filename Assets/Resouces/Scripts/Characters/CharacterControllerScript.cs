using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    Animator playerAnimator;
    Rigidbody2D playerRigidBody;
    SpriteRenderer playerSprite;

    [SerializeField] public float speed;
    float jumpForce = 20.0f;


    [SerializeField] float moveX;
    [SerializeField] float moveY;

    [SerializeField] GameObject bulletPrefab;
    GameObject leftEmtyGun;
    GameObject rightEmtyGun;

    Vector2 currentVelocity;

    int characterLives;

    bool playerShooting;
    bool ableToJump;
    bool playerJumping;

    float shootingTime;

    int numberCoins;


    bool isDamaged;
    float damageCooldown = 2.0f;

    bool characterInScoup;


    GameObject boss; 
    bool isCombatStarted = false;

     Collider2D startCombatCollider;


    [SerializeField] AudioClip jump;

    [SerializeField] AudioClip hit;






    // Start is called before the first frame update
    void Awake()
    {

        playerAnimator = gameObject.GetComponent<Animator>();
        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();

    }

    void Start()
    {

        ableToJump = true;
        playerShooting = false;
        playerJumping = false;

        //speed = 100.0f;
        moveX = 0.0f;

        characterLives = PlayerPrefs.GetInt("CurrentLifeHearts");
        numberCoins = PlayerPrefs.GetInt("CurrentCoins");

        rightEmtyGun = GameObject.FindGameObjectWithTag("RightGun");
        leftEmtyGun = GameObject.FindGameObjectWithTag("LeftGun");

        shootingTime = 0.25f;

        isDamaged = false;
        characterInScoup = false;


        boss = GameObject.FindGameObjectWithTag("Boss");
        if (boss != null)
        {
            startCombatCollider = boss.GetComponent<Collider2D>();
        }


    }

    // Update is called once per frame
    void Update()
    {
        moveY = 0.0f;

        if ((ableToJump && !playerShooting) || characterInScoup)
        {
            currentVelocity = playerRigidBody.velocity;

            if (Input.GetKey(KeyCode.Space))
            {
                playerAnimator.SetInteger("State", 2);
                playerJumping = true;

                moveY = 20.0f;

                gameObject.GetComponent<AudioSource>().PlayOneShot(jump);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerAnimator.SetInteger("State", 1);
                moveX = -1.0f;
                playerSprite.flipX = true;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                playerAnimator.SetInteger("State", 1);
                moveX = 1.0f;
                playerSprite.flipX = false;
            }
            else
            {
                playerAnimator.SetInteger("State", 0);
                moveX = 0.0f;
                currentVelocity.x = 0.0f;
                playerRigidBody.velocity = new Vector2(currentVelocity.x, currentVelocity.y);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            playerAnimator.SetInteger("State", 3);
            playerShooting = true;
            if (playerSprite.flipX)
            {
                GameObject bulletClone = Instantiate(bulletPrefab, leftEmtyGun.transform.position, transform.rotation) as GameObject;
                bulletClone.GetComponent<BulletControllerScript>().SetDirection(playerSprite.flipX);
            }
            else
            {
                GameObject bulletClone = Instantiate(bulletPrefab, rightEmtyGun.transform.position, transform.rotation) as GameObject;
                bulletClone.GetComponent<BulletControllerScript>().SetDirection(playerSprite.flipX);
            }

        }

        if (playerShooting)
        {
            shootingTime -= Time.deltaTime;
            currentVelocity.x = 0.0f;
            playerRigidBody.velocity = new Vector2(0.0f, 0.0f);

            if (shootingTime <= 0.0f)
            {
                playerShooting = false;
                shootingTime = 0.25f;
            }

        }

    }

    void FixedUpdate()
    {
        if (ableToJump && !playerShooting)
        {
            playerRigidBody.AddForce(new Vector2(moveX * speed, moveY * jumpForce));

            if (currentVelocity.x != 0.0f)
            {
                currentVelocity.x = playerSprite.flipX ? -1.0f : 1.0f;
            }

            playerRigidBody.velocity = currentVelocity;

        }

        if (playerJumping)
        {
            playerAnimator.SetFloat("VelocityDir", playerRigidBody.velocity.y);

        }

        if (characterInScoup)
        {
            playerRigidBody.gravityScale = 0.0f;
        }
        else
        {
            playerRigidBody.gravityScale = 1.0f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("floor") || collision.gameObject.CompareTag("ice") || collision.gameObject.CompareTag("iceCrystal"))
        {
            ableToJump = true;
        }
        else if (collision.gameObject.CompareTag("ramp"))
        {
            characterInScoup = true;
            ableToJump = true;
        }


        if (collision.gameObject.CompareTag("startCombat") && boss != null)
        {
            StartCombat();
        }

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("floor") || collision.gameObject.CompareTag("ice") || collision.gameObject.CompareTag("iceCrystal"))
        {
            ableToJump = true;
        }
        else if (collision.gameObject.CompareTag("ramp"))
        {
            characterInScoup = true;
            ableToJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("floor") || collision.gameObject.CompareTag("ice") || collision.gameObject.CompareTag("iceCrystal"))
        {
            ableToJump = false;
        }
        else if (collision.gameObject.CompareTag("ramp"))
        {
            characterInScoup = false;
            ableToJump = false;
        }

        if (collision.gameObject.CompareTag("startCombat") && boss != null)
        {
            startCombatCollider.isTrigger = false;
        }
        
        

    }

    public int GetCharacterLives()
    {
        return characterLives;
    }

    public void SetCharacterLives(int cl)
    {
        characterLives = cl;
    }

    public void DecreaseLives()
    {
        if (!isDamaged)
        {

            gameObject.GetComponent<AudioSource>().PlayOneShot(hit);


            characterLives--;

            StartCoroutine(DamageEffect());

            isDamaged = true;
            StartCoroutine(ResetDamageCooldown());
            PlayerPrefs.SetInt("CurrentLifeHearts", characterLives);

           

        }
    }

    IEnumerator DamageEffect()
    {
        playerSprite.color = new Color(0.6f, 0.4f, 0.8f, 1f);
        yield return new WaitForSeconds(0.1f);
        playerSprite.color = Color.white;
    }

    IEnumerator ResetDamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldown);
        isDamaged = false;
    }


    public int GetCoins()
    {
        return numberCoins;
    }

    public void SetCoins(int c)
    {
        numberCoins = c;
    }

    public void IncremeantCoins()
    {

        numberCoins++;
        PlayerPrefs.SetInt("CurrentCoins", numberCoins);

    }

    private IEnumerator DamageEffect(float duration)
    {
        playerSprite.color = new Color(0.6f, 0.4f, 0.8f, 1f); ;
        yield return new WaitForSeconds(duration);
        playerSprite.color = Color.white;
    }

    public bool getDamaged()
    {
        return isDamaged;
    }

    public void setCharcaterJump(float jump)
    {
        Debug.Log(jumpForce);
        jumpForce = jump;
        Debug.Log(jumpForce);

    }

    public void Comprar()
    {

        characterLives += 1;
        PlayerPrefs.SetInt("CurrentLifeHearts", characterLives);

        numberCoins -= 2;
        PlayerPrefs.SetInt("CurrentCoins", numberCoins);


        PlayerPrefs.Save();

        

    }


     void StartCombat()
    {
        if (!isCombatStarted && boss != null)
        {
            boss.GetComponent<BossControllerScript>().StartCombat();
            isCombatStarted = true;
        }
    }
}

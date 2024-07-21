using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowSpikeScript : MonoBehaviour
{


    float moveX = 1.0f;
    SpriteRenderer YellowSpikeSprite;
    [SerializeField] float speed = 5.0f;
    Rigidbody2D playerRigidBody;


    private SpriteRenderer spriteRenderer;




    [SerializeField] GameObject explosionPrefab;

    int lives;

    int maxlives;

    [SerializeField] private HealthBarScript healthBar;
    void Start()
    {
        YellowSpikeSprite = gameObject.GetComponent<SpriteRenderer>();
        speed = 3.0f;
        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
        lives = 5;
        maxlives = 5;
        healthBar.UpdateHealthBar(maxlives, lives);


        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        float moveAmount = moveX * speed * Time.deltaTime;


        transform.position += new Vector3(moveAmount, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            lives -= 1;
            collision.gameObject.GetComponent<BulletControllerScript>().DestroyBullet();
            float dameDuratiom = 0.1f;
            healthBar.UpdateHealthBar(maxlives, lives);

            if (lives == 0)
            {
                Destroy(gameObject);
                Instantiate(explosionPrefab, transform.position, transform.rotation);
            }
            else if (lives > 0)
            {
                StartCoroutine(DamageEffect(dameDuratiom));
            }
        }
        else if (collision.gameObject.CompareTag("Character"))
        {
            GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterControllerScript>().DecreaseLives();
        }

        if (collision.gameObject.CompareTag("TurnLeft"))
        {
            moveX = -1.0f;
            YellowSpikeSprite.flipX = true;
        }
        else if (collision.gameObject.CompareTag("TurnRight"))
        {
            moveX = 1.0f;
            YellowSpikeSprite.flipX = false;
        }
    }

    private IEnumerator DamageEffect(float duration)
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = Color.white;
    }



}


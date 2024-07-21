using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossControllerScript : MonoBehaviour
{
    GameObject player;
    bool isDefeated;
    float totalTimer;
    float speed;

    Animator bossAnimator;

    SpriteRenderer bossSpriteRenderer;
    Rigidbody2D bossRigidBody;
    Vector3 initialPosition;

    int lives;

    int maxlives;


    [SerializeField] private HealthBarScript healthBar;

    [SerializeField] GameObject explosionPrefab;

    [SerializeField] private Image healthGrey;

    [SerializeField] private Image healthRed;

    bool start;

    int currentState;

    GameObject hammerGameObject;
    GameObject buttGameObject;
    bool isAttacking;
    bool isSelectingAttack;
    bool isJumping;

    [SerializeField] AudioClip boss;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Character");
        isDefeated = false;
        speed = 0f;

        bossSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        bossRigidBody = gameObject.GetComponent<Rigidbody2D>();
        bossAnimator = gameObject.GetComponent<Animator>();

        bossAnimator.SetInteger("State", 0);


        lives = 50;
        maxlives = 50;
        healthGrey.enabled = false;
        healthRed.enabled = false;

        start = false;
        initialPosition = transform.position;
        currentState = 0;

        hammerGameObject = GameObject.FindGameObjectWithTag("Hammer");
        hammerGameObject.GetComponent<CircleCollider2D>().enabled = false;

        buttGameObject = GameObject.FindGameObjectWithTag("Butt");
        buttGameObject.GetComponent<CapsuleCollider2D>().enabled = false;

        isAttacking = false;
        isSelectingAttack = false;
        isJumping = false;
    }

    void Update()
    {
        if (isDefeated)
        {
            totalTimer = PlayerPrefs.GetFloat("Timer");
            PlayerPrefs.SetFloat("TotalTimer", totalTimer);
        }
        else
        {
            if (start)
            {
                CheckDistance();
            }
        }

    }

    void CheckDistance()
    {
        LookPlayer();
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        isAttacking = false;
        if ((distanceToPlayer - 0.5f) <= 3.5f)
        {
            if (!isAttacking && !isSelectingAttack && !isJumping)
            {
                isSelectingAttack = true;
                int randomAttack = Random.Range(2, 5);
                currentState = randomAttack;
                PerformAttack(currentState);
            }
        }
        else if ((distanceToPlayer - 0.5f) <= 9.0f && (distanceToPlayer - 0.5f) > 3.5f)
        {
            MoveTowardsPlayer();
            currentState = 1;
        }
        else if ((distanceToPlayer - 0.5f) > 9.0f)
        {
            ReturnInitialPosition();
            if (bossRigidBody.position.x != 125.0f)
            {
                currentState = 1;
            }
            else
            {
                currentState = 0;
            }
        }


        bossAnimator.SetInteger("State", currentState);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && start == true)
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
        else if (collision.gameObject.CompareTag("Head"))
        {
            if (gameObject.transform.position.x > player.transform.position.x)
            {
                player.transform.position = new Vector3(player.transform.position.x - 0.8f, player.transform.position.y, player.transform.position.z);
            }
            else
            {
                player.transform.position = new Vector3(player.transform.position.x + 0.8f, player.transform.position.y, player.transform.position.z);
            }
        }

        else if (collision.gameObject.CompareTag("Character"))
        {
            bossRigidBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);

            GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterControllerScript>().DecreaseLives();

        }
        else if (collision.gameObject.CompareTag("floor"))
        {
            isJumping = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            isJumping = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            isJumping = true;
        }
    }

    private IEnumerator DamageEffect(float duration)
    {
        bossSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(duration);
        bossSpriteRenderer.color = new Color(131, 255, 194, 255);
    }


    public float GetTotalTimer()
    {
        return totalTimer;
    }

    public bool GetIsDefeated()
    {
        return isDefeated;
    }

    public void LookPlayer()
    {
        bossAnimator.SetInteger("State", 0);


        if (gameObject.transform.position.x > player.transform.position.x && bossSpriteRenderer.flipX)
        {
            bossSpriteRenderer.flipX = false;
        }
        else if (gameObject.transform.position.x < player.transform.position.x && !bossSpriteRenderer.flipX)
        {
            bossSpriteRenderer.flipX = true;
        }

    }

    void MoveTowardsPlayer()
    {
        if (!isJumping && !isAttacking)
        {
            Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }
    void PerformAttack(int randomAttack)
    {
        switch (randomAttack)
        {
            case 2:
                currentState = 2;
                break;

            case 3:
                currentState = 3;
                break;

            case 4:
                currentState = 4;
                break;
        }

        bossAnimator.SetInteger("State", currentState);

    }

    void ReturnInitialPosition()
    {
        if (bossRigidBody.velocity.x > 0)
        {
            bossSpriteRenderer.flipX = false;
        }
        else
        {
            bossSpriteRenderer.flipX = true;
        }
        transform.position = Vector3.MoveTowards(transform.position, initialPosition, Time.deltaTime * 2f);
    }
    public void StartCombat()
    {
        speed = 3.0f;
        LookPlayer();
        bossAnimator.SetInteger("State", 0);
        healthGrey.enabled = true;
        healthRed.enabled = true;
        lives = 50;
        maxlives = 50;
        healthBar.UpdateHealthBar(maxlives, lives);
        start = true;

        gameObject.GetComponent<AudioSource>().PlayOneShot(boss);

    }

    public void Attack()
    {
        if (!isJumping && !isAttacking)
        {
            isAttacking = true;
            hammerGameObject.GetComponent<CircleCollider2D>().enabled = false;
            buttGameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        }
        else
        {
            hammerGameObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    public void Jump()
    {
        if (!isJumping && !isAttacking)
        {

            isAttacking = true;
            isJumping = true;
            buttGameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            hammerGameObject.GetComponent<CircleCollider2D>().enabled = false;
            bossRigidBody.velocity = new Vector2(0.0f, 0.0f);
            Vector3 directionToPlayer = player.transform.position - transform.position;

            bossRigidBody.AddForce(new Vector2(directionToPlayer.x, 35.0f) * 10.0f);
            GameObject.FindGameObjectWithTag("Boss").GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            buttGameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }

    public void JumpAttack()
    {
        if (!isJumping && !isAttacking)
        {
            isJumping = true;
            isAttacking = true;
            hammerGameObject.GetComponent<CircleCollider2D>().enabled = false;
            buttGameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            bossRigidBody.velocity = new Vector2(0.0f, 0.0f);
            Vector3 directionToPlayer = player.transform.position - transform.position;

            bossRigidBody.AddForce(new Vector2(directionToPlayer.x, 35.0f) * 10.0f);
            GameObject.FindGameObjectWithTag("Boss").GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            hammerGameObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    public void EndAttacks()
    {
        isAttacking = currentState == 0 || currentState == 1 ? false : true;
        hammerGameObject.GetComponent<CircleCollider2D>().enabled = false;
        buttGameObject.GetComponent<CapsuleCollider2D>().enabled = false;

        isJumping = bossRigidBody.velocity.y == 0.0f ? false : true;

        currentState = 1;
        bossAnimator.SetInteger("State", currentState);

        isSelectingAttack = false;

        CheckDistance();
    }
}



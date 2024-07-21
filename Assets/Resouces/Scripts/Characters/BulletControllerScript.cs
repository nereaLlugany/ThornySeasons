using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletControllerScript : MonoBehaviour
{
    [SerializeField] float speed;
    float lifeTimeBullet;
    float dirX;


    [SerializeField] GameObject hitBulletPrefab;


    [SerializeField] AudioClip audioBullet;

    // Start is called before the first frame update
    void Start()
    {
        speed = 20.0f;
        lifeTimeBullet = 2.5f;
        gameObject.GetComponent<AudioSource>().PlayOneShot(audioBullet);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimeBullet -= Time.deltaTime;

        if(lifeTimeBullet <= 0.0f)
        {
            Destroy(gameObject);
        }
            
        transform.Translate(new Vector2(dirX, 0.0f) * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            DestroyBullet();
        }
    }
    public void SetDirection(bool flipped)
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = flipped;
        dirX = flipped ? -1.0f : 1.0f;
    }


    public void DestroyBullet()
    {
        Destroy(gameObject);
        Instantiate(hitBulletPrefab, transform.position, transform.rotation);
    }

}

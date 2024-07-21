using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCrystallControllerScript : MonoBehaviour
{
    SpriteRenderer crystalSpriteRenderer;
    bool hasBeenHit;
    // Start is called before the first frame update
    void Start()
    {
        crystalSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        hasBeenHit = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GetHit()
    {
        return hasBeenHit; 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hasBeenHit = true; 
            crystalSpriteRenderer.color = new Color(132 / 255f, 255 / 255f, 146 / 255f);
            Destroy(collision.gameObject);

        }
    }
}

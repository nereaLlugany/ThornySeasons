using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitControllerScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] AudioClip audioHit;
    void Start()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(audioHit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void destroyHit()
    {
        Destroy(gameObject);
    }
}

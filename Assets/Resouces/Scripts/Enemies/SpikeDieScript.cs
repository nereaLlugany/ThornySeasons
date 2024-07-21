using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDieScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] AudioClip death;
    void Start()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(death);
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

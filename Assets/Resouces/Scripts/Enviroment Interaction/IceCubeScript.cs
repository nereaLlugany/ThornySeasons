using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceCubeScript : MonoBehaviour
{
    Animator iceCubeAnimator;
    bool animationPlayed;

    // Start is called before the first frame update
    void Start()
    {
        iceCubeAnimator = gameObject.GetComponent<Animator>();
        animationPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character") && !animationPlayed)
        {
            iceCubeAnimator.SetBool("ShouldFall", true);
            animationPlayed = true;
        }
    }

    public void ResetBool()
    {
        iceCubeAnimator.SetBool("ShouldFall", false);
        animationPlayed = false;
    }
}

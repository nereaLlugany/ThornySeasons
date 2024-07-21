using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float minX;

    [SerializeField] float maxX;

    [SerializeField] float minY;

    [SerializeField] float maxY;

    [SerializeField] GameObject player;



    void Start()
    {
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float camX = player.transform.position.x;
        float camY = player.transform.position.y;

        

        if (camX < minX)
        {
            camX = minX;
            
        }
        else if(camX> maxX)
        {

            camX = maxX;
        }




        if (camY < minY)
        {
            camY = minY;
        }
        else if (camY > maxY)
        {

            camY = maxY;
        }


        transform.position = new Vector3(camX, camY, -1.0f);




    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffectScript : MonoBehaviour
{
    [SerializeField] float parallaxMultiplier;
    Camera mainCamera;
    Vector3 prevoiusCameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        prevoiusCameraPosition = mainCamera.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float deltaX = (mainCamera.transform.position.x - prevoiusCameraPosition.x) * parallaxMultiplier;
        transform.Translate(new Vector3(deltaX, 0.0f, 0.0f));
        prevoiusCameraPosition = mainCamera.transform.position;
    }
}

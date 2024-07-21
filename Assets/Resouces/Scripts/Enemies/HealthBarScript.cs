using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    // Start is called before the first frame update
[SerializeField] private Image barImage;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateHealthBar( float maxHealth, float health){

        barImage.fillAmount = health / maxHealth;

    }
}

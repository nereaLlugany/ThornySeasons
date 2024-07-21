using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            
            PlayerPrefs.SetInt("CurrentLevel", 0);
            PlayerPrefs.SetInt("CurrentLifeHearts", 3);
            PlayerPrefs.SetInt("CurrentCoins", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startButtonClick()
    {
        SceneManager.LoadScene(1);
    }
}

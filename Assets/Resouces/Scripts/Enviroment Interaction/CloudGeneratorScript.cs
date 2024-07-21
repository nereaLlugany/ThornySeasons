using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class GeneratorControllerScript : MonoBehaviour
    {
        public GameObject[] cloudsPrefabs;
        float cloudsTimer;
        float minCloudSpeed = 0.1f;
        float maxCloudSpeed = 0.5f;

        // Start is called before the first frame update
        void Start()
        {
            cloudsTimer = 10.0f;
        }

        // Update is called once per frame
        void Update()
        {
            cloudsTimer -= Time.deltaTime;
            if (cloudsTimer <= 0.0f)
            {
                float posY = Random.Range(-1.8f, 4.4f);
                Vector3 iniPosAst = new Vector3(-12.0f, posY, 0.0f);
                int cloudNum = Random.Range(0, 2);

                float scale = Random.Range(0.5f, 0.8f);
                Vector3 scaleVector = new Vector3(scale, scale, 1.0f);

                GameObject newCloud = Instantiate(cloudsPrefabs[cloudNum], iniPosAst, Quaternion.identity);
                newCloud.transform.localScale = scaleVector;
                cloudsTimer = 10.0f;
            }

            MoveClouds();
        }

        void MoveClouds()
        {
            float cloudSpeed = Random.Range(minCloudSpeed, maxCloudSpeed);
            GameObject[] clouds = GameObject.FindGameObjectsWithTag("Cloud");
            foreach (GameObject cloud in clouds)
            {
                cloud.transform.Translate(Vector3.right * cloudSpeed * Time.deltaTime);
            }
        }
    }



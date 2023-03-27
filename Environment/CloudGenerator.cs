using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    [SerializeField] GameManager gM;
    public GameObject[] clouds;
    [SerializeField] float cloudSpawnTime;
    [SerializeField] float maxCloudTime = 30;
    public int chanceToSpawn = 30;
    public int direction = 1;  // 1 - From Left, -1 - From Right
    
    void Start()
    {
        gM = FindObjectOfType<GameManager>();
        
    }
    // Update is called once per frame
    void Update()
    {
        if(gM.difficulty <= 16)
        {
            UpdateClouds();
        }
        if (cloudSpawnTime <= 0)
        {
            SpawnCloud();
            ResetCloud();
        }
        else
        {
            cloudSpawnTime -= Time.deltaTime;
        }
    }
    void UpdateClouds()
    {
        if(gM.difficulty == 6)
        {
            maxCloudTime = 20;
            chanceToSpawn = 25;
        }
        else if (gM.difficulty == 9)
        {
            maxCloudTime = 20;
            chanceToSpawn = 35;
        }
        else if (gM.difficulty == 12)
        {
            maxCloudTime = 15;
            chanceToSpawn = 45;
        }
        else if (gM.difficulty == 15)
        {
            maxCloudTime = 10;
            chanceToSpawn = 65;
        }
        else if (gM.difficulty == 20)
        {
            maxCloudTime = 5;
            chanceToSpawn = 75;
        }

    }
    void SpawnCloud()
    {
        int chance = Random.Range(1, 100);
        if(chance <= chanceToSpawn)
        {
            int cloudSelect = Random.Range(0, clouds.Length);
            //float randPOS = Random.Range(transform.localPosition.y - 6, transform.localPosition.y + 6);
            float randPOS = Random.Range(2.0f, 5.0f);
            Vector2 spawnLocation = new Vector3(transform.position.x, randPOS);
            GameObject newCloud = (GameObject)Instantiate(clouds[cloudSelect], spawnLocation, transform.rotation);
            newCloud.GetComponent<CloudController>().direction = direction;
            //float size = Random.Range(0.3f, 1.0f);
            //newCloud.transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(1, 4));
            //newCloud.transform.localScale = new Vector3(size, size, 1);
        }
        
    }
    void ResetCloud()
    {
        cloudSpawnTime = Random.Range(0, maxCloudTime);
    }
}

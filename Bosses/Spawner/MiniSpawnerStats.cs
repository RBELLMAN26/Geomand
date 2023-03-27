using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSpawnerStats : MonoBehaviour
{
    public GameObject[] enemies; //Enemies For Spawn
    public GameObject[] enemyToSpawn = new GameObject[2]; //Selected Spawns
    public Sprite[] icons = new Sprite[2];
    public SpriteRenderer[] iconObjs = new SpriteRenderer[2];
    public GameObject[] hitPoints;
    public EnemyBossText eBT;
    public int health;
    public int hitHealth = 100;
    public float rateOfSpawn, maxRateOfSpawn;
    public bool innerSpawner;
    // Start is called before the first frame update
    void Start()
    {
        enemyToSpawn[0] = enemies[Random.Range(0, enemies.Length)];
        icons[0] = enemyToSpawn[0].GetComponent<SpriteRenderer>().sprite;
        iconObjs[0].sprite = icons[0];
        
        if(!innerSpawner)
        {
            enemyToSpawn[1] = enemies[Random.Range(0, enemies.Length)];
            icons[1] = enemyToSpawn[1].GetComponent<SpriteRenderer>().sprite;
            iconObjs[1].sprite = icons[1];
        }
    }
}

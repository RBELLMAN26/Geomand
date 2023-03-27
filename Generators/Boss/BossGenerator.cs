using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    public GameManager gM;
    public GameObject[] bosses;  //Three Bosses Total, Spawner, Attacker, Defender
    public float spawnChance = 10; //Percentage Of Chance To Spawn A Boss
    public float timeForChance = 180; // Every three minutes checks for bosses.
    public float maxTimeForChance = 180;
    public bool enableBosses;

    private void Start()
    {
        gM = FindObjectOfType<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if(gM.timeInMatch > 300 && !enableBosses)
        {
            enableBosses = true;
        }
        if(enableBosses)
        {
            updateChance();
        }
    }
    void updateChance()
    {
        if (timeForChance <= 0)
        {
            int chance = Random.Range(0, 100);
            if(chance <= spawnChance)
            {
                SpawnBoss();
            }
            else
            {
                timeForChance = maxTimeForChance;
            }
        }
        else
        {
            timeForChance -= Time.deltaTime;
        }
    }
    void SpawnBoss()
    {
        int rand = Random.Range(0, 3);
        GameObject newBoss = (GameObject)Instantiate(bosses[rand], transform.position, transform.rotation);
    }
}

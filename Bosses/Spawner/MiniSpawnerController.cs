using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MiniSpawnerController : MonoBehaviour
{
    public MiniSpawnerStats mSS;
    // Start is called before the first frame update
    void Start()
    {
        mSS = GetComponent<MiniSpawnerStats>();
        for(int i = 0; i < mSS.hitPoints.Length; i++)
        {
            mSS.hitPoints[i].GetComponent<HitPoint>().health = mSS.hitHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(mSS.rateOfSpawn > 0)
        {
            mSS.rateOfSpawn -= Time.deltaTime;
        }
        else
        {
            CreateEnemy();
            mSS.rateOfSpawn = mSS.maxRateOfSpawn;
        }
    }


    public void CreateEnemy()
    {
        for(int i = 0; i < mSS.enemyToSpawn.Length; i++)
        {
            GameObject newEnemy = (GameObject)Instantiate(mSS.enemyToSpawn[i], mSS.iconObjs[i].transform.position, transform.rotation);
        }
    }
    public void LoseHealth(int amount)
    {
        int newHealth = mSS.health -= amount;
        if(newHealth > 0)
        {
            mSS.health = newHealth;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            LoseHealth(collision.GetComponent<BulletStat>().damage);
            Destroy(collision.gameObject);
        }
    }
}

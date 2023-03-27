using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defect : MonoBehaviour
{

    public EnemyInfo eI;
    public EnemyController eC;

    public float shieldRate, maxShieldRate;
    public float reloadRate, maxReloadRate;
    public int amountToShield = 10;

    public GameObject shieldWave;
    // Start is called before the first frame update
    void Start()
    {
        eI = GetComponent<EnemyInfo>();
        eC = GetComponent<EnemyController>();
        CheckDifficulty();
        if (eI.chanceOfShield > 0)
        {
            int shieldChance = Random.Range(1, 100);
            if (shieldChance <= eI.chanceOfShield)
            {
                eC.ActivateShield(15, true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        eC.Movement(Vector2.down);
        ChargeShield();
    }

    public void ChargeShield()
    {
        if (reloadRate <= 0)
        {
            if (shieldRate > 0)
            {
                shieldRate -= Time.deltaTime;
            }
            else
            {
                FireShield();
                shieldRate = maxShieldRate;
                reloadRate = maxReloadRate;
            }

        }
        else
        {
            reloadRate -= Time.deltaTime;
        }

    }
    public void FireShield()
    {
        GameObject newShield = (GameObject)Instantiate(shieldWave, transform.position, transform.rotation);
        newShield.GetComponent<ShieldWave>().maxTime = amountToShield;
    }

    void CheckDifficulty()
    {
        eI.difficulty -= 10;
        if (eI.difficulty <= 10)
        {
            switch (eI.difficulty)
            {
                case 0:
                    {
                        SetEnemyStats(300, 400, 10);
                        break;
                    }
                case 1:
                    {
                        SetEnemyStats(350, 500, 10);
                        break;
                    }
                case 2:
                    {
                        SetEnemyStats(400, 600, 20);
                        break;
                    }
                case 3:
                    {
                        SetEnemyStats(500, 800, 30);
                        break;
                    }
                case 4:
                    {
                        SetEnemyStats(600, 1250, 40);
                        break;
                    }
                case 5:
                    {
                        SetEnemyStats(700, 1500, 50);
                        break;
                    }
                case 6:
                    {
                        SetEnemyStats(800, 1750, 60);
                        break;
                    }
                case 7:
                    {
                        SetEnemyStats(1000, 2500, 70);
                        break;
                    }
                case 8:
                    {
                        SetEnemyStats(1500, 3750, 80);
                        break;
                    }
                case 9:
                    {
                        SetEnemyStats(1800, 4000, 90);
                        break;
                    }
                case 10:
                    {
                        SetEnemyStats(2500, 5000, 100);
                        
                        break;
                    }
            }
        }
        else//if diffulty goes past 10 then it will increment at a percentage.
        {
            eI.batteryChance /= 2;
            int per = eI.difficulty - 10;
            if(eI.difficulty <= 15)
            {
                SetEnemyStats(3000 + (850 * per), 12500, 150 + (10 * per));
            }
            else if (eI.difficulty > 15 && eI.difficulty <= 20)
            {
                SetEnemyStats(3000 + (1000 * per), 15000, 200 + (25 * per));
            }
            else
            {
                SetEnemyStats(3000 + (1250 * per), 22500, 250 + (35 * per));
            }
        }
    }
    void SetEnemyStats(int health, int score, int damage)
    {
        eI.health = health;
        //eI.speed = speed;
        eI.amountOfScore = score;
        eI.damageDealtToTowers = damage;
    }
}

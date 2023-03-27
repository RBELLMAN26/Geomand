using UnityEngine;

//Heart gives the enemy characters health back will exceed max health if not put out first
public class Heart : MonoBehaviour
{

    public EnemyInfo eI;
    public EnemyController eC;

    public float regenRate, maxRegenRate;
    public float reloadRate, maxReloadRate;
    public int amountToRegen;

    public GameObject regenWave;
    public float timeToDestroyWave;
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
        ChargeRegen();
    }

    public void ChargeRegen()
    {
        if (reloadRate <= 0)
        {
            if (regenRate > 0)
            {
                regenRate -= Time.deltaTime;
            }
            else
            {
                FireRegen();
                regenRate = maxRegenRate;
                reloadRate = maxReloadRate;
            }

        }
        else
        {
            reloadRate -= Time.deltaTime;
        }

    }

    public void FireRegen()
    {
        GameObject newHeart = (GameObject)Instantiate(regenWave, transform.position, transform.rotation);
        newHeart.GetComponent<HeartAuraController>().healthToGive = (eI.damageDealtToTowers + amountToRegen);
    }

    void CheckDifficulty()
    {
        eI.difficulty -= 6;
        if (eI.difficulty <= 10)
        {
            switch (eI.difficulty)
            {
                case 0:
                    {
                        SetEnemyStats(200, 225, 5, 5, 100);
                        break;
                    }
                case 1:
                    {
                        SetEnemyStats(250, 250, 5, 5, 100);
                        break;
                    }
                case 2:
                    {
                        SetEnemyStats(350, 400, 5, 5 , 150);
                        break;
                    }
                case 3:
                    {
                        SetEnemyStats(450, 625, 5, 5 , 200);
                        break;
                    }
                case 4:
                    {
                        SetEnemyStats(500, 800, 10, 4, 250);
                        break;
                    }
                case 5:
                    {
                        SetEnemyStats(750, 1250, 10, 4, 350);
                        break;
                    }
                case 6:
                    {
                        SetEnemyStats(800, 1500, 10, 4, 500);
                        break;
                    }
                case 7:
                    {
                        SetEnemyStats(1000, 2000, 20,  4, 650);
                        break;
                    }
                case 8:
                    {
                        SetEnemyStats(1500, 3000, 50, 3, 700);
                        break;
                    }
                case 9:
                    {
                        SetEnemyStats(2000, 4000, 100, 3, 800);
                        break;
                    }
                case 10:
                    {
                        SetEnemyStats(3000,  5000, 150, 3, 900);
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
                SetEnemyStats(4000 + (500 * per), 10000, 200 + (10 * per), 3, 1000 + (200 * per));
                eI.chanceOfShield = Random.Range(25, 50);
            }
            else if (eI.difficulty > 15 && eI.difficulty <= 20)
            {
                SetEnemyStats(4000 + (650 * per), 12500, 250 + (25 * per), 3, 1000 + (250 * per));
                eI.chanceOfShield = Random.Range(50, 75);
            }
            else
            {
                SetEnemyStats(4000 + (700 * per), 15000, 300 + (35 * per), 3, 1000 + (300 * per));
                eI.chanceOfShield = Random.Range(75, 100);
            }

        }

    }
    void SetEnemyStats(int health, int score, int damage, float rate, int amount)
    {
        eI.health = health;
        maxRegenRate = rate;
        amountToRegen = amount;
        eI.amountOfScore = score;
        eI.damageDealtToTowers = damage;
    }
}

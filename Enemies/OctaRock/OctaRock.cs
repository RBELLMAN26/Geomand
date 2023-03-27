using UnityEngine;
using System.Collections;
public class OctaRock : MonoBehaviour
{

    public EnemyInfo eI;
    public EnemyController eC;

    public Transform[] rocketLaunchers;
    public GameObject rocketObj;
    public TowerInfo[] targets;
    public int numOfFires = 2;
    public bool[] fired = new bool[8];
    public float fireRate = 3, maxFireRate = 3;
    public float reloadRate = 5, maxReloadRate = 5;
    public Transform healthObj;
    int dir;
    bool waiting;
    // Start is called before the first frame update
    void Start()
    {
        dir = Random.Range(0, 2);
        eI = GetComponent<EnemyInfo>();
        eC = GetComponent<EnemyController>();
        targets = FindObjectOfType<TowerManager>().turrets;
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
        RotateEnemy();
        if (reloadRate <= 0)
        {
            if(!eI.isFrozen)
            {
                fired = new bool[8];
                if (fireRate > 0)
                {
                    fireRate -= Time.deltaTime;
                }
                else
                {
                    if(!waiting)
                    {
                        waiting = true;
                        StartCoroutine(FireRockets());
                    }
                }
            }
        }
        else
        {
            reloadRate -= Time.deltaTime;
        }

    }
    void RotateEnemy()
    {
        if(!eI.isFrozen)
        {
            if (dir == 0)
            {
                healthObj.Rotate(new Vector3(0, 0, -30 * Time.deltaTime));
                transform.Rotate(new Vector3(0, 0, 30 * Time.deltaTime));
            }
            else
            {
                healthObj.Rotate(new Vector3(0, 0, 30 * Time.deltaTime));
                transform.Rotate(new Vector3(0, 0, -30 * Time.deltaTime));
            }
        }
    }
    IEnumerator FireRockets()
    {
        int totalFired = numOfFires;
        for (int i = 0; i < numOfFires; i++)
        {
            yield return new WaitForEndOfFrame();
            int randomWeapon = Random.Range(0, 8);
            if (numOfFires < 8)
            {
                if (!fired[randomWeapon])
                {
                    if (totalFired > 0)
                    {
                        totalFired -= 1;
                        LaunchRocket(randomWeapon);
                        fired[randomWeapon] = true;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    i = 0;
                }
            }
            else
            {
                LaunchRocket(i);
                fired[i] = true;
            }

        }
        fireRate = maxFireRate;
        reloadRate = maxReloadRate;
        waiting = false;
    }
    void LaunchRocket(int rocketNum)
    {
        GameObject newRocket = (GameObject)Instantiate(rocketObj, rocketLaunchers[rocketNum].position, rocketLaunchers[rocketNum].rotation);
        RocketController rC = newRocket.GetComponent<RocketController>();
        rC.targets = targets;
        int health = Mathf.RoundToInt(eI.health / 3);
        int score = Mathf.RoundToInt(eI.amountOfScore / 2);
        int damage = eI.damageDealtToTowers;
        rC.UpdateRocket(health, score, damage);

    }
    void CheckDifficulty()
    {
        eI.difficulty -= 11;
        if (eI.difficulty <= 10)
        {
            switch (eI.difficulty)
            {
                case 0:
                    {
                        SetEnemyStats(500, 1500, 40, 3);
                        break;
                    }
                case 1:
                    {
                        SetEnemyStats(600, 2000, 50, 3);
                        break;
                    }
                case 2:
                    {
                        SetEnemyStats(700, 2500, 55, 3);
                        break;
                    }
                case 3:
                    {
                        SetEnemyStats(800, 3000, 65, 3);
                        break;
                    }
                case 4:
                    {
                        SetEnemyStats(900, 3750, 85, 4);
                        break;
                    }
                case 5:
                    {
                        SetEnemyStats(1000, 4500, 105, 4);
                        break;
                    }
                case 6:
                    {
                        SetEnemyStats(1100, 5000, 110, 5);
                        break;
                    }
                case 7:
                    {
                        SetEnemyStats(1250, 6000, 125, 5);
                        break;
                    }
                case 8:
                    {
                        SetEnemyStats(2000, 7000, 135, 5);
                        break;
                    }
                case 9:
                    {
                        SetEnemyStats(2500, 7500, 145, 6);
                        break;
                    }
                case 10:
                    {
                        SetEnemyStats(3000, 8500, 150, 6);
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
                SetEnemyStats(4000 + (750 * per), 9500, 200 + (25 * per), 8);
                eI.chanceOfShield = Random.Range(25, 50);
            }
            else if (eI.difficulty > 15 && eI.difficulty <= 20)
            {
                SetEnemyStats(4000 + (850 * per), 10000, 250 + (35 * per), 8);
                eI.chanceOfShield = Random.Range(50, 75);
            }
            else
            {
                SetEnemyStats(4000 + (1000 * per), 12500, 300 + (40 * per), 8);
                eI.chanceOfShield = Random.Range(75, 100);
            }

        }

    }
    void SetEnemyStats(int health, int score, int damage, int rockets)
    {
        eI.health = health;
        eI.amountOfScore = score;
        eI.damageDealtToTowers = damage;
        numOfFires = rockets;
    }
}

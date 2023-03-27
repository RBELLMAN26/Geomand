using UnityEngine;

//Enemy That Rotates and Fire Lasers 
//Depending On Progression more lasers will open up.
public class Arma : MonoBehaviour
{
    //Manages The Controls And Information;
    public EnemyInfo eI;
    public EnemyController eC;

    //public GameObject laserObj; //Laser Bullet
    public Transform[] weapons; //There are 4 Directions the weapon will fire from
    public int numOfFires = 1;
    public bool[] fired = new bool[4];
    public float fireRate = 5, maxFireRate = 5;
    public float reloadRate = 2, maxReloadRate = 2;
    public float chargeRate = 2, maxChargeRate = 2;
    //public GameObject[] newLasers;
    public bool fireMain;
    public Transform healthObj;
    int dir;
    // Start is called before the first frame update
    void Start()
    {
        eI = GetComponent<EnemyInfo>();
        eC = GetComponent<EnemyController>();
        dir = Random.Range(0, 2);
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
        ChargeLaser();
        RotateEnemy();
    }
    void RotateEnemy()
    {
        if(!eI.isFrozen)
        {
            if (dir == 0)
            {
                healthObj.Rotate(new Vector3(0, 0, (-20 * (eI.speed * 5)) * Time.deltaTime));
                transform.Rotate(new Vector3(0, 0, (20 * (eI.speed * 5)) * Time.deltaTime));
            }
            else
            {
                healthObj.Rotate(new Vector3(0, 0, (20 * (eI.speed * 5)) * Time.deltaTime));
                transform.Rotate(new Vector3(0, 0, (-20 * (eI.speed * 5)) * Time.deltaTime));
            }
        }
    }

    void ChargeLaser()
    {

        if (reloadRate <= 0 && !eI.isFrozen)
        {
            fired = new bool[4];
            if (chargeRate > 0)
            {
                chargeRate -= Time.deltaTime;
            }
            else
            {
                if (fireRate > 0)
                {
                    if (!fireMain)
                    {
                        FireLaser();
                        fireMain = true;
                    }
                    fireRate -= Time.deltaTime;
                }
                else
                {
                    reloadRate = maxReloadRate;
                    chargeRate = maxChargeRate;
                    fireRate = maxFireRate;
                    fireMain = false;
                    foreach(Transform obj in weapons)
                    {
                        obj.gameObject.SetActive(false);
                    }
                    /*
                    for (int i = 0; i < newLasers.Length; i++)
                    {
                        Destroy(newLasers[i]);
                    }
                    */

                }

            }

        }
        else
        {
            reloadRate -= Time.deltaTime;
            fireMain = false;
            chargeRate = maxChargeRate;
            fireRate = maxFireRate;
            foreach (Transform obj in weapons)
            {
                obj.gameObject.SetActive(false);
            }
            /*
            for (int i = 0; i < newLasers.Length; i++)
            {
                Destroy(newLasers[i]);
            }
            */
        }

    }

    void FireLaser()
    {
        int totalFired = numOfFires;
        for (int i = 0; i < numOfFires; i++)
        {
            if (numOfFires < 4)
            {
                int range = Random.Range(0, 4);
                if (!fired[range])
                {
                    if (totalFired > 0)
                    {
                        totalFired -= 1;
                        weapons[range].gameObject.SetActive(true);
                        //newLasers[range] = (GameObject)Instantiate(laserObj, weapons[range].position, weapons[range].rotation);
                        //newLasers[range].transform.parent = weapons[range].transform;
                        ////GameObject newLaser = (GameObject)Instantiate(laserObj, weapons[3].position, weapons[3].rotation);
                        fired[range] = true;
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
                weapons[i].gameObject.SetActive(true);
                //newLasers[i] = (GameObject)Instantiate(laserObj, weapons[i].position, weapons[i].rotation);
                //newLasers[i].transform.parent = weapons[i].transform;
                ////GameObject newLaser = (GameObject)Instantiate(laserObj, weapons[3].position, weapons[3].rotation);
                fired[i] = true;
            }

        }
    }
    void CheckDifficulty()
    {
        eI.difficulty -= 9;
        if (eI.difficulty <= 10)
        {
            switch (eI.difficulty)
            {
                case 0:
                    {
                        SetEnemyStats(500, 750, 5, 2);
                        break;
                    }
                case 1:
                    {
                        SetEnemyStats(600,  850, 5, 2);
                        break;
                    }
                case 2:
                    {
                        SetEnemyStats(700,  1000, 10, 2);
                        break;
                    }
                case 3:
                    {
                        SetEnemyStats(800, 1250, 20, 2);
                        break;
                    }
                case 4:
                    {
                        SetEnemyStats(900, 1500, 30, 3);
                        break;
                    }
                case 5:
                    {
                        SetEnemyStats(1000,  2250, 40, 3);
                        break;
                    }
                case 6:
                    {
                        SetEnemyStats(1100, 2500, 50, 3);
                        break;
                    }
                case 7:
                    {
                        SetEnemyStats(1350,  3750, 60, 3);
                        break;
                    }
                case 8:
                    {
                        SetEnemyStats(2000,  5000, 70, 4);
                        break;
                    }
                case 9:
                    {
                        SetEnemyStats(3000,  6250, 80, 4);
                        break;
                    }
                case 10:
                    {
                        SetEnemyStats(3500, 7500, 90, 4);
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
                SetEnemyStats(4000 + (500 * per), 8500, 100 + (5 * per), 4);
                eI.chanceOfShield = Random.Range(25, 50);
            }
            else if (eI.difficulty > 15 && eI.difficulty <= 20)
            {
                SetEnemyStats(4000 + (750 * per), 9500, 100 + (5 * per), 4);
                eI.chanceOfShield = Random.Range(50, 75);
            }
            else
            {
                SetEnemyStats(4000 + (850 * per), 10000, 125 + (10 * per), 4);
                eI.chanceOfShield = Random.Range(75, 100);
            }
        }
    }
    void SetEnemyStats(int health, int score, int damage, int lasers)
    {
        eI.health = health;
        eI.amountOfScore = score;
        eI.damageDealtToTowers = damage;
        numOfFires = lasers;
    }
}

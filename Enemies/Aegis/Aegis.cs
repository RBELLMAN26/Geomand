using UnityEngine;

//Enemy 6 has multi functions,  Attacks,  Has Different Shields
public class Aegis : MonoBehaviour
{
    //Manages The Controls And Information;
    public EnemyInfo eI;
    public EnemyController eC;

    public GameObject laserObj; //Laser Bullet // Will Attack Turrets and Node Directly
    public Transform shieldObj;
    public AegisShield[] shields; //There are 4 for each corner
    public Transform aimer;
    [SerializeField] TowerInfo[] towers;
    [SerializeField] Transform[] targets = new Transform[5];
    //How long the weapon fires
    public float fireRate = 3, maxFireRate = 3;
    //How Long Takes To Take Till Firing
    public float chargeRate = 3, maxChargeRate = 3;
    //After laser has fired how long takes to reload
    public float reloadRate = 3, maxReloadRate = 3;
    // Start is called before the first frame update
    public Transform target;
    public Transform healthObj;
    int dir;
    void Start()
    {
        dir = Random.Range(0, 2);
        eI = GetComponent<EnemyInfo>();
        eC = GetComponent<EnemyController>();
        CheckDifficulty();
        UpdateShields();
        towers = FindObjectOfType<TowerManager>().turrets;
        for (int i = 0; i < towers.Length; i++)
        {
            targets[i] = towers[i].transform;
        }
        if(FindObjectOfType<NodeController>())
        {
            targets[4] = FindObjectOfType<NodeController>().transform;
        }
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
        ChargeLaser();
    }
    void RotateEnemy()
    {
        if(!eI.isFrozen)
        {
            if (dir == 0)
            {
                shieldObj.Rotate(new Vector3(0, 0, (-60 * (eI.speed * 5)) * Time.deltaTime));
                healthObj.Rotate(new Vector3(0, 0, (-30 * (eI.speed * 5)) * Time.deltaTime));
                transform.Rotate(new Vector3(0, 0, (30 * (eI.speed * 5)) * Time.deltaTime));
            }
            else
            {
                shieldObj.Rotate(new Vector3(0, 0, (60 * (eI.speed * 5)) * Time.deltaTime));
                healthObj.Rotate(new Vector3(0, 0, (30 * (eI.speed * 5)) * Time.deltaTime));
                transform.Rotate(new Vector3(0, 0, (-30 * (eI.speed * 5)) * Time.deltaTime));
            }
        }
    }
    void ChargeLaser()
    {
        if (reloadRate <= 0 && !eI.isFrozen)
        {
            if (chargeRate > 0)
            {
                chargeRate -= Time.deltaTime;
            }
            else
            {
                if (fireRate > 0)
                {
                    FireLaser();
                    fireRate -= Time.deltaTime;
                }
                else
                {
                    reloadRate = maxReloadRate;
                    chargeRate = maxChargeRate;
                    fireRate = maxFireRate;
                    aimer.gameObject.SetActive(false);
                    //Destroy(newLaser);
                }

            }

        }
        else
        {
            reloadRate -= Time.deltaTime;
            chargeRate = maxChargeRate;
            fireRate = maxFireRate;
            aimer.gameObject.SetActive(false);
            /*
            if (newLaser != null)
            {
                Destroy(newLaser);
            }
            */
        }

    }
    void FireLaser()
    {
        
        if (!aimer.gameObject.activeSelf)
        {
            int targetInt = Random.Range(0, 5);
            target = targets[targetInt];
        }

        if (targets.Length > 0)
        {
            if (target.gameObject.activeSelf)
            {
                LookatTarget(target.transform);
                if(!aimer.gameObject.activeSelf)
                {
                    laserObj.GetComponent<EnemyBulletStats>().damage = eI.damageDealtToTowers;
                    aimer.gameObject.SetActive(true);
                }
                /*
                float dist = Vector2.Distance(transform.position, target.transform.position);
                if (newLaser == null)
                {
                    
                    newLaser = (GameObject)Instantiate(laserObj, aimer.position, aimer.rotation);
                    newLaser.GetComponent<LaserController>().Distance = dist;
                    newLaser.transform.position = aimer.position;
                    newLaser.transform.parent = aimer.transform;
                    
                }
                else
                {
                    newLaser.GetComponent<LaserController>().Distance = dist;
                    newLaser.transform.position = aimer.position;
                    newLaser.transform.rotation = aimer.rotation;
                    newLaser.transform.parent = aimer.transform;
                }
                */

            }
            else
            {
                for (int i = 0; i < targets.Length + 1; i++)
                {
                    if(i == 5)
                    {
                        if(targets[4] != null)
                        {
                            if (targets[4].gameObject.activeSelf)
                            {

                                target = targets[4].transform;
                            }
                            else
                            {
                                aimer.gameObject.SetActive(false);
                            }
                        }   
                    }
                    else
                    {
                        if(targets[i] != null)
                        {
                            if (targets[i].gameObject.activeSelf)
                            {
                                //target = targets[i];
                                int targetInt = Random.Range(0, 5);
                                target = targets[targetInt];
                                break;
                            }
                            else
                            {
                                reloadRate = maxReloadRate;
                                chargeRate = maxChargeRate;
                                fireRate = maxFireRate;
                                aimer.gameObject.SetActive(false);
                            }
                        }
                    }
                }
                
            }
        }
    }
    void LookatTarget(Transform targetPosition)
    {

        // get direction you want to point at // there was a normalized part in here that I removed if anything goes wrong
        Vector2 direction = (((Vector2)targetPosition.position) - (Vector2)transform.position);
        // set vector of transform directly
        aimer.up = direction;

    }

    void UpdateShields()
    {
        for (int i = 0; i < 4; i++)
        {
            shields[i].CreateShield(Random.Range(0, 3));
        }
    }

    void CheckDifficulty()
    {
        eI.difficulty -= 7;
        if (eI.difficulty <= 10)
        {
            switch (eI.difficulty)
            {
                case 0:
                    {
                        SetEnemyStats(500, 500, 5);
                        break;
                    }
                case 1:
                    {
                        SetEnemyStats(600, 575, 5);
                        break;
                    }
                case 2:
                    {
                        SetEnemyStats(700, 650, 10);
                        break;
                    }
                case 3:
                    {
                        SetEnemyStats(800, 750, 20);
                        break;
                    }
                case 4:
                    {
                        SetEnemyStats(900, 800, 30);
                        break;
                    }
                case 5:
                    {
                        SetEnemyStats(1000, 1100, 40);
                        break;
                    }
                case 6:
                    {
                        SetEnemyStats(1100, 1250, 50);
                        break;
                    }
                case 7:
                    {
                        SetEnemyStats(1200, 2500, 60);
                        break;
                    }
                case 8:
                    {
                        SetEnemyStats(1300, 3750, 70);
                        break;
                    }
                case 9:
                    {
                        SetEnemyStats(1400, 4000, 80);
                        break;
                    }
                case 10:
                    {
                        SetEnemyStats(1500, 4500, 90);
                        break;
                    }
            }
        }
        else//if diffulty goes past 10 then it will increment at a percentage.
        {
            eI.batteryChance /= 2;
            int per = eI.difficulty - 10;
            if (eI.difficulty <= 15)
            {
                SetEnemyStats(1500 + (500 * per), 5000, 100 + (5 * per));
                eI.chanceOfShield = Random.Range(25, 50);
            }
            else if(eI.difficulty > 15 && eI.difficulty <= 20)
            {
                SetEnemyStats(1500 + (600 * per), 6000, 100 + (5 * per));
                eI.chanceOfShield = Random.Range(50, 75);
            }
            else
            {
                SetEnemyStats(1500 + (700 * per), 7000, 125 + (10 * per));
                eI.chanceOfShield = Random.Range(75, 100);
            }
            
            
        }

    }
    void SetEnemyStats(int health, int score, int damage)
    {
        eI.health = health;
        eI.amountOfScore = score;
        eI.damageDealtToTowers = damage;
    }
}

using UnityEngine;

//Purpose Of The Cresent Is to Be More Dodgy While Heading Down

// The Enemy Will Be Fire Two Lasers at the end of each point.
public class Cresent : MonoBehaviour
{
    public EnemyInfo eI;
    public EnemyController eC;
    public GameObject bullet;
    public Transform[] weapons;
    public float fireRate = 3, maxFireRate = 3;
    public float reloadRate = 3, maxReloadRate = 3;
    public Vector2 screenBounds;
    private float objectWidth;
    int direction;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        direction = Random.Range(0, 2);
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
        //print(eI.speed * Time.deltaTime);
        //eC.Movement(Vector2.down);
        Sway();
        ChargeWeapon();
    }
    void ChargeWeapon()
    {
        if (reloadRate <= 0 && !eI.isFrozen)
        {
            if (fireRate > 0)
            {
                fireRate -= Time.deltaTime;
            }
            else
            {
                FireWeapon();
            }

        }
        else
        {
            reloadRate -= Time.deltaTime;
        }

    }

    void Sway()
    {
        //Direction 0 is left,  direction 1 is right;
        if (direction == 0)
        {
            eC.Movement((Vector2.down / 4) + (Vector2.left * 2));
            Vector3 pos = transform.position;
            if (pos.x - objectWidth <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
            {
                direction = 1;
            }
        }
        else
        {
            eC.Movement((Vector2.down / 4) + (Vector2.right * 2));
            Vector3 pos = transform.position;
            if (pos.x + objectWidth >= screenBounds.x)
            {
                direction = 0;
            }
        }
    }
    void FireWeapon()
    {
        GameObject newBullet1 = (GameObject)Instantiate(bullet, weapons[0].position, weapons[0].rotation);
        GameObject newBullet2 = (GameObject)Instantiate(bullet, weapons[1].position, weapons[1].rotation);
        newBullet1.GetComponent<EnemyBulletStats>().damage = eI.damageDealtToTowers;
        newBullet2.GetComponent<EnemyBulletStats>().damage = eI.damageDealtToTowers;

        fireRate = maxFireRate;
        reloadRate = maxReloadRate;

    }
    void CheckDifficulty()
    {
        eI.difficulty -= 5;
        if (eI.difficulty <= 10)
        {
            switch (eI.difficulty)
            {
                case 0:
                    {
                        SetEnemyStats(500, 300, 5,3);
                        break;
                    }
                case 1:
                    {
                        SetEnemyStats(600, 350, 5,3);
                        break;
                    }
                case 2:
                    {
                        SetEnemyStats(700, 400, 5,3);
                        break;
                    }
                case 3:
                    {
                        SetEnemyStats(800, 550, 10,2.5f);
                        break;
                    }
                case 4:
                    {

                        SetEnemyStats(900, 800, 10, 2.5f);
                        break;
                    }
                case 5:
                    {
                        SetEnemyStats(1000, 1300, 15, 2.5f);
                        break;
                    }
                case 6:
                    {
                        SetEnemyStats(1100, 1550, 15, 2);
                        break;
                    }
                case 7:
                    {
                        SetEnemyStats(1200, 2000, 20, 2);
                        break;
                    }
                case 8:
                    {
                        SetEnemyStats(1350, 2500, 20, 2);
                        break;
                    }
                case 9:
                    {
                        SetEnemyStats(1500, 3000, 35, 2);
                        break;
                    }
                case 10:
                    {
                        SetEnemyStats(2000, 4000, 45, 2);
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
                SetEnemyStats(2500 + (350 * per), 5000, 50 + (5 * per), 2f);
                eI.chanceOfShield = Random.Range(25, 50);
            }
            else if (eI.difficulty > 15 && eI.difficulty <= 20)
            {
                SetEnemyStats(2500 + (500 * per), 7500, 65 + (10 * per), 1.5f);
                eI.chanceOfShield = Random.Range(50, 75);
            }
            else
            {
                SetEnemyStats(2500 + (650 * per), 10000, 75 + (10 * per), 1.5f);
                eI.chanceOfShield = Random.Range(75, 100);
            }
        }
    }

    void SetEnemyStats(int health, int score, int damage, float fireRate)
    {
        eI.health = health;
        eI.amountOfScore = score;
        eI.damageDealtToTowers = damage;
        maxFireRate = fireRate;
    }

}

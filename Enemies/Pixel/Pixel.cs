using UnityEngine;

public class Pixel : MonoBehaviour
{
    public EnemyInfo eI;
    public EnemyController eC;
    public Vector2 screenBounds;
    private float objectWidth;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
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
        eC.Movement(-transform.up);
    }
    void CheckDifficulty()
    {
        if (eI.difficulty <= 10)
        {
            switch (eI.difficulty)
            {
                case 0:
                    {
                        SetEnemyStats(5, 13, 5);
                        break;
                    }
                case 1:
                    {
                        SetEnemyStats(15,  25, 5);
                        break;
                    }
                case 2:
                    {
                        SetEnemyStats(25,  38, 5);
                        break;
                    }
                case 3:
                    {
                        SetEnemyStats(40,  50, 5);
                        break;
                    }
                case 4:
                    {
                        SetEnemyStats(75,  75, 10);
                        break;
                    }
                case 5:
                    {
                        SetEnemyStats(125,  250, 10);
                        break;
                    }
                case 6:
                    {
                        SetEnemyStats(150,  325, 10);
                        break;
                    }
                case 7:
                    {
                        SetEnemyStats(200,  400, 20);
                        break;
                    }
                case 8:
                    {
                        SetEnemyStats(350,  1000, 35);
                        break;
                    }
                case 9:
                    {
                        SetEnemyStats(600,  1125, 65);
                        break;
                    }
                case 10:
                    {
                        SetEnemyStats(750,  1250, 75);
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
                SetEnemyStats(800 + (350 * per), 2500, 150 + (10 * per));
                eI.chanceOfShield = Random.Range(25, 50);
            }
            else if (eI.difficulty > 15 && eI.difficulty <= 20)
            {
                SetEnemyStats(800 + (500 * per), 3750, 200 + (25 * per));
                eI.chanceOfShield = Random.Range(50, 75);
            }
            else
            {
                SetEnemyStats(800 + (600 * per), 5000, 250 + (35 * per));
                eI.chanceOfShield = Random.Range(75, 100);
            }
        }
        eI.maxHealth = eI.health;
    }

    void SetEnemyStats(int health, int score, int damage)
    {
        eI.health = health;
        eI.amountOfScore = score;
        eI.damageDealtToTowers = damage;
    }
}

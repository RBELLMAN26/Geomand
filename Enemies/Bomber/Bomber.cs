using UnityEngine;

public class Bomber : MonoBehaviour
{
    public EnemyInfo eI;
    public EnemyController eC;
    public Transform shield;
    public Transform deathWave;
    public float disableTime = 5;
    public int block;
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
        //SetShield();
    }

    // Update is called once per frame
    void Update()
    {
        //eC.Movement(-transform.up);
        eC.Movement(Vector2.down);
    }
    public void OnDeath()
    {
        GameObject newWave = (GameObject)Instantiate(deathWave.gameObject, transform.position, transform.rotation);
        newWave.GetComponent<DeathWave>().damageToTowers = Mathf.RoundToInt(eI.damageDealtToTowers / 2);
        newWave.GetComponent<DeathWave>().disableTime = disableTime;
        Destroy(this.gameObject);
    }
    void CheckDifficulty()
    {
        eI.difficulty -= 4;
        if (eI.difficulty <= 10)
        {
            switch (eI.difficulty)
            {
                case 0:
                    {
                        SetEnemyStats(200, 250, 5, 5);
                        break;
                    }
                case 1:
                    {
                        SetEnemyStats(300, 375, 5, 5);
                        break;
                    }
                case 2:
                    {
                        SetEnemyStats(400, 500, 15, 5);
                        break;
                    }
                case 3:
                    {
                        SetEnemyStats(450, 675, 25, 5);
                        break;
                    }
                case 4:
                    {
                        SetEnemyStats(600, 750, 35, 10);
                        break;
                    }
                case 5:
                    {
                        SetEnemyStats(650, 1000, 50, 10);
                        break;
                    }
                case 6:
                    {
                        SetEnemyStats(750, 1500, 75, 10);
                        break;
                    }
                case 7:
                    {
                        SetEnemyStats(800, 1750, 85, 15);
                        break;
                    }
                case 8:
                    {
                        SetEnemyStats(850, 2000, 95, 15);
                        break;
                    }
                case 9:
                    {
                        SetEnemyStats(1250, 2250, 100, 15);
                        break;
                    }
                case 10:
                    {
                        SetEnemyStats(1500, 2500, 150, 15);
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
                SetEnemyStats(1750 + (250 * per), 5000, 250 + (10 * per), 10);
                eI.chanceOfShield = Random.Range(25, 50);
            }
            else if (eI.difficulty > 15 && eI.difficulty <= 20)
            {
                SetEnemyStats(1750 + (350 * per), 6250, 300 + (25 * per), 10);
                eI.chanceOfShield = Random.Range(50, 75);
            }
            else
            {
                SetEnemyStats(1750 + (450 * per), 7250, 350 + (35 * per), 12);
                eI.chanceOfShield = Random.Range(75, 100);
            }
        }
        eI.maxHealth = eI.health;
    }
    void SetEnemyStats(int health, int score, int damage, float disable)
    {
        eI.health = health;
        disableTime = disable;
        eI.amountOfScore = score;
        eI.damageDealtToTowers = damage;
    }
}

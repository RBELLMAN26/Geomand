using UnityEngine;

public class EndlessEnemyGenerator : MonoBehaviour
{
    public GameManager gM;
    public int difficulty = 0;
    public int currentEnemySpawners = 3;
    //public Transform Enemy;
    public Transform[] Enemies;
    public Transform battery;
    public GameObject nodeObj;
    public float rateToSpawnEnemy = 5, timeToSpawnEnemy = 5;
    //public float rateToSpawnBoss = 2, timeToSpawnBoss = 2;
    public float batterySpawnTime = 30;

    public Vector2 randomSpawn;
    public float sizeOfArea;
    public int scoreIncrease;
    public int scoreBoost;
    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDifficulty();
        if (timeToSpawnEnemy > 0)
        {
            timeToSpawnEnemy -= Time.deltaTime;
        }
        else
        {
            SpawnEnemy();
            timeToSpawnEnemy = rateToSpawnEnemy;
        }

        if (nodeObj.activeSelf)
        {
            if (batterySpawnTime > 0)
            {
                batterySpawnTime -= Time.deltaTime;
            }
            else
            {
                SpawnBattery();
                batterySpawnTime = Random.Range(15.0f, 80.0f);
            }
        }

        /*
        if(timeToSpawnBoss > 0)
        {
            timeToSpawnBoss -= Time.deltaTime;
        }
        else
        {
            SpawnBoss();
            timeToSpawnBoss = rateToSpawnBoss;
        }
        */
    }
    void SpawnEnemy()
    {
        int RandEnemy = Random.Range(0, currentEnemySpawners);
        randomSpawn = new Vector2(Random.Range(-sizeOfArea, sizeOfArea), transform.position.y);
        Transform NewEnemy = Instantiate(Enemies[RandEnemy], randomSpawn, transform.rotation);
        NewEnemy.GetComponent<EnemyInfo>().difficulty = difficulty;
        if (scoreIncrease != 0)
        {
            NewEnemy.GetComponent<EnemyInfo>().amountOfScore *= scoreIncrease;
        }

    }
    void SpawnBoss()
    {

    }
    void SpawnBattery()
    {
        Vector2 batterySpawn = new Vector2(Random.Range(-sizeOfArea, sizeOfArea), transform.position.y);
        Transform newBattery = Instantiate(battery, batterySpawn, transform.rotation);
    }
    void CheckDifficulty()
    {
        //This calculates every difficulty will be added at 1 minute
        if (gM.timeInMatch > difficulty * 60 + 60)
        {
            difficulty += 1;
            gM.difficulty = difficulty;
            gM.costToRevive += 250 * (difficulty);
            switch (difficulty)
            {
                case 1:
                    {
                        currentEnemySpawners += 1;
                        break;
                    }
                case 2:
                    {
                        rateToSpawnEnemy -= 1;
                        break;
                    }
                case 3:
                    {
                        currentEnemySpawners += 1;
                        break;
                    }
                case 4:
                    {
                        rateToSpawnEnemy -= 0.5f;
                        break;
                    }
                case 5:
                    {
                        currentEnemySpawners += 1;
                        break;
                    }
                case 6:
                    {
                        rateToSpawnEnemy -= 0.5f;
                        break;
                    }
                case 7:
                    {
                        currentEnemySpawners += 1;
                        break;
                    }
                case 8:
                    {
                        currentEnemySpawners += 1;
                        break;
                    }
                case 9:
                    {
                        currentEnemySpawners += 1;
                        break;
                    }
                case 10:
                    {
                        currentEnemySpawners += 1;
                        break;
                    }
            }
        }
    }

}

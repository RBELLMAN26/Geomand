using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    EnemyGenerationManager eGM;
    public Transform enemyToSpawn;
    public int minChanceToSpawn;
    public int maxChanceToSpawn;
    public int chanceToMultiSpawn;
    public int maxMultiSpawns;
    public int scoreBoosts;
    public float spawnRate, spawnTimer;
    Vector3 screenBounds;
    float widthOfSprite;
    public bool isFrozen;
    NodeController nodeObj;
    // Start is called before the first frame update
    void Start()
    {
        nodeObj = FindObjectOfType<NodeController>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        widthOfSprite = enemyToSpawn.GetComponent<SpriteRenderer>().size.x;
        eGM = GetComponentInParent<EnemyGenerationManager>();
        //GetComponent<BoxCollider2D>().size = new Vector2( Screen.width / 2, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFrozen)
        {
            if (eGM.gM.fps > 30)
            {
                if (minChanceToSpawn != 0 && maxChanceToSpawn != 0)
                {
                    if (spawnTimer > 0)
                    {
                        spawnTimer -= Time.deltaTime;
                    }
                    else
                    {
                        int randValue = Random.Range(1, 100);
                        int randChance = Random.Range(minChanceToSpawn, maxChanceToSpawn);
                        if (randValue <= randChance)
                        {
                            SpawnEnemy();
                            spawnTimer = spawnRate;
                        }
                        else
                        {
                            spawnTimer = spawnRate;
                        }
                    }
                }
            }
        }  
    }

    void SpawnEnemy()
    {
        int randMultiChance = Random.Range(0, 100);
        int multiEnemies = 0;
        if (randMultiChance <= chanceToMultiSpawn && chanceToMultiSpawn != 0)
        {
            multiEnemies = Random.Range(1, maxMultiSpawns);
        }

        float leftSide = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float rightSide = screenBounds.x;

        if (multiEnemies > 0)
        {
            for (int i = 0; i < multiEnemies; i++)
            {
                Vector2 randomSpawn = new Vector2(Random.Range(leftSide, rightSide), transform.position.y);
                Transform newEnemy;
                if (randomSpawn.x > 0)
                {
                    randomSpawn = new Vector2(randomSpawn.x - widthOfSprite, randomSpawn.y);
                    newEnemy = Instantiate(enemyToSpawn, randomSpawn, transform.rotation);
                }
                else
                {
                    randomSpawn = new Vector2(randomSpawn.x + widthOfSprite, randomSpawn.y);
                    newEnemy = Instantiate(enemyToSpawn, randomSpawn, transform.rotation);
                }
                newEnemy.GetComponent<EnemyInfo>().difficulty = eGM.difficulty;
                newEnemy.GetComponent<EnemyInfo>().amountOfScore += scoreBoosts;
                if (!nodeObj.gameObject.activeSelf)
                {
                    newEnemy.GetComponent<EnemyInfo>().batteryChance = 0;
                }
            }
        }
        else
        {
            Vector2 randomSpawn = new Vector2(Random.Range(leftSide, rightSide), transform.position.y);
            Transform newEnemy;
            if (randomSpawn.x > 0)
            {
                randomSpawn = new Vector2(randomSpawn.x - widthOfSprite, randomSpawn.y);
                newEnemy = Instantiate(enemyToSpawn, randomSpawn, transform.rotation);
            }
            else
            {
                randomSpawn = new Vector2(randomSpawn.x + widthOfSprite, randomSpawn.y);
                newEnemy = Instantiate(enemyToSpawn, randomSpawn, transform.rotation);
            }

            newEnemy.GetComponent<EnemyInfo>().difficulty = eGM.difficulty;
            newEnemy.GetComponent<EnemyInfo>().amountOfScore += scoreBoosts;
            if (!nodeObj.gameObject.activeSelf)
            {
                newEnemy.GetComponent<EnemyInfo>().batteryChance = 0;
            }
        }

    }

}

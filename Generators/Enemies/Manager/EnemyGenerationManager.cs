using UnityEngine;

public class EnemyGenerationManager : MonoBehaviour
{
    public GameManager gM;
    public int difficulty;
    [SerializeField] private EnemyGenerator[] generators;
    public Transform battery;
    public GameObject nodeObj;
    [SerializeField]
    Vector3 screenBounds;
    public float pauseDuration;

    int scoreMultiplier;
    [SerializeField]
    float multiplierDuration;
    [SerializeField] float batterySpawnTime = 30;
    bool scoreReset = true;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        gM = GameObject.FindObjectOfType<GameManager>();
        //generators = GetComponentsInChildren<EnemyGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckDifficulty())
        {
            difficulty = gM.difficulty;
            UpdateGenerators();
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
                batterySpawnTime = Random.Range(15.0f, 120.0f);
            }
        }
        if(pauseDuration > 0)
        {
            pauseDuration -= Time.deltaTime;
        }
        else
        {
            UnPauseEnemies();
        }
        if(multiplierDuration > 0)
        {
            multiplierDuration -= Time.deltaTime;
        }
        else if(!scoreReset)
        {
            ScoreResetMultiplier();
        }
    }

    public void PauseEnemies(float duration)
    {
        pauseDuration = duration;
        for(int i = 0; i < generators.Length; i++)
        {
            generators[i].isFrozen = true;
        }
        foreach(EnemyInfo obj in FindObjectsOfType<EnemyInfo>())
        {
            obj.isFrozen = true;
        }
        foreach(RocketStats obj in FindObjectsOfType<RocketStats>())
        {
            obj.isFrozen = true;
        }
    }
    void UnPauseEnemies()
    {
        for (int i = 0; i < generators.Length; i++)
        {
            generators[i].isFrozen = false;
        }
        foreach (EnemyInfo obj in FindObjectsOfType<EnemyInfo>())
        {
            obj.isFrozen = false;
        }
        foreach (RocketStats obj in FindObjectsOfType<RocketStats>())
        {
            obj.isFrozen = false;
        }
    }
    bool CheckDifficulty()
    {
        if (gM.difficulty != difficulty)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void SpawnBattery()
    {
        float leftSide = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + battery.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float rightSide = screenBounds.x - battery.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        Vector2 batterySpawn = new Vector2(Random.Range(leftSide, rightSide), transform.position.y);
        Transform newBattery = Instantiate(battery, batterySpawn, transform.rotation);
    }
    void UpdateGenerators()
    {
        //int Gen, int minSpawnChance, int maxSpawnChance, int multiSpawnChance, int maxMultiSpawns, float spawnRate
        switch (difficulty)
        {
            case 0:
                {
                    SetGenerator(0, 75, 100, 15, 2, 1); //Pixel
                    SetGenerator(1, 25, 50, 15, 2, 2f); //Disabler
                    SetGenerator(2, 25, 50, 10, 2, 2.5f); //Splitter
                    break;
                }
            case 1:
                {
                    SetGenerator(0, 75, 100, 15, 2, 1); //Pixel
                    SetGenerator(1, 25, 50, 15, 2, 2f); //Disabler
                    SetGenerator(2, 25, 50, 10, 2, 2.5f); //Splitter
                    break;
                }
            case 2:
                {
                    SetGenerator(0, 75, 100, 15, 3, 1); //Pixel
                    SetGenerator(1, 25, 50, 15, 2, 2f); //Disabler
                    SetGenerator(2, 25, 50, 10, 2, 2.5f); //Splitter
                    break;
                }
            case 3:
                {
                    SetGenerator(0, 75, 100, 15, 3, 1); //Pixel
                    SetGenerator(1, 25, 50, 15, 2, 2f); //Disabler
                    SetGenerator(2, 25, 50, 10, 2, 2.5f); //Splitter
                    SetGenerator(3, 5, 10, 15, 1, 3); //Blocker
                    break;
                }
            case 4:
                {
                    SetGenerator(0, 60, 80, 15, 3, 1); //Pixel
                    SetGenerator(1, 25, 50, 15, 2, 2f); //Disabler
                    SetGenerator(2, 25, 50, 15, 2, 2.5f); //Splitter
                    SetGenerator(3, 10, 15, 15, 1, 3); //Blocker
                    SetGenerator(4, 5, 10, 15, 1, 3); //Bomber
                    batterySpawnTime = 60;
                    break;
                }
            case 5:
                {
                    SetGenerator(0, 60, 80, 15, 3, 1); //Pixel
                    SetGenerator(1, 25, 50, 15, 2, 2f); //Disabler
                    SetGenerator(2, 25, 50, 15, 2, 2.5f); //Splitter
                    SetGenerator(3, 10, 25, 15, 2, 3); //Blocker
                    SetGenerator(4, 10, 15, 15, 2, 3); //Bomber
                    SetGenerator(5, 5, 10, 15, 1, 3); //Cresent
                    break;
                }
            case 6:
                {
                    SetGenerator(0, 60, 80, 15, 3, 1); //Pixel
                    SetGenerator(1, 25, 50, 15, 2, 2f); //Disabler
                    SetGenerator(2, 25, 50, 15, 2, 2.5f); //Splitter
                    SetGenerator(3, 10, 25, 15, 2, 3); //Blocker
                    SetGenerator(4, 15, 20, 15, 2, 3); //Bomber
                    SetGenerator(5, 5, 15, 15, 2, 3); //Cresent
                    SetGenerator(6, 5, 10, 15, 1, 3); //Heart
                    break;
                }
            case 7:
                {
                    SetGenerator(0, 60, 80, 15, 3, 1); //Pixel
                    SetGenerator(1, 25, 50, 15, 2, 2f); //Disabler
                    SetGenerator(2, 25, 50, 15, 2, 2.5f); //Splitter
                    SetGenerator(3, 10, 25, 15, 2, 3); //Blocker
                    SetGenerator(4, 15, 20, 15, 2, 3); //Bomber
                    SetGenerator(5, 10, 15, 15, 2, 3); //Cresent
                    SetGenerator(6, 10, 15, 15, 2, 3); //Heart
                    SetGenerator(7, 5, 10, 15, 1, 3); //Aegis
                    batterySpawnTime = 90;
                    break;
                }
            case 8:
                {
                    SetGenerator(0, 60, 80, 15, 3, 1); //Pixel
                    SetGenerator(1, 25, 50, 15, 2, 2f); //Disabler
                    SetGenerator(2, 25, 50, 15, 2, 2.5f); //Splitter
                    SetGenerator(3, 10, 25, 15, 2, 3); //Blocker
                    SetGenerator(4, 10, 20, 10, 2, 3); //Bomber
                    SetGenerator(5, 10, 20, 10, 2, 3); //Cresent
                    SetGenerator(6, 20, 25, 25, 2, 3); //Heart
                    SetGenerator(7, 10, 20, 10, 1, 3); //Aegis
                    SetGenerator(8, 5, 10, 15, 1, 3); //Starify
                    break;
                }
            case 9:
                {
                    SetGenerator(0, 60, 80, 15, 3, 1); //Pixel
                    SetGenerator(1, 25, 50, 15, 2, 2f); //Disabler
                    SetGenerator(2, 25, 50, 15, 2, 2.5f); //Splitter
                    SetGenerator(3, 15, 30, 15, 2, 3); //Blocker
                    SetGenerator(4, 10, 20, 10, 2, 3); //Bomber
                    SetGenerator(5, 10, 20, 10, 2, 3); //Cresent
                    SetGenerator(6, 20, 25, 25, 2, 3); //Heart
                    SetGenerator(7, 10, 15, 10, 1, 3); //Aegis
                    SetGenerator(8, 10, 15, 15, 2, 3); //Starify
                    SetGenerator(9, 5, 10, 15, 1, 3); //Arma
                    batterySpawnTime = 100;
                    break;
                }
            case 10:
                {
                    SetGenerator(0, 60, 80, 15, 4, 1); //Pixel
                    SetGenerator(1, 25, 50, 15, 2, 2f); //Disabler
                    SetGenerator(2, 25, 50, 15, 2, 2.5f); //Splitter
                    SetGenerator(3, 15, 35, 15, 2, 3); //Blocker
                    SetGenerator(4, 10, 20, 10, 2, 3); //Bomber
                    SetGenerator(5, 10, 20, 10, 2, 3); //Cresent
                    SetGenerator(6, 20, 25, 25, 2, 3); //Heart
                    SetGenerator(7, 10, 15, 10, 1, 3); //Aegis
                    SetGenerator(8, 10, 15, 15, 2, 3); //Starify
                    SetGenerator(9, 10, 15, 15, 1, 3); //Arma
                    SetGenerator(10, 5, 10, 15, 1, 3); //Defect
                    break;
                }
            case 11:
                {
                    SetGenerator(0, 60, 80, 15, 4, 1); //Pixel
                    SetGenerator(1, 25, 50, 15, 2, 2f); //Disabler
                    SetGenerator(2, 25, 50, 15, 2, 2.5f); //Splitter
                    SetGenerator(3, 15, 40, 15, 2, 3); //Blocker
                    SetGenerator(4, 10, 20, 10, 2, 3); //Bomber
                    SetGenerator(5, 10, 20, 10, 2, 3); //Cresent
                    SetGenerator(6, 20, 25, 25, 2, 3); //Heart
                    SetGenerator(7, 10, 15, 10, 1, 3); //Aegis
                    SetGenerator(8, 10, 15, 15, 2, 3); //Starify
                    SetGenerator(9, 10, 15, 15, 1, 3); //Arma
                    SetGenerator(10, 10, 20, 15, 2, 3); //Defect
                    SetGenerator(11, 10, 15, 15, 1, 3); //OctaRock

                    break;
                }
            case 12:
                {
                    SetGenerator(0, 60, 80, 15, 5, 1); //Pixel
                    SetGenerator(1, 30, 60, 15, 3, 2f); //Disabler
                    SetGenerator(2, 30, 60, 15, 3, 2.5f); //Splitter
                    SetGenerator(3, 15, 40, 15, 2, 3); //Blocker
                    SetGenerator(4, 10, 25, 10, 2, 3); //Bomber
                    SetGenerator(5, 10, 25, 10, 2, 3); //Cresent
                    SetGenerator(6, 25, 35, 25, 2, 3); //Heart
                    SetGenerator(7, 10, 15, 10, 1, 3); //Aegis
                    SetGenerator(8, 10, 25, 15, 2, 3); //Starify
                    SetGenerator(9, 10, 15, 15, 1, 4); //Arma
                    SetGenerator(10, 15, 30, 20, 2, 3); //Defect
                    SetGenerator(11, 15, 20, 15, 1, 5); //OctaRock
                    SetGenerator(12, 5, 15, 10, 2, 5); //Sun
                    batterySpawnTime = 120;
                    break;
                }
        }
        if(difficulty > 13 && difficulty < 25)
        {
                SetGenerator(0, 40, 80, 15, 5, 1); //Pixel
                SetGenerator(1, 30, 60, 15, 3, 2f); //Disabler
                SetGenerator(2, 30, 60, 15, 3, 2f); //Splitter
                SetGenerator(3, 20, 40, 15, 2, 3); //Blocker
                SetGenerator(4, 10, 30, 10, 2, 3); //Bomber
                SetGenerator(5, 10, 30, 10, 2, 3); //Cresent
                SetGenerator(6, 20, 30, 25, 2, 3); //Heart
                SetGenerator(7, 10, 20, 10, 1, 4); //Aegis
                SetGenerator(8, 10, 30, 15, 2, 3); //Starify
                SetGenerator(9, 5, 25, 15, 1, 4); //Arma
                SetGenerator(10, 10, 30, 20, 2, 3); //Defect
                SetGenerator(11, 10, 30, 15, 1, 5); //OctaRock
                SetGenerator(12, 5, 10, 10, 2, 5); //Sun
        }
        else if(difficulty > 25)
        {
            SetGenerator(0, 50, 80, 20, 5, 1); //Pixel
            SetGenerator(1, 30, 60, 15, 3, 2f); //Disabler
            SetGenerator(2, 30, 60, 15, 3, 2f); //Splitter
            SetGenerator(3, 20, 60, 15, 2, 3); //Blocker
            SetGenerator(4, 10, 40, 10, 2, 3); //Bomber
            SetGenerator(5, 10, 40, 10, 2, 3); //Cresent
            SetGenerator(6, 20, 40, 25, 2, 3); //Heart
            SetGenerator(7, 10, 30, 10, 1, 4); //Aegis
            SetGenerator(8, 10, 40, 15, 2, 3); //Starify
            SetGenerator(9, 10, 30, 10, 2, 5); //Arma
            SetGenerator(10, 10, 40, 20, 2, 3); //Defect
            SetGenerator(11, 10, 30, 1, 1, 5); //OctaRock
            SetGenerator(12, 5, 30, 10, 2, 5); //Sun
        }
    }
    void SetGenerator(int Gen, int minSpawnChance, int maxSpawnChance, int multiSpawnChance, int maxMultiSpawns, float spawnRate)
    {
        generators[Gen].minChanceToSpawn = minSpawnChance;
        generators[Gen].maxChanceToSpawn = maxSpawnChance;
        generators[Gen].maxMultiSpawns = maxMultiSpawns;
        generators[Gen].chanceToMultiSpawn = multiSpawnChance;
        generators[Gen].spawnRate = spawnRate;
    }
    public void ScoreBoost(int scoreToBoosts)
    {
        for (int i = 0; i < generators.Length; i++)
        {
            generators[i].scoreBoosts += scoreToBoosts;
        }
    }
    public void ScoreMultiplier(int multiplier, float duration)
    {
        scoreMultiplier = multiplier;
        multiplierDuration = duration;
        scoreReset = false;
        for (int i = 0; i < generators.Length; i++)
        {
            generators[i].scoreBoosts *= multiplier;
        }
        foreach (EnemyInfo obj in FindObjectsOfType<EnemyInfo>())
        {
            obj.amountOfScore *= 2;
        }
    }
    void ScoreResetMultiplier()
    {
        scoreReset = true;
        for (int i = 0; i < generators.Length; i++)
        {
            generators[i].scoreBoosts /= scoreMultiplier;
        }
        foreach (EnemyInfo obj in FindObjectsOfType<EnemyInfo>())
        {
            obj.amountOfScore /= 2;
        }
    }
}

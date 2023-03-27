using UnityEngine;

public class BlockMaker : MonoBehaviour
{
    public EnemyController eC;
    public EnemyInfo eI;
    public GameObject miniBlock;
    public GameObject previousBlock;
    public GameObject nearestBlock;
    public bool isBlock;
    //0 Left 1 Down 2 Right
    public Vector3 direction;
    int dirValue;
    //public int currentDirection = 0; // 0 is Down 1 is left 2 is right.
    public int healthToBlock;
    public float timeToChangeDir;
    public float timeToCreateBlock, blockrate;
    public Vector2 screenBounds;
    private float objectWidth, objectHeight;
    public int eventPassed = 0;
    public bool firstDirection = false;
    public bool lastDirection = false;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        eC = GetComponent<EnemyController>();
        eI = GetComponent<EnemyInfo>();
        direction = -transform.up;
        CheckDifficulty();
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        timeToChangeDir = Random.Range(0.0f, 3.0f);
        timeToCreateBlock = Random.Range(0.0f, blockrate);
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
        //eC.Movement(direction);
        eC.Movement(direction);
        //BorderHit();

        /*
        RaycastHit2D[] cols = Physics2D.RaycastAll(transform.position, direction, 0.1f);
        for (int i = 0; i < cols.Length; i++)
        {
            if((transform.position.x - objectWidth) <= Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).x)
            {
                print("To Far Left On Block");
                //RandomizeDirection(4);
            }
            else if((transform.position.x) + objectWidth >= screenBounds.x)
            {
                print("To Far Right On Block");
                //RandomizeDirection(4);
            }
            /*
            if (cols[i].transform.CompareTag("Boundaries"))
            {
                RandomizeDirection(4);
            }
            
        }
        */

        if ((transform.position.y - objectHeight) <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y)
        {
            direction = Vector3.down;
        }
        else
        {
            if (timeToCreateBlock > 0)
            {
                timeToCreateBlock -= Time.deltaTime;
            }
            else
            {
                if (CheckArea())
                {
                    CreateMiniBlock();
                    timeToCreateBlock = Random.Range(1.0f, blockrate);
                }
            }
            if (timeToChangeDir > 0)
            {
                timeToChangeDir -= Time.deltaTime;
            }
            else
            {
                if (!lastDirection)
                {
                    ChooseDirection();
                }
                else
                {
                    if (direction != Vector3.down)
                    {
                        direction = Vector3.down;
                    }
                    else
                    {
                        //ChooseDirection();
                        if (BorderHit() == 0)
                        {
                            direction = Vector3.right;
                            timeToChangeDir = Random.Range(1.0f, 3.0f);

                        }
                        if (BorderHit() == 1)
                        {
                            direction = Vector3.left;
                            timeToChangeDir = Random.Range(1.0f, 3.0f);
                        }
                    }

                }
                //RandomizeDirection();

                //timeToChangeDir = Random.Range(1.0f, 3.0f);
            }

        }




    }

    void ChooseDirection()
    {
        int dir = Random.Range(0, 2);

        if (!firstDirection)
        {
            switch (dir)
            {
                //Left
                case 0:
                    {
                        direction = Vector3.left;
                        firstDirection = true;
                        break;
                    }
                //Right
                case 1:
                    {
                        direction = Vector3.right;
                        firstDirection = true;
                        break;
                    }
            }
            firstDirection = true;
        }
        else
        {
            if (direction != Vector3.down)
            {
                if (BorderHit() == 0 || BorderHit() == 1)
                {
                    direction = Vector3.down;
                    timeToChangeDir = Random.Range(1.0f, 5.0f);
                }

            }
            else
            {
                if (BorderHit() == 0)
                {
                    direction = Vector3.right;
                    dirValue = 0;

                    return;
                }
                else if (BorderHit() == 1)
                {
                    direction = Vector3.left;
                    dirValue = 1;
                    return;
                }
                else if (BorderHit() == 2)
                {
                    if (dir == 0)
                    {
                        direction = Vector3.right;
                    }
                    else
                    {
                        direction = Vector3.left;
                    }
                }
            }

        }
        dirValue = dir;
    }

    int BorderHit()
    {
        if ((transform.position.x - objectWidth) <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
        {
            // direction = Vector3.down;
            //print("To Far Left On Block");

            return 0;
            //hitBorder = true;
            //return true;
            //RandomizeDirection(4);
        }
        else if ((transform.position.x) + objectWidth >= screenBounds.x)
        {
            //direction = Vector3.down;
            //print("To Far Right On Block");
            //timeToChangeDir = Random.Range(1.0f, 5.0f);
            //hitBorder = true;
            return 1;
            //RandomizeDirection(4);
        }
        return 2;
    }
    void RandomizeDirection(int currentDirection = 4)
    {
        if (currentDirection == 4)
        {
            currentDirection = Random.Range(0, 3);
        }

        switch (currentDirection)
        {
            case 0: //Down
                {
                    /*
                    RaycastHit2D[] cols = Physics2D.RaycastAll(transform.position, -transform.up, 0.1f);
                    for (int i = 0; i < cols.Length; i++)
                    {
                        if(cols[i].transform.CompareTag("Boundaries"))
                        {
                            print("Detected Wall");
                            break;
                        }
                    }
                    */
                    direction = -transform.up;

                    break;
                }
            case 1: //Right
                {
                    /*
                    RaycastHit2D[] cols = Physics2D.RaycastAll(transform.position, transform.right, 0.1f);
                    for (int i = 0; i < cols.Length; i++)
                    {
                        if (cols[i].transform.CompareTag("Boundaries"))
                        {
                            print("Detected Wall");
                            break;
                        }
                    }
                    */
                    if (transform.position.x - objectWidth <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
                    {
                        int randDir = Random.Range(0, 2);
                        if (randDir == 0)
                        {
                            direction = -transform.up;
                        }
                        else
                        {
                            direction = -transform.right;
                        }
                        break;
                    }
                    else if (transform.position.x + objectWidth >= screenBounds.x)
                    {
                        int randDir = Random.Range(0, 2);
                        if (randDir == 0)
                        {
                            direction = -transform.up;
                        }
                        else
                        {
                            direction = -transform.right;
                        }
                        break;
                    }
                    direction = transform.right;

                    break;
                }
            case 2: //Left
                {
                    /*
                    RaycastHit2D[] cols = Physics2D.RaycastAll(transform.position, -transform.right, 0.1f);
                    for (int i = 0; i < cols.Length; i++)
                    {
                        if (cols[i].transform.CompareTag("Boundaries"))
                        {
                            print("Detected Wall");
                            break;
                        }
                    }
                    */
                    if (transform.position.x - objectWidth <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
                    {
                        int randDir = Random.Range(0, 2);
                        if (randDir == 0)
                        {
                            direction = -transform.up;
                        }
                        else
                        {
                            direction = transform.right;
                        }
                        break;
                    }
                    else if (transform.position.x + objectWidth >= screenBounds.x)
                    {
                        int randDir = Random.Range(0, 2);
                        if (randDir == 0)
                        {
                            direction = -transform.up;
                        }
                        else
                        {
                            direction = transform.right;
                        }
                        break;
                    }
                    direction = -transform.right;
                    break;
                }
        }
    }
    bool CheckDistance(GameObject obj, float distance)
    {
        float vectorXDist = obj.transform.position.x - transform.position.x;
        float vectorYDist = obj.transform.position.y - transform.position.y;
        //print("X" + vectorXDist + "Y" + vectorYDist);
        if ((Mathf.Abs(vectorXDist) >= distance && Mathf.Abs(vectorYDist) >= distance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool CheckArea()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position, new Vector2(.5f, .5f), 0);
        foreach (Collider2D obj in cols)
        {
            if (obj.CompareTag("Obstacle"))
            {
                //print("Found Block");
                return false;
            }
        }
        //print("Not Found Block");
        return true;

    }
    void CreateMiniBlock()
    {
        healthToBlock = Mathf.RoundToInt(eI.maxHealth * 2);
        previousBlock = (GameObject)Instantiate(miniBlock, transform.position, transform.rotation);
        previousBlock.GetComponent<EnemyInfo>().health = healthToBlock;
        previousBlock.GetComponent<EnemyInfo>().amountOfScore = Mathf.RoundToInt(eI.amountOfScore / 2);
        //print("Prev" + Vector2.Distance(previousBlock.transform.position, transform.position).ToString());
        // if (nearestBlock != null)
        //print("Near" + Vector2.Distance(nearestBlock.transform.position, transform.position).ToString());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BlockerEvent"))
        {
            if (eventPassed == 0)
            {
                eventPassed += 1;
            }
            else
            {
                lastDirection = true;
            }
        }
        if (collision.CompareTag("Obstacle"))
        {
            nearestBlock = collision.gameObject;
            isBlock = true;
        }
    }
    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (nearestBlock == collision.transform)
        {
            isBlock = false;
        }
        if (collision.CompareTag("Obstacle"))
        {
            isBlock = false;
        }
    }
    */
    void CheckDifficulty()
    {
        eI.difficulty -= 3;
        if (eI.difficulty <= 10)
        {
            switch (eI.difficulty)
            {
                case 0:
                    {
                        SetEnemyStats(150, 400, 5, 10);
                        break;
                    }
                case 1:
                    {
                        SetEnemyStats(200, 650, 5, 10);
                        break;
                    }
                case 2:
                    {
                        SetEnemyStats(250, 700, 10, 8);
                        break;
                    }
                case 3:
                    {
                        SetEnemyStats(300, 800, 10, 8);
                        break;
                    }
                case 4:
                    {
                        SetEnemyStats(400, 850, 10, 6);
                        break;
                    }
                case 5:
                    {
                        SetEnemyStats(450, 1000, 30, 6);
                        break;
                    }
                case 6:
                    {
                        SetEnemyStats(550, 1150, 30, 6);
                        break;
                    }
                case 7:
                    {
                        SetEnemyStats(650, 1250, 30, 5);
                        break;
                    }
                case 8:
                    {
                        SetEnemyStats(700, 2000, 65, 5);
                        break;
                    }
                case 9:
                    {
                        SetEnemyStats(750, 3000, 75, 5);
                        break;
                    }
                case 10:
                    {
                        SetEnemyStats(1250, 4000, 100, 4);
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
                SetEnemyStats(1500 + (350 * per), 7500, 200 + (10 * per), 3);
                eI.chanceOfShield = Random.Range(25, 50);
            }
            else if (eI.difficulty > 15 && eI.difficulty <= 20)
            {
                SetEnemyStats(1500 + (550 * per), 10000, 250 + (25 * per), 3);
                eI.chanceOfShield = Random.Range(50, 75);
            }
            else
            {
                SetEnemyStats(1500 + (700 * per), 15000, 300 + (30 * per), 3);
                eI.chanceOfShield = Random.Range(75, 100);
            }

        }
        eI.maxHealth = eI.health;
    }
    void SetEnemyStats(int health, int score, int damage, float rate)
    {
        eI.health = health;
        blockrate = rate;
        eI.amountOfScore = score;
        eI.damageDealtToTowers = damage;
    }
}

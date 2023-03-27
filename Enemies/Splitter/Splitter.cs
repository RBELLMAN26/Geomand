using UnityEngine;

public class Splitter : MonoBehaviour
{
    public EnemyInfo eI;
    public EnemyController eC;
    public GameObject miniSplits;
    public int numOfTris = 2;
    public Vector3[] pos;
    public float dodgeRate = 3;
    Vector3 dodgePos;
    bool isDodge;
    public Vector2 screenBounds;
    private float objectWidth;
    bool isSplit;
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
        if(!eI.isFrozen)
        {
            // eC.Movement(-transform.up);
            if (!isDodge)
            {
                if (dodgeRate > 0)
                {
                    eC.Movement(-transform.up);
                    dodgeRate -= Time.deltaTime;
                }
                else
                {
                    dodgePos = Dodge();
                    isDodge = true;
                }

            }
            else
            {
                if (Vector2.Distance(transform.position, dodgePos) > 0.1f)
                {
                    transform.position = Vector3.Slerp(transform.position, dodgePos, Time.deltaTime * 10);
                }
                else
                {
                    isDodge = false;
                    dodgeRate = 3;
                }

            }
        }
        

    }
    Vector3 Dodge()
    {
        int dir = Random.Range(0, 8);
        switch (dir)
        {
            //Up
            case 0:
                {
                    return transform.position + Vector3.up;
                }
            //Down
            case 1:
                {
                    return transform.position + Vector3.down;
                }
            //Right
            case 2:
                {
                    Vector3 pos = transform.position + Vector3.right;
                    //if (pos.x - objectWidth <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
                    if (pos.x + objectWidth >= screenBounds.x)
                    {
                        return transform.position + Vector3.left;
                    }
                    else
                    {
                        return transform.position + Vector3.right;
                    }
                }
            //Left
            case 3:
                {
                    Vector3 pos = transform.position + Vector3.left;
                    if (pos.x - objectWidth <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
                    {
                        return transform.position + Vector3.right;
                    }
                    else
                    {
                        return transform.position + Vector3.left;
                    }

                }
            //UpLeft
            case 4:
                {
                    Vector3 pos = transform.position + (Vector3.up + Vector3.left);
                    if (pos.x - objectWidth <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
                    {
                        return transform.position + (Vector3.up + Vector3.right);
                    }
                    else
                    {
                        return transform.position + (Vector3.up + Vector3.left);
                    }

                }
            //UpRight
            case 5:
                {
                    Vector3 pos = transform.position + (Vector3.up + Vector3.right);
                    //if (pos.x - objectWidth <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
                    if (pos.x + objectWidth >= screenBounds.x)
                    {
                        return transform.position + (Vector3.up + Vector3.left);
                    }
                    else
                    {
                        return transform.position + (Vector3.up + Vector3.right);
                    }

                }
            //DownLeft
            case 6:
                {
                    Vector3 pos = transform.position + (Vector3.up + Vector3.left);
                    if (pos.x - objectWidth <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
                    {
                        return transform.position + (Vector3.down + Vector3.right);
                    }
                    else
                    {
                        return transform.position + (Vector3.down + Vector3.left);
                    }
                }
            //DownRight
            case 7:
                {
                    Vector3 pos = transform.position + (Vector3.up + Vector3.right);
                    //if (pos.x - objectWidth <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
                    if (pos.x + objectWidth >= screenBounds.x)
                    {
                        return transform.position + (Vector3.down + Vector3.left);
                    }
                    else
                    {
                        return transform.position + (Vector3.down + Vector3.right);
                    }
                }

        }
        return Vector3.zero;
    }
    public void SplitEnemy()
    {
        //print("Split Enemy");
        if (!isSplit)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject newTri = (GameObject)Instantiate(miniSplits, transform.position + pos[i], transform.rotation);
                EnemyInfo tri = newTri.GetComponent<EnemyInfo>();
                tri.amountOfScore = Mathf.RoundToInt(eI.amountOfScore / 2);
                tri.health = (int)Mathf.Round(eI.maxHealth / 2);
                tri.damageDealtToTowers = (int)Mathf.Round(eI.damageDealtToTowers / 2);
                tri.isFrozen = eI.isFrozen;
            }
            isSplit = true;
        }

        Destroy(this.gameObject);
    }
    void CheckDifficulty()
    {
        if (eI.difficulty <= 10)
        {
            switch (eI.difficulty)
            {
                case 0:
                    {
                        SetEnemyStats(15, 50, 15);
                        break;
                    }
                case 1:
                    {
                        SetEnemyStats(20, 125, 15);
                        break;
                    }
                case 2:
                    {
                        SetEnemyStats(25,  150, 25);
                        break;
                    }
                case 3:
                    {
                        SetEnemyStats(75,  200, 45);
                        break;
                    }
                case 4:
                    {
                        SetEnemyStats(200,  250, 55);
                        break;
                    }
                case 5:
                    {
                        SetEnemyStats(400,  375, 65);
                        break;
                    }
                case 6:
                    {
                        SetEnemyStats(750,  425, 75);
                        break;
                    }
                case 7:
                    {
                        SetEnemyStats(800,  500, 85);
                        break;
                    }
                case 8:
                    {
                        SetEnemyStats(1000,  750, 100);
                        break;
                    }
                case 9:
                    {
                        SetEnemyStats(2000,  1000, 125);
                        break;
                    }
                case 10:
                    {
                        SetEnemyStats(2500,  1500, 200);
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
                SetEnemyStats(3000 + (250 * per), 2500, 250 + (10 * per));
                eI.chanceOfShield = Random.Range(25, 50);
            }
            else if (eI.difficulty > 15 && eI.difficulty <= 20)
            {
                SetEnemyStats(3000 + (350 * per), 3750, 300 + (20 * per));
                eI.chanceOfShield = Random.Range(50, 75);
            }
            else
            {
                SetEnemyStats(3000 + (500 * per), 5000, 350 + (30 * per));
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

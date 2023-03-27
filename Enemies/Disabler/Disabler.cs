using UnityEngine;

public class Disabler : MonoBehaviour
{
    public EnemyInfo eI;
    public Transform target;
    public EnemyController eC;
    public Transform nodeTarget;
    public TowerController[] tC;
    public float disableTime = 3.0f;
    bool lockedOn;
    // Start is called before the first frame update
    void Start()
    {
        eI = GetComponent<EnemyInfo>();
        eC = GetComponent<EnemyController>();
        tC = FindObjectsOfType<TowerController>();
        NodeInfo tempNode = FindObjectOfType<NodeInfo>();
        if (tempNode != null)
        {
            nodeTarget = tempNode.GetComponent<Transform>();
        }

        CheckDifficulty();
        SelectTarget();
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
        if (target != null)
        {
            if (target.gameObject.activeSelf)
            {
                LookatTarget();
            }
        }
    }
    void LookatTarget()
    {
        Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //print(rotation);
        this.transform.eulerAngles = new Vector3(0, 0, rotation + 90);
    }
    void SelectTarget()
    {
        int select = Random.Range(0, 1);
        int num = Random.Range(0, tC.Length);
        //print(num.ToString());
        if (select == 0)
        {
            if (tC.Length > 0)
            {
                target = tC[num].GetComponent<Transform>();
            }
            lockedOn = true;
        }
        else
        {
            target = nodeTarget;
            lockedOn = true;
        }

    }
    void CheckDifficulty()
    {
        if (eI.difficulty <= 10)
        {
            switch (eI.difficulty)
            {
                case 0:
                    {
                        SetEnemyStats(10, 25, 5, 1);
                        break;
                    }
                case 1:
                    {
                        SetEnemyStats(20, 75, 5, 1);
                        break;
                    }
                case 2:
                    {
                        SetEnemyStats(25, 88, 10, 2);
                        break;
                    }
                case 3:
                    {
                        SetEnemyStats(100, 125, 15, 3);
                        break;
                    }
                case 4:
                    {
                        SetEnemyStats(200, 175, 20, 4);
                        break;
                    }
                case 5:
                    {
                        SetEnemyStats(275, 225, 25, 5);
                        break;
                    }
                case 6:
                    {
                        SetEnemyStats(350, 300, 30, 6);
                        break;
                    }
                case 7:
                    {
                        SetEnemyStats(400, 500, 35, 7);
                        break;
                    }
                case 8:
                    {
                        SetEnemyStats(550, 1000, 50, 8);
                        break;
                    }
                case 9:
                    {
                        SetEnemyStats(750, 1500, 75, 9);
                        break;
                    }
                case 10:
                    {
                        SetEnemyStats(1000, 2000, 100, 10);
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
                SetEnemyStats(1250 + (150 * per), 2500, 150 + (10 * per), 10);
                eI.chanceOfShield = Random.Range(25, 50);
            }
            else if (eI.difficulty > 15 && eI.difficulty <= 20)
            {
                SetEnemyStats(1250 + (250 * per), 3750, 200 + (25 * per), 10);
                eI.chanceOfShield = Random.Range(50, 75);
            }
            else
            {
                SetEnemyStats(1250 + (300 * per), 6500, 250 + (35 * per), 12);
                eI.chanceOfShield = Random.Range(75, 100);
            }

        }

    }
    void SetEnemyStats(int health, int score, int damage, float disableTime)
    {
        eI.health = health;
        //eI.speed = speed;
        eI.amountOfScore = score;
        eI.damageDealtToTowers = damage;
    }
}

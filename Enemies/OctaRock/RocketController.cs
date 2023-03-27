using UnityEngine;
using System.Collections;
public class RocketController : MonoBehaviour
{
    RocketStats rS;
    public float countToAim = 1;
    public TowerInfo[] targets;
    public Transform nodeTarget;
    public Transform[] baseHits;
    //public bool[] isActive = new bool[4];
    public Transform target;
    bool lockedOn;
    public float slowPercentage;
    public GameObject KillScore;
    bool waiting;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 0f, 1f, 1f, 1f);
        rS = GetComponent<RocketStats>();
        NodeInfo nodeTemp = FindObjectOfType<NodeInfo>();
        if (nodeTemp != null)
        {
            nodeTarget = nodeTemp.GetComponent<Transform>();
        }
        baseHits = FindObjectOfType<BaseInfo>().hits;
    }

    // Update is called once per frame
    void Update()
    {
        if (countToAim > 0)
        {
            countToAim -= Time.deltaTime;
        }
        else
        {
            if (lockedOn)
            {
                if (target != null)
                {
                    if (target.gameObject.activeSelf)
                    {
                        RotateTowardTarget();
                        /*
                        if(!waiting)
                        {
                            waiting = true;
                            RotateTowardTarget();
                        }
                        */

                    }
                    else
                    {
                        //waiting = false;
                        //StopCoroutine(RotateTowardTarget());
                        FindTarget();
                    }
                }
                else
                {
                    //waiting = false;
                    //StopCoroutine(RotateTowardTarget());
                    FindTarget();
                }


            }
            else
            {
                /*
                for (int i = 0; i < targets.Length; i++)
                {
                    isActive[i] = targets[i].isActive;
                }
                */
                FindTarget();
            }

        }
        if(!rS.isFrozen)
        {
            transform.position += (transform.up * Time.deltaTime * rS.speed);
        }
    }
    void RotateTowardTarget()
    {
        Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.transform.eulerAngles = new Vector3(0, 0, rotation + 270);
        //yield return new WaitForEndOfFrame();
    }

    void FindTarget()
    {
        int tar = Random.Range(0, targets.Length + 2);
        if (tar < targets.Length)
        {
            if(targets[tar].gameObject.activeSelf)
            {
                target = targets[tar].transform;
                lockedOn = true;
            }
            /*
            if (isActive[tar])
            {
                target = targets[tar].GetComponent<Transform>();
                lockedOn = true;
            }
            */
        }
        else if (tar == 4 && nodeTarget != null)
        {
            if(nodeTarget.gameObject.activeSelf)
            {
                target = nodeTarget;
                lockedOn = true;
            } 
        }
        else
        {
            int randHits = Random.Range(0, baseHits.Length);
            target = baseHits[randHits];
            lockedOn = true;
        }

    }
    public void UpdateRocket(int health, int score, int damageDealt)
    {
        if(rS == null)
        {
            rS = GetComponent<RocketStats>();
        }
        rS.health = health;
        rS.amountOfScore = score;
        rS.damageDealtToTowers = damageDealt;
    }
    void SlowDown(float time, float percentage)
    {
        slowPercentage = percentage;
        rS.timeSlow = time;
    }
    public void OnDeath()
    {
        //GameObject death = (GameObject)Instantiate(eI.deathParticles, transform.position, transform.rotation);
        ParticleSystem.MainModule death = rS.deathParticles.GetComponent<ParticleSystem>().main;
        //ParticleSystem.MainModule psMain = death.GetComponent<ParticleSystem>().main;
        //psMain.startColor = new ParticleSystem.MinMaxGradient(Random.ColorHSV(0f, 1f, 0f, 1f, 1f, 1f), GetComponent<SpriteRenderer>().color);
        death.startColor = new ParticleSystem.MinMaxGradient(Random.ColorHSV(0f, 1f, 0f, 1f, 1f, 1f), GetComponent<SpriteRenderer>().color);
        rS.deathParticles.gameObject.SetActive(true);
        rS.deathParticles.transform.parent = null;
        rS.deathParticles.GetComponent<ParticleSystem>().Play();
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            BulletStat bS = collision.GetComponent<BulletStat>();
            int newHealth = rS.health - bS.damage;
            if (bS.currentColor != 1)
            {
                collision.GetComponent<BulletController>().ApplyDeath();
            }
            else if (bS.currentColor == 1)
            {
                SlowDown(bS.slowTime, bS.slowPercentage);
            }

            if (newHealth > 0)
            {
                rS.health -= bS.damage;
            }
            else
            {
                GameObject scorePost = (GameObject)Instantiate(KillScore, transform.position, KillScore.transform.rotation);
                scorePost.GetComponentInChildren<EnemyScoreText>().killScore = rS.amountOfScore;
                GameObject.FindObjectOfType<GameManager>().UpdateScore(rS.amountOfScore);
                OnDeath();
            }
        }
        else if (collision.CompareTag("Turret"))
        {
            collision.GetComponent<TowerController>().LoseHealth(rS.damageDealtToTowers);
            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("Base"))
        {
            collision.GetComponent<BaseInfo>().LoseHealth();
            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("WaveWipe"))
        {
            rS.health -= collision.GetComponent<WaveWipe>().damage;
        }
        else if (collision.CompareTag("HeartAura"))
        {
            rS.health += collision.GetComponent<HeartAuraController>().healthToGive;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("WaveWipe"))
        {
            if (rS.health > 1)
            {
                rS.health -= collision.GetComponent<WaveWipe>().damage;
            }
            else
            {
                GameObject.FindObjectOfType<GameManager>().UpdateScore(rS.amountOfScore);
                OnDeath();
                //Destroy(this.gameObject);
            }
        }
    }
}

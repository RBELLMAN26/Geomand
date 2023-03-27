using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyInfo eI;
    public float slowPercentage; // What To Divide Speed By
    public GameObject KillScore;
    //For Laser Hits
    WaveWipe wW;
    float constHit = 0.0f, maxConstHit = 0.50f;
    //For Color Changing
    //[SerializeField] float hitTime = 0.25f, maxHitTime = 0.25f;
    //[SerializeField] bool hit = false, colorSwap = false;
    //Color color;
    SpriteRenderer sR;
    // Start is called before the first frame update
    private void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        sR.color = Random.ColorHSV(0f, 1f, 0f, 1f, 1f, 1f);
        //color = sR.color;
        eI = GetComponent<EnemyInfo>();       
    }
    public void Movement(Vector3 dir)
    {
        if(eI != null)
        {
            if (!eI.isFrozen)
            {
                if (eI.timeSlow)
                {
                    transform.position += (dir * Time.deltaTime * (eI.speed - ((eI.speed / slowPercentage) + (eI.speed / 2))));
                }
                else
                {
                    transform.position += (dir * Time.deltaTime * eI.speed);
                }
            }
        }
        
        if (transform.position.y <= -20)
        {
            Destroy(this.gameObject);
        }
    }
    void SlowDown(float time, float percentage)
    {
        slowPercentage = percentage;
        eI.timeSlow = true;
        StartCoroutine(Slow(time));
    }
    IEnumerator Slow(float time)
    {
        yield return new WaitForSeconds(time);
        eI.timeSlow = false;
    }
    public void ActivateShield(float time, bool byChance)
    {
        if(eI.shield != null)
        {
            //Hit by Shield Aura
            if (!byChance)
            {
                eI.shieldTime = time;
                eI.speed = eI.shieldspeed;
                eI.shield.SetActive(true);
                StartCoroutine(ShieldTime(time));

            }
            else
            {
                eI.shieldChance = true;
                eI.speed = eI.shieldspeed;
                eI.shield.SetActive(true);
                StartCoroutine(ShieldTime(time));
            }
        }
        
    }
    IEnumerator ShieldTime(float time)
    {
        yield return new WaitForSeconds(time);
        eI.speed = eI.savedSpeed;
        eI.shield.SetActive(false);
    }
    void CheckForBattery()
    {
        int bat = Random.Range(1, 100);
        if (bat <= eI.batteryChance && eI.battery != null && GameObject.Find("Node").activeSelf)
        {
            Instantiate(eI.battery, transform.position, transform.rotation);
        }
    }
    void HitColor()
    {
        sR.color = Random.ColorHSV(0f, 1f, 0f, 1f, 1f, 1f);
        /*
        if(hitTime > 0)
        {
            hitTime -= Time.deltaTime;
            if (!colorSwap)
            {
                sR.color = Random.ColorHSV(0f, 1f, 0f, 1f, 1f, 1f);
                colorSwap = true;
            }
        }
        else
        {
            hit = false;
            colorSwap = false;
            hitTime = maxHitTime;
            sR.color = color;
        }
        */


    }
    void IsDeath(int value)
    {
        if (value > 0)
        {
            eI.health = value;
            HitColor();
        }
        else
        {
            OnDeath();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            BulletStat bS = collision.GetComponent<BulletStat>();
            switch (bS.currentColor)
            {
                case 0:
                    {
                        BulletController bC = collision.GetComponent<BulletController>();
                        bC.ApplyDeath();
                        int newHealth = eI.health - bS.damage;
                        IsDeath(newHealth);
                        break;
                    }
                case 1:
                    {
                        if (constHit > 0)
                        {
                            constHit -= Time.deltaTime;
                        }
                        else
                        {
                            SlowDown(bS.slowTime, bS.slowPercentage);
                            int newHealth = eI.health - bS.damage;
                            constHit = maxConstHit;
                            IsDeath(newHealth);
                        }
                        
                        break;
                    }
                case 2:
                    {
                        BulletController bC = collision.GetComponent<BulletController>();
                        bC.ApplyDeath();
                        int newHealth = eI.health - bS.damage;
                        IsDeath(newHealth);
                        break;
                    }
            }
            
        }
        if (collision.CompareTag("ImpulseWave"))
        {
            ImpulseWave iW = collision.GetComponent<ImpulseWave>();
            SlowDown(iW.slowTime, iW.slowPercentage);
        }
        if (collision.CompareTag("ExplosiveWave"))
        {
            ExplosiveWave eW = collision.GetComponent<ExplosiveWave>();
            int newHealth = eI.health - eW.damage;
            IsDeath(newHealth);
        }
        if (collision.CompareTag("Base"))
        {
            collision.GetComponent<BaseInfo>().LoseHealth();
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("WaveWipe"))
        {
            wW = collision.GetComponent<WaveWipe>();
            //int newHealth = eI.health - collision.GetComponent<WaveWipe>().damage;
            int newHealth = eI.health - wW.damage;
            IsDeath(newHealth);
        }
        if (collision.CompareTag("HeartAura"))
        {
            if(!GetComponent<Heart>())
            {
                eI.health += collision.GetComponent<HeartAuraController>().healthToGive;
            }
        }
        if (collision.CompareTag("ShieldAura"))
        {
            ActivateShield(collision.GetComponent<ShieldWave>().amountOfShield, false);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(Time.timeScale > 0)
        {
            //Freezer Laser
            if (collision.CompareTag("Bullet"))
            {
                BulletStat bS = collision.GetComponent<BulletStat>();
                if (bS.currentColor == 1)
                {
                    if (constHit > 0)
                    {
                        constHit -= Time.deltaTime;
                    }
                    else
                    {
                        int newHealth = eI.health - collision.GetComponent<BulletStat>().damage;
                        IsDeath(newHealth);
                        constHit = maxConstHit;
                        SlowDown(bS.slowTime, bS.slowPercentage);
                    }
                }
            }
            if (collision.CompareTag("WaveWipe"))
            {
                int newHealth = eI.health - wW.damage;
                IsDeath(newHealth);
            }
        }
    }

    void OnDeath()
    {
        //GameObject death = (GameObject)Instantiate(eI.deathParticles, transform.position, transform.rotation);
        ParticleSystem.MainModule death = eI.deathParticles.GetComponent<ParticleSystem>().main;
        //ParticleSystem.MainModule psMain = death.GetComponent<ParticleSystem>().main;
        //psMain.startColor = new ParticleSystem.MinMaxGradient(Random.ColorHSV(0f, 1f, 0f, 1f, 1f, 1f), GetComponent<SpriteRenderer>().color);
        death.startColor = new ParticleSystem.MinMaxGradient(Random.ColorHSV(0f, 1f, 0f, 1f, 1f, 1f), GetComponent<SpriteRenderer>().color);
        eI.deathParticles.gameObject.SetActive(true);
        eI.deathParticles.transform.parent = null;
        eI.deathParticles.GetComponent<ParticleSystem>().Play();
        if (GetComponent<Splitter>())
        {
            GameObject.FindObjectOfType<GameManager>().UpdateScore(eI.amountOfScore);
            GetComponent<Splitter>().SplitEnemy();
            CheckForBattery();
            GameObject scorePost = (GameObject)Instantiate(KillScore, transform.position, KillScore.transform.rotation);
            scorePost.GetComponentInChildren<EnemyScoreText>().killScore = eI.amountOfScore;
            //Destroy(this.gameObject);
        }
        else if (GetComponent<Bomber>())
        {
            GameObject.FindObjectOfType<GameManager>().UpdateScore(eI.amountOfScore);
            GetComponent<Bomber>().OnDeath();
            CheckForBattery();
            GameObject scorePost = (GameObject)Instantiate(KillScore, transform.position, KillScore.transform.rotation);
            scorePost.GetComponentInChildren<EnemyScoreText>().killScore = eI.amountOfScore;
        }
        else if(GetComponent<Sun>())
        {
            GameObject.FindObjectOfType<GameManager>().UpdateScore(eI.amountOfScore);
            GetComponent<Sun>().OnDeath();
            CheckForBattery();
            GameObject scorePost = (GameObject)Instantiate(KillScore, transform.position, KillScore.transform.rotation);
            scorePost.GetComponentInChildren<EnemyScoreText>().killScore = eI.amountOfScore;
        }
        else
        {
            GameObject.FindObjectOfType<GameManager>().UpdateScore(eI.amountOfScore);
            CheckForBattery();
            GameObject scorePost = (GameObject)Instantiate(KillScore, transform.position, KillScore.transform.rotation);
            scorePost.GetComponentInChildren<EnemyScoreText>().killScore = eI.amountOfScore;
            Destroy(this.gameObject);

        }
    }
}

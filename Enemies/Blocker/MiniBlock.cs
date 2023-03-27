using UnityEngine;
using System.Collections;
public class MiniBlock : MonoBehaviour
{
    public EnemyInfo eI;
    public GameObject KillScore;
    //For Laser Hits
    float constHit = 1.0f, maxConstHit = 1.0f;
    //For Color Changing
    //[SerializeField] float hitTime = 0.25f, maxHitTime = 0.25f;
    //[SerializeField] bool hit = false, colorSwap = false;
    //Color color;
    SpriteRenderer sR;

    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        sR.color = Random.ColorHSV(0f, 1f, 0f, 1f, 1f, 1f);
        //color = sR.color;
        eI = GetComponent<EnemyInfo>();
        if (eI.chanceOfShield > 0)
        {
            int shieldChance = Random.Range(0, 100);
            if (shieldChance <= eI.chanceOfShield)
            {
                ActivateShield(15, true);
            }
        }
    }
    void ActivateShield(float time, bool byChance)
    {
        if (eI.shield != null)
        {
            //Hit by Shield Aura
            if (!byChance)
            {
                StartCoroutine(ShieldTime(time));
                eI.shieldTime = time;
                eI.shield.SetActive(true);

            }
            else
            {
                StartCoroutine(ShieldTime(time));
                eI.shieldChance = true;
                eI.shield.SetActive(true);
            }
        }

    }
    IEnumerator ShieldTime(float time)
    {
        yield return new WaitForSeconds(time);
        eI.shieldTime = 0;
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            BulletStat bS = collision.GetComponent<BulletStat>();
            FreezeLaserController fLC = collision.GetComponent<FreezeLaserController>();
            ImpulseWave iW = collision.GetComponent<ImpulseWave>();
            BulletController bC = collision.GetComponent<BulletController>();
            ExplosiveWave eW = collision.GetComponent<ExplosiveWave>();
            //int newHealth = eI.health - collision.GetComponent<BulletStat>().damage;
            int newHealth = eI.health - bS.damage;
            if (bS.currentColor != 2 && iW == null)
            {
                if (fLC == null)
                {
                    bC.ApplyDeath();
                }
            }
            else if (bS.currentColor == 2 && eW == null)
            {
                bC.ApplyDeath();
            }
            if (newHealth > 0)
            {
                eI.health = newHealth;
                HitColor();
            }
            else
            {

                OnDeath();

            }
        }
        if (collision.CompareTag("Base"))
        {
            collision.GetComponent<BaseInfo>().LoseHealth();
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("WaveWipe"))
        {
            //eI.health -= collision.GetComponent<WaveWipe>().damage;
            int newHealth = eI.health - collision.GetComponent<WaveWipe>().damage;
            if (newHealth > 0)
            {
                eI.health = newHealth;
                HitColor();
            }
            else
            {
                OnDeath();
            }
        }
        if (collision.GetComponent<HeartAuraController>())
        {
            eI.health += collision.GetComponent<HeartAuraController>().healthToGive;
        }
        if (collision.GetComponent<ShieldWave>())
        {
            //eI.health += collision.GetComponent<ShieldWave>().healthToGive;
            ActivateShield(collision.GetComponent<ShieldWave>().amountOfShield, false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<BulletStat>())
        {
            if (collision.GetComponent<BulletStat>().currentColor == 1 && !collision.GetComponent<ImpulseWave>())
            {
                if (constHit > 0)
                {
                    constHit -= Time.deltaTime;
                }
                else
                {
                    if (eI.health - collision.GetComponent<BulletStat>().damage > 0)
                    {
                        eI.health = eI.health - collision.GetComponent<BulletStat>().damage;
                        HitColor();
                        constHit = maxConstHit;
                    }
                    else
                    {
                        Destroy(this.gameObject);
                    }
                }
                BulletStat bS = collision.GetComponent<BulletStat>();
            }
        }

        if (collision.CompareTag("WaveWipe"))
        {
            int newHealth = eI.health - collision.GetComponent<WaveWipe>().damage;
            if (newHealth > 0)
            {
                eI.health = newHealth;
                HitColor();
            }
            else
            {
                OnDeath();
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
        GameObject.FindObjectOfType<GameManager>().UpdateScore(eI.amountOfScore);
        CheckForBattery();
        GameObject scorePost = (GameObject)Instantiate(KillScore, transform.position, KillScore.transform.rotation);
        scorePost.GetComponentInChildren<EnemyScoreText>().killScore = eI.amountOfScore;
        Destroy(this.gameObject);
    }

}

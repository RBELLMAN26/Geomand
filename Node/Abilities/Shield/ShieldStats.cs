using UnityEngine;

public class ShieldStats : MonoBehaviour
{
    public bool startCount;
    [SerializeField] bool Upgraded;
    public float duration = 10;
    public int shieldStrength = 100;
    public Transform parentNode;
    float delayedAttack = 0.2f, maxDelayedAttack = 0.2f; 
    // Update is called once per frame
    void Update()
    {
        if(startCount)
        {
            if (duration > 0)
            {
                duration -= Time.deltaTime;
            }
            else
            {
                //Destroy(this.gameObject);
                gameObject.SetActive(false);
                transform.parent = parentNode;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyBullet"))
        {
            EnemyBlocked(collision.GetComponent<EnemyBulletStats>().damage);
            Destroy(collision.gameObject);
        }
        if(collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<RocketStats>())
            {
                EnemyBlocked(collision.GetComponent<RocketStats>().damageDealtToTowers);
                Destroy(collision.gameObject);
            }
            else if (collision.GetComponent<DeathWave>())
            {
                EnemyBlocked(collision.GetComponent<DeathWave>().damageToTowers);
            }
            else if (collision.GetComponent<Sun>())
            {
                if (Upgraded)
                {
                    GetComponentInParent<ShieldManager>().CheckCount();
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                Destroy(collision.gameObject);
            }
            else if(collision.GetComponent<Bomber>())
            {
                collision.GetComponent<Bomber>().OnDeath();
            }
            else
            {
                EnemyBlocked(collision.GetComponent<EnemyInfo>().damageDealtToTowers);
                Destroy(collision.gameObject);
            }
            
        }
        if (delayedAttack <= 0)
        {
            if (collision.CompareTag("EnemyLaser"))
            {
                EnemyBlocked(collision.GetComponent<EnemyBulletStats>().damage);
            }
            delayedAttack = maxDelayedAttack;
        }
        else
        {
            delayedAttack -= Time.deltaTime;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.timeScale > 0)
        {
            if(delayedAttack <= 0)
            {
                if (collision.CompareTag("EnemyLaser"))
                {
                    EnemyBlocked(collision.GetComponent<EnemyBulletStats>().damage);
                }
                delayedAttack = maxDelayedAttack;
            }
            else
            {
                delayedAttack -= Time.deltaTime;
            }
        }
    }
    public void EnemyBlocked(int damage)
    {
        int ss = shieldStrength - damage;
        if (ss > 0)
        {
            shieldStrength = ss;
        }
        else
        {
            if(Upgraded)
            {
                GetComponentInParent<ShieldManager>().CheckCount();
                gameObject.SetActive(false);
            }
            else
            {
                //Destroy(this.gameObject);
                gameObject.SetActive(false);
                transform.parent = parentNode;
            }
            
        }
    }
}

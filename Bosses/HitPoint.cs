using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    public int health;
    public EnemyBossText eBT;
    public bool destroyed;
    public void LoseHealth(int amount)
    {
        int newHealth = health -= amount;
        if (newHealth > 0)
        {
            health = newHealth;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            //GetComponentInParent<MiniSpawnerController>().LoseHealth(collision.GetComponent<BulletStat>().damage);
            LoseHealth(collision.GetComponent<BulletStat>().damage);
            Destroy(collision.gameObject);
        }
    }
}

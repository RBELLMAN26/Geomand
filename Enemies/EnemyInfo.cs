using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    public int amountOfScore;
    public int health;
    public int maxHealth;
    public float speed, shieldspeed, savedSpeed;
    public int chanceOfShield = 1; //Based Off of 100 percent so example would be that 1 is 1 percent chance of getting.
    public float shieldTime;
    public bool shieldChance;
    //Damage Is Different Since Towers Will Have More Health Then Base
    public bool timeSlow;
    public int damageDealtToTowers;
    public int difficulty;
    public GameObject battery;
    public GameObject shield;
    public int batteryChance = 10;
    public bool isFrozen;
    public GameObject deathParticles;
}

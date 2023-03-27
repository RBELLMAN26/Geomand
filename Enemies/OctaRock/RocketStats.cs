using UnityEngine;

public class RocketStats : MonoBehaviour
{
    public float timeTillDeath = 10;
    public float speed = 12;
    //Once the bullet Reaches this point the bullet will destroy itself.
    //public Vector2 destinToDest;
    public GameObject deathParticles;
    public int amountOfScore;
    public int health;
    //public bool useAShield = false;
    public int chanceOfShield = 0; //Based Off of 100 percent so example would be that 1 is 1 percent chance of getting.
    //Damage Is Different Since Towers Will Have More Health Then Base
    public float timeSlow;
    public int damageDealtToTowers;
    public int damageDealtToBase;
    public int difficulty;
    public bool isFrozen;
    //public GameObject nullifiedField;

}

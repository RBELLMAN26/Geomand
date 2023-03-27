using UnityEngine;

public class BulletStat : MonoBehaviour
{
    public int currentTouch = 0;
    public int currentColor = 0;
    public float timeTillDeath = 10;
    public float speed = 12;
    public int damage = 1;
    public float slowTime = 0;
    public float slowPercentage = 0;
    public int amountOfPiercing = 0;
    //Once the bullet Reaches this point the bullet will destroy itself.
    public Vector2 destinToDest;
    public GameObject redExplosive;
    public Transform fromTurretToReturn;
    public bool upgradedBlue = false, upgradedRed = false;
    void Start()
    {
        switch (currentColor)
        {
            case 0:
                {
                    //GetComponent<SpriteRenderer>().color = Color.green;
                    break;
                }
            case 1:
                {
                    //GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
                }
            case 2:
                {
                    //GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                }
        }
    }

}

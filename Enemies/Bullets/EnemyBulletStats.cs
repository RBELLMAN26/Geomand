using UnityEngine;

public class EnemyBulletStats : MonoBehaviour
{
    public int damage = 1;
    public int currentTouch = 0;
    public int currentColor = 0;
    //public float timeTillDeath = 10;
    public float speed = 12;
    //Once the bullet Reaches this point the bullet will destroy itself.
    public Vector2 destinToDest;
    //public bool isNullified = false;
    //public GameObject nullifiedField;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -50)
        {
            Death();
        }
        /*
        if (timeTillDeath > 0)
        {
            timeTillDeath -= Time.deltaTime;
        }
        else
        {
            Death();
        }
        if (GetComponent<SpriteRenderer>())
        {
            GetComponent<SpriteRenderer>().enabled = !isNullified;
        }
        */

    }
    public void Death()
    {
        Destroy(this.gameObject);
    }
    /*
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Nulield"))
        {
            if (col.GetComponent<FieldsInfo>().nullifyTouch == currentTouch)
            {
                //nullifiedField = col.gameObject;
                //isNullified = true;
                //GetComponent<SpriteRenderer>().color = 0;
                print("Is Nullified");
            }

        }
    }
    */
    /*
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Nulield"))
        {
            if (nullifiedField != null)
            {
                nullifiedField = null;
                isNullified = false;
            }

        }
    }
    */
}

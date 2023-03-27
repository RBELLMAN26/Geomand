using UnityEngine;

public class BaseController : MonoBehaviour
{
    public BaseInfo bI;
    float constHit = 0.25f, maxConstHit = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        bI = GetComponent<BaseInfo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print(collision.name);
        if (collision.CompareTag("EnemyBullet"))
        {
            bI.LoseHealth();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("EnemyLaser"))
        {
            bI.LoseHealth();
        }
        if(collision.GetComponent<Sun>())
        {
            bI.health = 0;
            GameObject.FindObjectOfType<GameManager>().GameOver();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyLaser"))
        {
            if (constHit > 0)
            {
                constHit -= Time.deltaTime;
            }
            else
            {
                bI.LoseHealth();
                constHit = maxConstHit;
            }

        }
    }
}

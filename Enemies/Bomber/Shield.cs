using UnityEngine;

public class Shield : MonoBehaviour
{
    public int color;
    SpriteRenderer sR;

    public void OnEnable()
    {
        sR = GetComponent<SpriteRenderer>();
        color = Random.Range(0, 3);
        switch (color)
        {
            //Green
            case 0:
                {
                    if(sR.color != Color.green)
                    {
                        sR.color = Color.green;
                    }
                    break;
                }
            //Blue
            case 1:
                {
                    if (sR.color != Color.blue)
                    {
                        sR.color = Color.blue;
                    }
                    break;
                }
            //Red
            case 2:
                {
                    if (sR.color != Color.red)
                    {
                        sR.color = Color.red;
                    }
                    break;
                }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletStat bS = collision.gameObject.GetComponent<BulletStat>();
            if (bS.currentTouch != color)
            {
                if (bS.currentColor != 1)
                {
                    collision.gameObject.GetComponent<BulletController>().ApplyDeath(true);
                }
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletStat bS = collision.gameObject.GetComponent<BulletStat>();
            if (bS.currentTouch != color)
            {
                if (bS.currentColor != 1)
                {
                    collision.gameObject.GetComponent<BulletController>().ApplyDeath(true);
                }
            }
        }
    }
}

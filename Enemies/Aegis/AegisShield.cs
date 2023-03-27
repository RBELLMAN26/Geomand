using UnityEngine;

public class AegisShield : MonoBehaviour
{
    public int colorToBlock;
    // Start is called before the first frame update

    public void CreateShield(int color)
    {
        colorToBlock = color;
        switch (color)
        {
            case 0:
                {
                    GetComponent<SpriteRenderer>().color = Color.green;
                    break;
                }
            case 1:
                {
                    GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
                }
            case 2:
                {
                    GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletStat bS = collision.gameObject.GetComponent<BulletStat>();
            if (bS.currentTouch != colorToBlock)
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
            if (bS.currentTouch != colorToBlock)
            {
                if (bS.currentColor != 1)
                {
                    collision.gameObject.GetComponent<BulletController>().ApplyDeath(true);
                }
            }
        }
    }
}

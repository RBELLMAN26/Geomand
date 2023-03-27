using UnityEngine;

public class FieldsController : MonoBehaviour
{
    public FieldsInfo fI;
    public int dir;
    float deathClock = 15;
    // Start is called before the first frame update
    void Start()
    {
        fI = GetComponent<FieldsInfo>();
        dir = fI.direction;
    }

    // Update is called once per frame
    void Update()
    {
        switch (dir)
        {
            case 0:
                {
                    Movement(Vector2.right);
                    break;
                }
            case 1:
                {
                    Movement(Vector2.left);
                    break;
                }
            case 2:
                {
                    Movement(Vector2.down);
                    break;
                }
        }
        if (deathClock > 0)
        {
            deathClock -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Movement(Vector3 direction)
    {
        transform.position += (direction * Time.deltaTime * fI.speed);
    }
}

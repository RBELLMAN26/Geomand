using UnityEngine;

public class BossController : MonoBehaviour
{
    public BossStats bS;
    // Start is called before the first frame update
    void Start()
    {
        bS = GetComponent<BossStats>();
    }

    public void Movement(Vector3 dir)
    {
        if (bS.timeSlow > 0)
        {
            transform.position += (dir * Time.deltaTime * (bS.speed - ((bS.speed / bS.slowPercentage) + (bS.speed / 2))));
        }
        else
        {
            transform.position += (dir * Time.deltaTime * bS.speed);
        }
    }

    public void LoseHealth(int amount)
    {

    }
}

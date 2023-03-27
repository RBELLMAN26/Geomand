using UnityEngine;

public class RegenMini : MonoBehaviour
{
    public int healthToRegen;
    public Transform target;
    public float speed;
    float timeTillDeath = 10;

    // Update is called once per frame
    void Update()
    {
        transform.Translate((target.position - transform.position) * speed);
        //print(target.position);
        if (timeTillDeath > 0)
        {
            timeTillDeath -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
        if(!target.gameObject.activeSelf || target == null)
        {
            Destroy(this.gameObject);
        }
    }
}

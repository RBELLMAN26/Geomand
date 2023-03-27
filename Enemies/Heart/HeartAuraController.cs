using UnityEngine;

public class HeartAuraController : MonoBehaviour
{
    public int healthToGive = 10;
    public float timeToDestroy = 2;
    public float rateOfExpansion = 1;

    // Update is called once per frame
    void Update()
    {
        Vector3 size = transform.localScale;
        size.x += Time.deltaTime * rateOfExpansion;
        size.y += Time.deltaTime * rateOfExpansion;
        transform.localScale = size;
        if (timeToDestroy > 0)
        {
            timeToDestroy -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}

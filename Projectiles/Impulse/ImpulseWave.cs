using UnityEngine;

public class ImpulseWave : MonoBehaviour
{
    public float slowTime;
    public float slowPercentage;
    public float timeToDestroy = 5;
    public float rateOfExpansion;
    
    // Update is called once per frame
    void Update()
    {
        if (timeToDestroy > 0)
        {
            transform.localScale += new Vector3(Time.deltaTime * rateOfExpansion, Time.deltaTime * rateOfExpansion, 0);
            timeToDestroy -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
}

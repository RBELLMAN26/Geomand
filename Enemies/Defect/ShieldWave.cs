using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWave : MonoBehaviour
{
    public float amountOfShield; //Provides how much time the shield remains for the enemy.
    public float maxTime = 15; //Max Time On the Shield, will be improved when difficulty is raised.
    public float timeToDestroy = 2;
    float rateOfExpansion = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        amountOfShield = Random.Range(maxTime / 2, maxTime);
    }

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

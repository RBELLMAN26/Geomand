using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveWave : MonoBehaviour
{
    float timeToDestroy = 1.5f;
    public int damage = 10;
    void Start()
    {
        StartCoroutine(DestroyMe());
    }
   
    IEnumerator DestroyMe()
    {
        while(timeToDestroy > 0)
        {
            transform.localScale += new Vector3(Time.deltaTime * 3f, Time.deltaTime * 3f, 0);
            timeToDestroy -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
    }
}

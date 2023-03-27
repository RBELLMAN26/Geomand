using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseEnemies : MonoBehaviour
{
    float timeToDestroy = 10;
    void OnEnable()
    {
        timeToDestroy = 10;
    }
    // Update is called once per frame
    void Update()
    {
        if (timeToDestroy > 0)
        {
            timeToDestroy -= Time.deltaTime;
        }
        else
        {
            //Destroy(this.gameObject);
            gameObject.SetActive(false);
        }
    }
}

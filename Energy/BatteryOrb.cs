using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryOrb : MonoBehaviour
{
    public Transform energyTarget;
    public float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        energyTarget = FindObjectOfType<EnergyStats>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(energyTarget != null)
        {
            if (Vector2.Distance(transform.position, energyTarget.position) < 0.5f)
            {
                Destroy(this.gameObject);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, energyTarget.position, speed * Time.deltaTime);
            }
        }
        else
        {
            OnDeath();
        }
    }
    public void OnDeath()
    {
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public BossStats bS;
    public BossController bC;
    public GameObject[] miniSpawners; //Three Spawners,  Left(Small Enemies),Right (Small Enemies), Middle(Large Enemies)
    // Start is called before the first frame update
    void Start()
    {
        bS = GetComponent<BossStats>();
        bC = GetComponent<BossController>();
        UpdateSpawners();
    }

    // Update is called once per frame
    void Update()
    {
        bC.Movement(Vector2.down);
    }

    void UpdateSpawners()
    {
        for (int i = 0; i < miniSpawners.Length; i++)
        {
            miniSpawners[i].GetComponent<MiniSpawnerStats>().health = bS.health;
        }
        
    }

    void ApplyDeath()
    {
        Destroy(this.gameObject);
    }
}

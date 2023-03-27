using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    // Start is called before the first frame update
    EnemyBulletStats bS;
    void Start()
    {
        bS = GetComponent<EnemyBulletStats>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.up * Time.deltaTime * bS.speed);
    }
}

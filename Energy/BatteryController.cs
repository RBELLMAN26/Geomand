using UnityEngine;

public class BatteryController : MonoBehaviour
{
    [SerializeField] BatteryStats eS;
    [SerializeField] GameObject deathOrb;
    Transform node;
    
    // Start is called before the first frame update
    void Start()
    {
        eS = GetComponent<BatteryStats>();
        node = FindObjectOfType<NodeController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(node != null)
        {
            transform.position += (-Vector3.up * Time.deltaTime * eS.speed);
            transform.Rotate(new Vector3(0, 0, -30 * Time.deltaTime));
            if (transform.position.y <= -20)
            {
                OnDeath();
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            if(FindObjectOfType<NodeInfo>())
            {
                FindObjectOfType<NodeInfo>().UpdateEnergy(eS.amountOfEnergy);
            }
            Instantiate(deathOrb, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}

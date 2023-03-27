using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    //This will get spawned out of the player 
    public Transform RegenMini;
    [SerializeField] TowerInfo[] towers;
    [SerializeField] Transform[] targets = new Transform[5];
    public Transform[] regens;
    public float duration;
    public float timeOfMinis = 2;
    public float rateOfMinis; //How Many Minis Get Created To Heal
    public float healthToRegen = 10;
    int dir;
    [SerializeField] bool isUpgraded;
    public Transform parentNode;
    // Start is called before the first frame update
    void OnEnable()
    {
        if(isUpgraded)
        {
            duration = 15;
        }
        towers = FindObjectOfType<TowerManager>().turrets;
        for (int i = 0; i < towers.Length; i++)
        {
            targets[i] = towers[i].transform;
        }
        if (FindObjectOfType<NodeController>())
        {
            targets[4] = FindObjectOfType<NodeController>().transform;
        }
        dir = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(dir == 0)
        {
            //transform.Rotate(new Vector3(0, 0, 60 * Time.deltaTime));
            foreach (Transform obj in regens)
            { 
                obj.Rotate(new Vector3(0, 0, -60 * Time.deltaTime));
            }
        }
        else
        {
            //transform.Rotate(new Vector3(0, 0, -60 * Time.deltaTime));
            foreach (Transform obj in regens)
            {
                obj.Rotate(new Vector3(0, 0, 60 * Time.deltaTime));
            }
        }
        if (duration > 0)
        {
            if (rateOfMinis > 0)
            {
                rateOfMinis -= Time.deltaTime;
            }
            else
            {
                int rand = Random.Range(0, 5);
                if (rand != 4)
                {
                    if(targets[rand].gameObject.activeSelf)
                    {
                        Transform newMini = Instantiate(RegenMini, regens[Random.Range(0, regens.Length)].position, transform.rotation);
                        newMini.GetComponent<RegenMini>().target = targets[rand].transform;
                        newMini.transform.parent = null;
                        rateOfMinis = timeOfMinis;
                    }
                }
                else
                {
                    if (targets[4].gameObject.activeSelf)
                    {
                        Transform newMini = Instantiate(RegenMini, regens[Random.Range(0, regens.Length)].position, transform.rotation);
                        newMini.GetComponent<RegenMini>().target = targets[4];
                        newMini.transform.parent = null;
                        rateOfMinis = timeOfMinis;
                    }
                }
            }
            duration -= Time.deltaTime;
        }
        else
        {
            if(isUpgraded)
            {
                //transform.parent.gameObject.SetActive(false);
                GetComponentInParent<HealthRegenManager>().ReturnToNode();
            }
            else
            {
                gameObject.SetActive(false);
                transform.parent = parentNode;
            }
            //Destroy(this.gameObject);
            
        }

    }
}

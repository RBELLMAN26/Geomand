using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    public Transform[] lPositionObjs;
    public Transform[] rPositionObjs;
    public Transform[] tPositionObjs;
    public Transform[] fields;
    //public int rand;
    public float currentTime, maxTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            RandomizeSpawn();
            currentTime = maxTime;
        }
    }

    void RandomizeSpawn()
    {
        int RandomSelection = Random.Range(0, 3);
        switch (RandomSelection)
        {
            case 0:
                {
                    int leng = Random.Range(0, lPositionObjs.Length);
                    int fieldLength = Random.Range(0, fields.Length);
                    Transform newField = Instantiate(fields[fieldLength], lPositionObjs[leng].position, transform.rotation);
                    newField.GetComponent<FieldsInfo>().direction = 0;
                    break;
                }
            case 1:
                {
                    int leng = Random.Range(0, rPositionObjs.Length);
                    int fieldLength = Random.Range(0, fields.Length);
                    Transform newField = Instantiate(fields[fieldLength], rPositionObjs[leng].position, transform.rotation);
                    newField.GetComponent<FieldsInfo>().direction = 1;
                    break;
                }
            case 2:
                {
                    int leng = Random.Range(0, tPositionObjs.Length);
                    int fieldLength = Random.Range(0, fields.Length);
                    Transform newField = Instantiate(fields[fieldLength], tPositionObjs[leng].position, transform.rotation);
                    newField.GetComponent<FieldsInfo>().direction = 2;
                    break;
                }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    float lifeTime = 30;
    float speed = 0.1f;
    float maxSpeed = 0.5f;
    public int direction = 1;  // 1 - From Left, -1 - From Right
    public Vector2 screenBounds;
    private float objectWidth, objectHeight;
    [SerializeField] bool ableToRain = true;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        speed = Random.Range(speed, maxSpeed);
        int rainChance = Random.Range(0, 5);
        if(ableToRain && rainChance == 1)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(OutOfBounds())
        {
            Destroy(this.gameObject);
        }
        /*
        if(lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
        */
        Movement(direction);
    }
    void Movement(float direction)
    {
        transform.position += (Vector3.left * direction) * (speed * Time.deltaTime);
    }

    bool OutOfBounds()
    {
        if(direction == -1)
        {
            if ((transform.position.x) >= screenBounds.x + objectWidth)
            {
                return true;
            }
        }
        else
        {
            if ((transform.position.x) <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - objectWidth)
            {
                return true;
            }
        }
        
        
        return false;
    }
}

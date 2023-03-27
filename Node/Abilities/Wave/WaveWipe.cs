using UnityEngine;

public class WaveWipe : MonoBehaviour
{
    public float speed = 5;
    public int damage = 2;
    [SerializeField] bool isUpgrade;
    public Transform parentNode;
    // Update is called once per frame
    void Update()
    {
        if(isUpgrade)
        {
            transform.Rotate(new Vector3(0, 0, 25 * Time.deltaTime));
            transform.position += (Vector3.up * Time.deltaTime * speed);
        }
        else
        {
            transform.position += (transform.up * Time.deltaTime * speed);
        }
        if (transform.position.y > 10)
        {
            gameObject.SetActive(false);
            transform.parent = parentNode;
            //transform.localPosition = Vector2.zero;
            /*
            //Destroy(this.gameObject);
            if(isUpgrade)
            {
                transform.parent.gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
            */
        }
    }
}

using UnityEngine;

public class FieldsInfo : MonoBehaviour
{
    //This Scripts contains to Nullifying The Bullets in the field that is provided,  Will Create Different Modes For This Effect
    //After Testing Of Nullifying Will Add Modes.
    //Modes Will Consists of the Object Changing Shape and moving around. Will be adding a random to change the color or touch to nullify.
    // Start is called before the first frame update

    public int nullifyTouch; // This will be 0 or 1 for the current Bullets.
    public float timeTillDeath;
    public int direction;
    public float speed = 1;
    void Start()
    {
        nullifyTouch = Random.Range(0, 2);
        switch (nullifyTouch)
        {
            case 0:
                {
                    GetComponent<SpriteRenderer>().color = Color.green;
                    break;
                }
            case 1:
                {
                    GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
                }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {

        }
    }
}

using UnityEngine;

public class BackGroundFX : MonoBehaviour
{
    //This Scripts contains to Nullifying The Bullets in the field that is provided,  Will Create Different Modes For This Effect
    //After Testing Of Nullifying Will Add Modes.
    //Modes Will Consists of the Object Changing Shape and moving around. Will be adding a random to change the color or touch to nullify.
    // Start is called before the first frame update

    public int nullifyTouch; // This will be 0 or 1 for the current Bullets.
    public float timeTillDeath;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {

        }
    }
}

using UnityEngine;

public class CursorGUI : MonoBehaviour
{
    public int colorNum;
    int dir;
    // Start is called before the first frame update
    void Start()
    {
        dir = Random.Range(0, 2);
        switch (colorNum)
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

    private void Update()
    {
        if(dir == 0)
        {
            transform.Rotate(new Vector3(0, 0, 60 * Time.deltaTime));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, -60 * Time.deltaTime));
        }
        
    }
}

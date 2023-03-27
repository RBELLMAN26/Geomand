using UnityEngine;

public class TurretAimer : MonoBehaviour
{
    public Vector2 target, touchPosition;
    public TowerController tC;
    // Update is called once per frame
    void Update()
    {
        target = tC.tI.selectedPOS;
        if (target != Vector2.zero)
        {
            //target = Vector2.up * 2;
            LookatTarget();
        }
    }
    void LookatTarget()
    {
        /*
        Vector2 direction = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //print(rotation);
        transform.eulerAngles = new Vector3(0, 0, rotation - 90);
        */
        touchPosition = Camera.main.ScreenToWorldPoint(target);
        touchPosition += new Vector2(0, 1);
        // get direction you want to point at // there was a normalized part in here that I removed if anything goes wrong
        Vector2 direction = ((touchPosition) - (Vector2)transform.position);
        // set vector of transform directly
        transform.up = direction;
    }
}

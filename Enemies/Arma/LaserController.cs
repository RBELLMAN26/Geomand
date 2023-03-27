using UnityEngine;

public class LaserController : MonoBehaviour
{

    public LineRenderer lR;
    public BoxCollider2D lRc;
    public PolygonCollider2D lRPc;
    //public Transform target;
    public bool fixedDistance;
    public float Distance = 100;
    public float freezerDist = 100;
    public bool phasable;
    public float TimeToActivate = 10.0f;
    public float TimeToDeactivate = 5.0f;
    public bool UseTime = true;
    public Vector2 lRPOS;
    public Vector2 tarPos;
    public float size = 0.1f;
    // Use this for initialization
    void Start()
    {
        lR.startWidth = size;
        lR.endWidth = size;
    }

    // Update is called once per frame
    void Update()
    {
        //lR.SetPosition(0, transform.localPosition);
        lR.SetPosition(0, new Vector2(0, 0));
        //TimeToDeactivate means the laser is active
        //TimeToActivate means the laser is off
        if (UseTime)
        {
            if (TimeToDeactivate > 0)
            {
                lR.gameObject.SetActive(true);
                FireLaser();
                TimeToDeactivate -= Time.deltaTime;
                TimeToActivate = 10.0f;
            }
            else
            {
                if (TimeToActivate > 0)
                {
                    lR.gameObject.SetActive(false);
                    TimeToActivate -= Time.deltaTime;
                }
                else
                {
                    TimeToDeactivate = 5.0f;
                }
            }
        }
        else
        {
            FireLaser();
        }
        //lRPc.points.SetValue(new Vector2(0, 5), 0);
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, target.transform.localPosition) * new Quaternion(0,0,0,0);
        //Vector3 perpendicular = transform.position - target.position;
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 90);
        //Raycast
    }

    void FireLaser()
    {
        lRPOS = new Vector2(lR.transform.position.x, lR.transform.position.y);
        //tarPos = new Vector2(target.transform.position.x, target.transform.position.y);
        Vector2 direction = tarPos - lRPOS;
        //Ray2D ray = new Ray2D(lRPOS, tarPos);
        //Ray2D ray = new Ray2D(lRPOS, transform.up * Distance); //Keep Old
        Ray2D ray = new Ray2D(lRPOS, transform.up * Mathf.Infinity); //New Test
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, Distance); //Keep Old
        RaycastHit2D hit;
        hit = Physics2D.Raycast(lRPOS, transform.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("Structures"));

        if (hit.collider != null)
        {
            Distance = Vector2.Distance(transform.position, hit.point);
            //print(hit.collider.name);
        }
        else
        {
           Distance = 50;          
        }

        
        Vector3 point = ray.origin + (ray.direction * Distance);
        Vector3 lpoint = transform.InverseTransformPoint(point);
        //print("Not Hitting");
        //lR.SetPosition(1, tarPos - lRPOS);
        //-Was in Originally lR.SetPosition(1, new Vector2(lpoint.x - transform.localPosition.x, lpoint.y - transform.localPosition.y));
        //lR.SetPosition(1, new Vector2(point.x - lRPOS.x, point.y - lRPOS.y));
        lR.SetPosition(1, new Vector2(0, Distance));
        lRc.offset = lR.GetPosition(1) / 2;
        //lRc.size = new Vector2(lR.GetPosition(1).x, 10);
        lRc.size = new Vector2((size), Distance);
        //lRPc.points = new[] { new Vector2(lR.GetPosition(0).x, lR.GetPosition(0).y + 5), new Vector2(lR.GetPosition(0).x, lR.GetPosition(0).y - 5), new Vector2(lR.GetPosition(1).x, lR.GetPosition(1).y - 5), new Vector2(lR.GetPosition(1).x, lR.GetPosition(1).y + 5) };
        //Points 0 - 4 Of A Box
        //lRPc.points = new[] { new Vector2(lR.GetPosition(0).x, lR.GetPosition(0).y + 0.1f), new Vector2(lR.GetPosition(0).x, lR.GetPosition(0).y - 0.1f), new Vector2(lR.GetPosition(1).x, lR.GetPosition(1).y - 0.1f), new Vector2(lR.GetPosition(1).x, lR.GetPosition(1).y + 0.1f) };
        lRPc.points = new[] { new Vector2(lR.GetPosition(0).x - (size / 2), 0), new Vector2(lR.GetPosition(0).x + (size / 2), 0), new Vector2(lR.GetPosition(1).x + (size / 2), Distance), new Vector2(lR.GetPosition(1).x - (size / 2), Distance) };
        //Point 1 is -Size / 2 , 0 [] Point 2 is Size / 2 , 0 [] Point 3 
        //We need width (size) / 2
    }
}

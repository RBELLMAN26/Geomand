using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class FreezeLaserController : MonoBehaviour
{
    public BulletStat bS;
    public LineRenderer lR;
    public PolygonCollider2D lRPc;
    //public Transform target;
    //public bool fixedDistance;
    public float Distance = 100;
    public float freezerDist = 100;
    //public bool phasable;
    public Vector2 lRPOS;
    public Vector2 tarPos;
    public float size = 0.1f;
    public bool isBlueUpgraded;
    public GameObject impulseWave;
    public float expansionRate;
    float delayBetweenWave = 1f, maxDelayBetweenWave = 1f;
    RaycastHit2D hit, shieldHit;
    // Use this for initialization
    // Start is called before the first frame update
    void OnEnable()
    {
        bS = GetComponent<BulletStat>();
        //lR = GetComponent<LineRenderer>();
        lRPc = GetComponent<PolygonCollider2D>();
        lR.SetPosition(0, new Vector2(0, 0));
        lR.startWidth = size;
        lR.endWidth = size;
    }
    void OnDisable()
    {
        lR.enabled = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        FireLaser();
        if(lR.enabled == false)
        {
            lR.enabled = true;
        }
    }
    void FireLaser()
    {
        lRPOS = new Vector2(lR.transform.position.x, lR.transform.position.y);
        Vector2 direction = tarPos - lRPOS;
        Ray2D ray = new Ray2D(lRPOS, transform.up * bS.destinToDest);
        
        //hit = Physics2D.Raycast(lRPOS, transform.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("Enemy"));
        hit = Physics2D.Raycast(lRPOS, transform.up, Vector2.Distance(transform.InverseTransformPoint(transform.position), transform.InverseTransformPoint(bS.destinToDest)), (1 << LayerMask.NameToLayer("Enemy")));
        shieldHit = Physics2D.Raycast(lRPOS, transform.up, Vector2.Distance(transform.InverseTransformPoint(transform.position), transform.InverseTransformPoint(bS.destinToDest)), (1 << LayerMask.NameToLayer("Shield")));
        if (hit.collider != null || shieldHit.collider != null)
        {
            if(hit.collider != null)
            {
                Distance = Vector2.Distance(transform.InverseTransformPoint(transform.position), transform.InverseTransformPoint(hit.point));
                //print(hit.collider.name);
            }
            if(shieldHit.collider != null)
            {
                if(shieldHit.collider.gameObject.GetComponent<Shield>())
                {
                    if (shieldHit.collider.gameObject.GetComponent<Shield>().color != bS.currentColor)
                    {
                        Distance = Vector2.Distance(transform.InverseTransformPoint(transform.position), transform.InverseTransformPoint(shieldHit.point));
                    }
                    else
                    {
                        Distance = Vector2.Distance(transform.InverseTransformPoint(transform.position), transform.InverseTransformPoint(hit.point));
                    }
                }
                else if(shieldHit.collider.gameObject.GetComponent<AegisShield>())
                {
                    if (shieldHit.collider.gameObject.GetComponent<AegisShield>().colorToBlock != bS.currentColor)
                    {
                        Distance = Vector2.Distance(transform.InverseTransformPoint(transform.position), transform.InverseTransformPoint(shieldHit.point));
                    }
                    else
                    {
                        Distance = Vector2.Distance(transform.InverseTransformPoint(transform.position), transform.InverseTransformPoint(hit.point));
                    }
                }
                else
                {
                    Distance = Vector2.Distance(transform.InverseTransformPoint(transform.position), transform.InverseTransformPoint(hit.point));
                }

            }      
        }
        else
        {
            //Distance = freezerDist;
            Distance = Vector2.Distance(transform.InverseTransformPoint(transform.position), transform.InverseTransformPoint(bS.destinToDest));
        }

        Vector3 point = ray.origin + (ray.direction * Distance);
        //Vector3 lpoint = transform.InverseTransformPoint(point);
        lR.SetPosition(1, new Vector2(0, Distance));
        //lRc.offset = lR.GetPosition(1) / 2;
        //lRc.size = new Vector2((size), Distance);
        lRPc.points = new[] { new Vector2(lR.GetPosition(0).x - (size / 2), 0), new Vector2(lR.GetPosition(0).x + (size / 2), 0), new Vector2(lR.GetPosition(1).x + (size / 2), Distance), new Vector2(lR.GetPosition(1).x - (size / 2), Distance) };

        if (isBlueUpgraded)
        {
            if (delayBetweenWave <= 0)
            {
                GameObject newObject = (GameObject)Instantiate(impulseWave, bS.destinToDest, transform.rotation);
                newObject.GetComponent<ImpulseWave>().slowTime = bS.slowTime;
                newObject.GetComponent<ImpulseWave>().slowPercentage = bS.slowPercentage;
                newObject.GetComponent<ImpulseWave>().rateOfExpansion = expansionRate;
                delayBetweenWave = maxDelayBetweenWave;
            }
            else
            {
                delayBetweenWave -= Time.deltaTime;
            }
        }
    }
}

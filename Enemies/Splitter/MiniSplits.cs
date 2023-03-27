using System.Collections;
using UnityEngine;

public class MiniSplits : MonoBehaviour
{
    
    EnemyInfo eI;
    public EnemyController eC;
    public Transform target;
    public TowerController[] tC;
    public Transform nodeTarget;
    public Transform[] baseHits;
    
    public float countToAim = 1;
    Vector3 dodgePos;
    bool isDodge;
    public Vector2 screenBounds;
    private float objectWidth;
    public float dodgeRate = 3;
    bool waiting;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        eI = GetComponent<EnemyInfo>();
        eC = GetComponent<EnemyController>();
        tC = FindObjectsOfType<TowerController>();
        NodeInfo tempNode = FindObjectOfType<NodeInfo>();
        if(tempNode != null)
        {
            nodeTarget = tempNode.GetComponent<Transform>();
        }
        baseHits = FindObjectOfType<BaseInfo>().hits;
        SelectTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(!eI.isFrozen)
        {
            if (countToAim > 0)
            {
                countToAim -= Time.deltaTime;
            }
            else
            {
                if(target != null)
                {
                    if (target.gameObject.activeSelf)
                    {
                        /*
                        if (!waiting)
                        {
                            waiting = true;
                            //StartCoroutine(LookatTarget());
                        }
                        */
                        LookatTarget();
                    }
                    else
                    {
                        /*
                        waiting = false;
                        StopCoroutine(LookatTarget());
                        SelectTarget();
                        */
                        SelectTarget();
                    }
                }
                else
                {
                    /*
                    waiting = false;
                    StopCoroutine(LookatTarget());
                    SelectTarget();
                    */
                    SelectTarget();
                }

            }
            if (!isDodge)
            {
                if (dodgeRate > 0)
                {
                    if(eC != null)
                    {
                        eC.Movement(-transform.up);
                        dodgeRate -= Time.deltaTime;
                    }
                    else
                    {
                        eC = GetComponent<EnemyController>();
                    }
                }
                else
                {
                    dodgePos = Dodge();
                    isDodge = true;
                }

            }
            else
            {
                if(dodgePos == Vector3.zero)
                {
                    dodgePos = Dodge();
                }
                if (Vector2.Distance(transform.position, dodgePos) > 0.1f)
                {
                    transform.position = Vector3.Slerp(transform.position, dodgePos, Time.deltaTime * 10);
                }
                else
                {
                    isDodge = false;
                    dodgeRate = 3;
                }

            }
        }
        
    }
    Vector3 Dodge()
    {
        int dir = Random.Range(0, 8);
        switch (dir)
        {
            //Up
            case 0:
                {
                    return transform.position + Vector3.up;
                }
            //Down
            case 1:
                {
                    return transform.position + Vector3.down;
                }
            //Right
            case 2:
                {
                    Vector3 pos = transform.position + Vector3.right;
                    //if (pos.x - objectWidth <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
                    if (pos.x + objectWidth >= screenBounds.x)
                    {
                        return transform.position + Vector3.left;
                    }
                    else
                    {
                        return transform.position + Vector3.right;
                    }
                }
            //Left
            case 3:
                {
                    Vector3 pos = transform.position + Vector3.left;
                    if (pos.x - objectWidth <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
                    {
                        return transform.position + Vector3.right;
                    }
                    else
                    {
                        return transform.position + Vector3.left;
                    }

                }
            //UpLeft
            case 4:
                {
                    Vector3 pos = transform.position + (Vector3.up + Vector3.left);
                    if (pos.x - objectWidth <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
                    {
                        return transform.position + (Vector3.up + Vector3.right);
                    }
                    else
                    {
                        return transform.position + (Vector3.up + Vector3.left);
                    }

                }
            //UpRight
            case 5:
                {
                    Vector3 pos = transform.position + (Vector3.up + Vector3.right);
                    //if (pos.x - objectWidth <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
                    if (pos.x + objectWidth >= screenBounds.x)
                    {
                        return transform.position + (Vector3.up + Vector3.left);
                    }
                    else
                    {
                        return transform.position + (Vector3.up + Vector3.right);
                    }

                }
            //DownLeft
            case 6:
                {
                    Vector3 pos = transform.position + (Vector3.up + Vector3.left);
                    if (pos.x - objectWidth <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
                    {
                        return transform.position + (Vector3.down + Vector3.right);
                    }
                    else
                    {
                        return transform.position + (Vector3.down + Vector3.left);
                    }
                }
            //DownRight
            case 7:
                {
                    Vector3 pos = transform.position + (Vector3.up + Vector3.right);
                    //if (pos.x - objectWidth <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
                    if (pos.x + objectWidth >= screenBounds.x)
                    {
                        return transform.position + (Vector3.down + Vector3.left);
                    }
                    else
                    {
                        return transform.position + (Vector3.down + Vector3.right);
                    }
                }

        }
        return Vector3.zero;
    }
    void LookatTarget()
    {
        Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.transform.eulerAngles = new Vector3(0, 0, rotation + 90);
        //yield return new WaitForEndOfFrame();
    }
    void SelectTarget()
    {
        int select = Random.Range(0, 3);
        switch(select)
        {
            case 0://Towers
                {
                    if(tC.Length != 0)
                    {
                        int num = Random.Range(0, tC.Length);
                        if(tC[num].gameObject.activeSelf && tC[num] != null)
                        {
                            target = tC[num].transform;
                        }
                    }
                    break;
                }
            case 1://Node
                {
                    if(nodeTarget != null && nodeTarget.gameObject.activeSelf)
                    {
                        target = nodeTarget;
                    }
                    break;
                }
            case 2://Base
                {
                    int randhits = Random.Range(0, baseHits.Length);
                    target = baseHits[randhits];
                    break;
                }
        }

    }
}

using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    BulletStat bS;
    //public GameObject FreezerBlast;
    LaserController freezerLC;
    Vector2 screenBounds;
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        bS = GetComponent<BulletStat>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (bS.currentColor)
        {
            //Laser
            case 0:
                {
                    transform.position += (transform.up * Time.deltaTime * bS.speed);
                    if(transform.position.y > screenBounds.y + 10 || transform.position.y < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - 10)
                    {
                        gameObject.SetActive(false);
                        transform.parent = bS.fromTurretToReturn;
                    }
                    if(transform.position.x < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - 10 || transform.position.x > screenBounds.x + 10)
                    {
                        gameObject.SetActive(false);
                        transform.parent = bS.fromTurretToReturn;
                    }
                    break;
                }
            //Impulse
            case 1:
                {
                    //transform.position += (transform.up * Time.deltaTime * bS.speed);
                    
                    if (freezerLC == null)
                    {
                        freezerLC = GetComponent<LaserController>();
                    }
                    else
                    {
                        freezerLC.Distance = Vector2.Distance(transform.position, bS.destinToDest);
                        freezerLC.freezerDist = Vector2.Distance(transform.position, bS.destinToDest);
                        //Debug.Log(freezerLC.freezerDist);
                    }
                    
                    break;
                }
            //RailGun
            case 2:
                {
                    transform.position += (transform.up * Time.deltaTime * bS.speed);
                    if (transform.position.y > screenBounds.y + 10 || transform.position.y < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - 10)
                    {
                        gameObject.SetActive(false);
                        transform.parent = bS.fromTurretToReturn;
                    }
                    if (transform.position.x < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - 10 || transform.position.x > screenBounds.x + 10)
                    {
                        gameObject.SetActive(false);
                        transform.parent = bS.fromTurretToReturn;
                    }
                    break;
                }
        }
    }

    public void ApplyDeath(bool shieldhit = false)
    {
        switch (bS.currentColor)
        {
            //Laser
            case 0:
                {
                    //Destroy(this.gameObject);
                    gameObject.SetActive(false);
                    transform.parent = bS.fromTurretToReturn;
                    break;
                }
            //Impulse
            case 1:
                {
                    //GetComponent<Rigidbody2D>().AddForce(transform.up * 10, ForceMode2D.Impulse);
                    if (bS.upgradedBlue)
                    {
                        //GameObject newFreezeBlast = (GameObject)Instantiate(FreezerBlast, transform.position, transform.rotation);
                        //newFreezeBlast.GetComponent<ImpulseWave>().slowPercentage = bS.slowPercentage;
                        //newFreezeBlast.GetComponent<ImpulseWave>().slowTime = bS.slowTime;
                    }
                    //Destroy(this.gameObject);
                    break;
                }
            //RailGun
            case 2:
                {
                    if (bS.upgradedRed)
                    {
                        if (bS.amountOfPiercing > 0 && !shieldhit)
                        {
                            bS.amountOfPiercing -= 1;
                            GameObject exp = (GameObject)Instantiate(bS.redExplosive, transform.position, transform.rotation);
                            exp.GetComponent<ExplosiveWave>().damage = bS.damage;
                        }
                        else
                        {
                            if(shieldhit)
                            {

                                gameObject.SetActive(false);
                                transform.parent = bS.fromTurretToReturn;
                            }
                            else
                            {
                                GameObject exp = (GameObject)Instantiate(bS.redExplosive, transform.position, transform.rotation);
                                exp.GetComponent<ExplosiveWave>().damage = bS.damage;
                                gameObject.SetActive(false);
                                transform.parent = bS.fromTurretToReturn;

                            }
                            
                            //Destroy(this.gameObject);
                        }
                    }
                    else
                    {
                        if(shieldhit)
                        {
                            gameObject.SetActive(false);
                            transform.parent = bS.fromTurretToReturn;
                        }
                        else
                        {
                            GameObject exp = (GameObject)Instantiate(bS.redExplosive, transform.position, transform.rotation);
                            exp.GetComponent<ExplosiveWave>().damage = bS.damage;
                            gameObject.SetActive(false);
                            transform.parent = bS.fromTurretToReturn;
                        }
                       
                        //Destroy(this.gameObject);
                    }

                    break;
                }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            if (bS.currentColor != 1)
            {
                gameObject.SetActive(false);
                transform.parent = bS.fromTurretToReturn;
            }
        }
    }
}

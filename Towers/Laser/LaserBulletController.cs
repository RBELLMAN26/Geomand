using UnityEngine;
using UnityEngine.UI;
public class LaserBulletController : MonoBehaviour
{
    LaserBulletStats lBS;
    SpriteRenderer sR;
    // Start is called before the first frame update
    void Start()
    {
        
        lBS = GetComponent<LaserBulletStats>();
        sR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lBS.tI.isActive)
        {
            lBS.tI.weaponBar.weaponBars[0].value = lBS.chargeLevel;
            if (lBS.tI.tM.pTM.isTouch[1] && lBS.tI.secondAimer)
            {
                lBS.tI.touchSelected = 1;

            }
            else
            {
                lBS.tI.touchSelected = 0;
            }
            if (lBS.tI.disabled)
            {
                if (sR.color == Color.green)
                {
                    sR.color = Color.gray;
                }
            }
            else
            {
                if (sR.color == Color.gray)
                {
                    sR.color = Color.green;
                }
            }
            if (lBS.tA.target != Vector2.zero || lBS.tI.tM.debugMode)
            {
                if(!lBS.tI.disabled)
                {
                    if (!lBS.upgradeGreen)
                    {
                        LaserAutoFire();
                    }
                    else
                    {
                        LaserChargeFire();
                    }
                }
                

            }
            if (!lBS.tI.tM.activeTouch[lBS.tI.touchSelected] && !lBS.tI.tM.debugMode)
            {
                RechargeWeapons();
            }
            
            if (lBS.isReset)
            {
                lBS.isReset = !lBS.isReset;
            }
        }
        else if (!lBS.isReset)
        {
            //ResetAIM();
            lBS.isReset = true;
        }

    }
    void RechargeWeapons()
    {
        if(lBS.chargeLevel > 0)
        {
            lBS.chargeLevel -= Time.deltaTime * lBS.chargeRate;
        }
    }
    void LaserAutoFire()
    {
        if (lBS.tI.tM.activeTouch[0] || lBS.tI.tM.activeTouch[1] || lBS.tI.tM.debugMode)
        {
            if (lBS.tI.tM.activeTouch[lBS.tI.touchSelected] || lBS.tI.tM.debugMode)
            {
                if (lBS.tI.disabledTime <= 0)
                {
                    if (lBS.chargeLevel >= lBS.maxCharge)
                    {
                        for(int i = 0; i < lBS.bullet.Length; i++)
                        {
                            if(!lBS.bullet[i].gameObject.activeSelf)
                            {
                                //GameObject newBullet = (GameObject)Instantiate(lBS.bullet[i].gameObject, lBS.aimer.position, lBS.aimer.rotation);
                                GameObject newBullet = lBS.bullet[i].gameObject;
                                newBullet.transform.position = lBS.aimer.position;
                                newBullet.transform.rotation = lBS.aimer.rotation;
                                newBullet.transform.parent = null;
                                BulletStat bS = newBullet.GetComponent<BulletStat>();
                                bS.damage = lBS.damage;
                                bS.currentColor = lBS.tI.selectedColor;
                                bS.destinToDest = lBS.tI.touchPosition;
                                bS.fromTurretToReturn = lBS.bulletHolder;
                                GetComponent<AudioSource>().Play();
                                lBS.chargeLevel = 0;
                                newBullet.SetActive(true);
                                break;
                            }
                        }
                        
                    }
                    else
                    {
                        lBS.chargeLevel += Time.deltaTime * lBS.chargeRate;
                    }
                }
                else
                {
                    lBS.chargeLevel = 0;
                }
            }
        }


    }
    void LaserChargeFire()
    {
        if (lBS.tI.tM.activeTouch[0] || lBS.tI.tM.activeTouch[1] || lBS.tI.tM.debugMode)
        {
            if (lBS.tI.tM.activeTouch[lBS.tI.touchSelected] || lBS.tI.tM.debugMode)
            {
                if (lBS.chargeLevel >= lBS.maxCharge)
                {
                    if (lBS.delaySpread <= 0)
                    {
                        if (lBS.chargedShots < lBS.maxChargedShots)
                        {
                            GameObject[] newBullet = new GameObject[lBS.maxChargedShots];
                            for (int b = 0; b < lBS.maxChargedShots; b++)
                            {
                                for (int i = 0; i < lBS.bullet.Length; i++)
                                {
                                    if (!lBS.bullet[i].gameObject.activeSelf)
                                    {
                                        float spread = Random.Range(-12f, 12f);
                                        newBullet[b] = lBS.bullet[i].gameObject;
                                        //newBullet[b] = (GameObject)Instantiate(lBS.bullet.gameObject, lBS.aimer.position, Quaternion.Euler(lBS.aimer.eulerAngles.x, lBS.aimer.eulerAngles.y, lBS.aimer.eulerAngles.z + spread));
                                        BulletStat bS = newBullet[b].GetComponent<BulletStat>();
                                        //bS.damage = Mathf.RoundToInt(lBS.damage / lBS.maxChargedShots);
                                        newBullet[b].transform.position = lBS.aimer.position;
                                        newBullet[b].transform.rotation = Quaternion.Euler(lBS.aimer.eulerAngles.x, lBS.aimer.eulerAngles.y, lBS.aimer.eulerAngles.z + spread);
                                        newBullet[b].transform.parent = null;
                                        bS.damage = lBS.damage;
                                        bS.speed = lBS.projectileSpeed;
                                        bS.currentColor = lBS.tI.selectedColor;
                                        bS.destinToDest = lBS.tI.touchPosition;
                                        bS.fromTurretToReturn = lBS.bulletHolder;
                                        GetComponent<AudioSource>().Play();
                                        //lBS.chargeLevel = 0;
                                        newBullet[b].SetActive(true);
                                        break;
                                    }
                                }
                            }
                           
                            lBS.chargedShots += lBS.maxChargedShots;
                        }
                        else
                        {
                            lBS.delaySpread = lBS.maxDelaySpread;
                            lBS.chargeLevel = 0;
                            lBS.chargedShots = 0;
                        }
                    }
                    else
                    {
                       
                        lBS.delaySpread -= Time.deltaTime;
                    }
                }
                else
                {
                    lBS.chargeLevel += Time.deltaTime * lBS.chargeRate;
                }
            }
        }
    }

    void ResetAIM()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}

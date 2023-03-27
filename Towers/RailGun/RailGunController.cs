using UnityEngine;

public class RailGunController : MonoBehaviour
{
    public RailGunStats rS;
    SpriteRenderer sR;
    // Start is called before the first frame update
    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rS.tI.isActive)
        {
            rS.tI.weaponBar.weaponBars[3].value = rS.chargeUp;
            if (rS.tI.tM.pTM.isTouch[1] && rS.tI.secondAimer)
            {
                rS.tI.touchSelected = 1;

            }
            else
            {
                rS.tI.touchSelected = 0;
            }
            if(rS.tI.disabled)
            {
                if(sR.color == Color.red)
                {
                    sR.color = Color.gray;
                    rS.firedRed = false;
                    rS.chargeUp = 0;
                    rS.railGunCharged = false;
                    rS.railgunCharging = false;
                }
            }
            else
            {
                if (sR.color == Color.gray)
                {
                    sR.color = Color.red;
                }
            }
            if (rS.tA.target != Vector2.zero || rS.tI.tM.debugMode)
            {
                rS.touchPosition = rS.tA.touchPosition;
                if (rS.firedRed)
                {
                    if (rS.chargeUp > 0)
                    {
                        RechargeWeapons();
                    }
                    else
                    {
                        rS.firedRed = false;
                        rS.chargeUp = 0;
                    }
                }
                else
                {
                    RailGunChargeUp();
                }
            }
        }
        
        if (!rS.tI.tM.activeTouch[rS.tI.touchSelected] && !rS.tI.tM.debugMode)
        {
            if(!rS.tI.disabled)
            {
                if (rS.chargeUp > 0)
                {
                    RechargeWeapons();
                }
                else
                {
                    rS.firedRed = false;
                    rS.chargeUp = 0;
                }
            }
        }
        if (rS.isReset)
        {
            rS.isReset = !rS.isReset;
        }
        else if (!rS.isReset)
        {
            //ResetAIM();
            rS.isReset = true;
        }
    }
    void RechargeWeapons()
    {
        //if (rS.firedRed)
       // {
            if (rS.chargeUp > 0)
            {
                rS.chargeUp -= Time.deltaTime * rS.coolDownRate;
            }
            else
            {
                rS.firedRed = false;
                rS.chargeUp = 0;
            }
        //}
    }

    //When The Weapon Is Switched To RailGun
    //RailGun will need to be charged at max to fire.
    //If the railgun isn't at max charge it won't fire.
    void RailGunChargeUp()
    {
        if (rS.tI.tM.activeTouch[0] || rS.tI.tM.activeTouch[1] || rS.tI.tM.debugMode)
        {
            if (rS.tI.tM.activeTouch[rS.tI.touchSelected] || rS.tI.tM.debugMode)
            {
                if (rS.tI.disabledTime <= 0)
                {
                    if (rS.chargeUp >= rS.maxChargeUp)
                    {
                       RailGunFire();
                       rS.railGunCharged = false;
                       rS.railgunCharging = false;
                    }
                    else
                    {
                        rS.chargeUp += (Time.deltaTime) * rS.chargeUpRate;
                        rS.railgunCharging = true;
                    }
                }
            }
        }
        
    }
    void RailGunFire()
    {
        //GameObject newBullet = (GameObject)Instantiate(rS.bullet.gameObject, rS.aimer.position, rS.aimer.rotation);
        //BulletStat bS = newBullet.GetComponent<BulletStat>();
        //bS.upgradedRed = rS.upgradeRed;
        //bS.amountOfPiercing = rS.peircingShots;
        //bS.destinToDest = rS.touchPosition;
        for(int i = 0; i < rS.bullet.Length; i++)
        {
            if(!rS.bullet[i].gameObject.activeSelf)
            {
                BulletStat bS = rS.bullet[i].GetComponent<BulletStat>();
                rS.bullet[i].transform.parent = null;
                rS.bullet[i].position = rS.aimer.position;
                rS.bullet[i].rotation = rS.aimer.rotation;
                bS.damage = rS.damage;
                bS.upgradedRed = rS.upgradeRed;
                bS.amountOfPiercing = rS.peircingShots;
                bS.destinToDest = rS.touchPosition;
                rS.bullet[i].gameObject.SetActive(true);
                bS.fromTurretToReturn = this.transform;
                GetComponent<AudioSource>().Play();
                rS.railgunCharging = false;
                rS.firedRed = true;
                break;
            }
        }
        
    }
    void ResetAIM()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}

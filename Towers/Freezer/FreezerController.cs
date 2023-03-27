using UnityEngine;
using UnityEngine.UI;
public class FreezerController : MonoBehaviour
{
    public FreezerStats fS;
    public GameObject freezerObj;
    SpriteRenderer sR;
    float delayShot = 0.1f , maxDelayShot = 0.1f;
    BulletStat bS;
    // Start is called before the first frame update
    void Start()
    {
        fS.GetComponent<FreezerStats>();
        sR = GetComponent<SpriteRenderer>();
        bS = fS.bullet.GetComponent<BulletStat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fS.tI.isActive)
        {
            fS.tI.weaponBar.weaponBars[1].value = fS.chargeUp;
            fS.tI.weaponBar.weaponBars[2].value = fS.energyLevel;
            if (fS.tI.tM.pTM.isTouch[1] && fS.tI.secondAimer)
            {
                fS.tI.touchSelected = 1;

            }
            else
            {
                fS.tI.touchSelected = 0;
            }
            if (fS.tI.disabled)
            {
                if (sR.color == Color.blue)
                {
                    sR.color = Color.gray;
                    if (fS.bullet.gameObject.activeSelf)
                    {
                        //Destroy(freezerObj);
                        fS.bullet.gameObject.SetActive(false);
                        GetComponent<AudioSource>().Stop();
                        fS.impulseCharged = false;
                        fS.impulseCharging = false;
                        fS.chargeUp = 0;
                    }
                }
            }
            else
            {
                if (sR.color == Color.gray)
                {
                    sR.color = Color.blue;
                }
            }
            if (fS.tA.target != Vector2.zero || fS.tI.tM.debugMode)
            {
                if (!fS.overheated && !fS.tI.disabled)
                {
                    fS.tI.touchPosition = fS.tA.touchPosition;
                    if (!fS.impulseCharged)
                    {
                        ChargingWeapon();
                    }
                    else
                    {
                        if (fS.energyLevel < fS.maxEnergyLevel && !fS.overheated)
                        {
                            fS.energyLevel += Time.deltaTime * fS.energyLevelRate;
                            FireFreezer();
                        }
                        else
                        {
                            fS.overheated = true;
                        }
                    }
                }

            }
            else
            {
                CoolDown();
            }
            if (!fS.tI.tM.activeTouch[fS.tI.touchSelected] && !fS.tI.tM.debugMode || fS.overheated)
            {
                CoolDown();
            }
        }
    }

    void CoolDown()
    {
        if (fS.overheated)
        {
            GetComponent<AudioSource>().Stop();
            if(fS.bullet.gameObject.activeSelf)
            {
                fS.bullet.gameObject.SetActive(false);
            }
            if (fS.energyLevel > 0)
            {
                fS.energyLevel -= Time.deltaTime * fS.overCoolDownRate;
                fS.chargeUp -= Time.deltaTime * fS.overCoolDownRate;
            }
            else
            {
                fS.energyLevel = 0;
                fS.chargeUp = 0;
                fS.overheated = false;
                fS.impulseCharging = false;
            }

        }
        else
        {
            if (fS.bullet.gameObject.activeSelf)
            {
                fS.bullet.gameObject.SetActive(false);
                GetComponent<AudioSource>().Stop();
            }
            if (fS.energyLevel > 0)
            {
                fS.energyLevel -= Time.deltaTime * fS.coolDownRate;
                fS.chargeUp -= Time.deltaTime * fS.coolDownRate;
                GetComponent<AudioSource>().Stop();
            }
            else
            {
                fS.energyLevel = 0;
                fS.chargeUp = 0;
                fS.impulseCharging = false;
            }

        }
        fS.impulseCharged = false;
        
    }
    /*
    void RechargeWeapons()
    {
        if (fS.firedBlue)
        {
            if (fS.chargeUp < fS.maxChargeUp)
            {  
                if (freezerObj != null)
                {
                    fS.bullet.GetComponent<BulletStat>().destinToDest = fS.tI.touchPosition;
                    //freezerObj.GetComponent<BulletStat>().destinToDest = fS.tI.touchPosition;
                    
                }

                fS.chargeUp += Time.deltaTime * fS.chargeUpRate;
            }
            else
            {
                fS.firedBlue = false;
                
                if (freezerObj != null)
                {
                    Destroy(freezerObj);
                }
                
                if (fS.bullet.gameObject.activeSelf)
                {
                    fS.bullet.gameObject.SetActive(false);
                }
                fS.chargeUp = 0;
            }
        }
    }
    */
    //When The Weapon Is Switched To Impulse
    //Weapon damage/radius is deterimed by the charge.
    //Once at max charge it will automatically fire.
    void ChargingWeapon()
    {
        if (fS.tI.tM.activeTouch[0] || fS.tI.tM.activeTouch[1] || fS.tI.tM.debugMode)
        {
            if (fS.tI.tM.activeTouch[fS.tI.touchSelected] || fS.tI.tM.debugMode)
            {
                if (fS.tI.disabledTime <= 0)
                {
                    //If Charge Rate Reachs Capacity To Automatically Fire
                    if (fS.chargeUp >= fS.maxChargeUp)
                    {
                        fS.impulseCharged = true;
                        fS.impulseCharging = false;
                    }
                    else
                    {
                        fS.chargeUp += (Time.deltaTime * fS.chargeUpRate);
                        fS.impulseCharging = true;
                    }

                }
            }
        }
        /*//This is premptive Fire When Letting Go.
        else if (fS.impulseCharging)
        {
            float reduction = fS.chargeUp / fS.maxChargeUp;
            GameObject newBullet = (GameObject)Instantiate(fS.bullet.gameObject, fS.aimer.position, fS.aimer.rotation);
            BulletStat bS = newBullet.GetComponent<BulletStat>();
            bS.damage = Mathf.RoundToInt(fS.damage * reduction);
            bS.slowTime = fS.slowTime * reduction;
            bS.slowPercentage = fS.slowPercentage * reduction;
            bS.destinToDest = fS.tI.touchPosition;
            GetComponent<AudioSource>().Play();
            fS.chargeUp = 0;
            fS.firedBlue = true;
            fS.impulseCharging = false;
        }
        */
    }

    void FireFreezer()
    {
        if (fS.tI.tM.activeTouch[0] || fS.tI.tM.activeTouch[1] || fS.tI.tM.debugMode)
        {
            if (fS.tI.tM.activeTouch[fS.tI.touchSelected] || fS.tI.tM.debugMode)
            {
                if (!fS.bullet.gameObject.activeSelf)
                {
                    bS.destinToDest = fS.tI.touchPosition;
                    fS.bullet.gameObject.SetActive(true);
                    fS.bullet.transform.position = fS.aimer.position;
                    fS.bullet.transform.rotation = fS.aimer.rotation;
                    bS.damage = fS.damage;
                    bS.slowTime = fS.slowTime;
                    bS.destinToDest = fS.tI.touchPosition;
                    bS.slowPercentage = fS.slowPercentage;
                    fS.bullet.GetComponent<FreezeLaserController>().expansionRate = fS.expansionRate;
                    bS.currentTouch = fS.tI.touchSelected;
                    bS.currentColor = fS.tI.selectedColor;
                    fS.bullet.GetComponent<FreezeLaserController>().isBlueUpgraded = fS.upgradeBlue;
                    GetComponent<AudioSource>().Play();


                }
                else
                {
                    bS.destinToDest = fS.tI.touchPosition;

                }
                /*
                if (freezerObj == null)
                {
                    GameObject newBullet = (GameObject)Instantiate(fS.bullet.gameObject, fS.aimer.position, fS.aimer.rotation);
                    freezerObj = newBullet;
                    freezerObj.transform.parent = fS.aimer.transform;
                    BulletStat bS = newBullet.GetComponent<BulletStat>();
                    bS.damage = fS.damage;
                    bS.slowTime = fS.slowTime;
                    bS.destinToDest = fS.tI.touchPosition;
                    bS.slowPercentage = fS.slowPercentage;
                    newBullet.GetComponent<FreezeLaserController>().expansionRate = fS.expansionRate;
                    bS.currentTouch = fS.tI.touchSelected;
                    bS.currentColor = fS.tI.selectedColor;
                    bS.destinToDest = fS.tI.touchPosition;
                    newBullet.GetComponent<FreezeLaserController>().isBlueUpgraded = fS.upgradeBlue;
                    GetComponent<AudioSource>().Play();
                    

                }
                else
                {
                    freezerObj.GetComponent<BulletStat>().destinToDest = fS.tI.touchPosition;

                }
                */
            }
        }
    }
    void ResetAIM()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}

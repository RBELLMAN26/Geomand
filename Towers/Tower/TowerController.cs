using UnityEngine;

public class TowerController : MonoBehaviour
{
    public TowerInfo tI;
    bool isReset;
    float constHit = 0.25f, maxConstHit = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        tI = transform.GetComponent<TowerInfo>();
        tI.tM = transform.root.GetComponent<TowerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tI.isActive)
        {
            if (tI.tM.pTM.isTouch[1] && tI.secondAimer)
            {
                tI.touchSelected = 1;

            }
            else
            {
                tI.touchSelected = 0;
            }
            AutoAim();
        }

    }
    
    void AutoAim()
    {
        //Leave Alone,  For direction of autoaimer
        if (tI.tM.activeTouch[tI.touchSelected])
        { 
            tI.selectedPOS = tI.tM.pTM.TouchPOS[tI.touchSelected];
        }
    }
    
    //This script will cycle through the towers
    public void ChangeTower(int prevColor,int newColor)
    {
        if(!tI.disabled)
        {
            tI.turrets[prevColor].gameObject.SetActive(false);
            tI.turrets[newColor].gameObject.SetActive(true);
            tI.selectedColor = newColor;
            tI.weaponBar.ChangeBar(newColor);
        }
        
    }
    public void LoseHealth(int hitDamage)
    {
        int newHealth = tI.currentHealth - hitDamage;
        //print(newHealth);
        if (newHealth > 0)
        {
            tI.currentHealth = newHealth;
        }
        else if (newHealth <= 0)
        {
            tI.isActive = false;
            OnDeath();
            //tI.tM.DestroyedTurret(tI.turretNum);
            //gameObject.SetActive(false);
        }
    }
    public void UpgradeTurret(Upgrades upObject)
    {
        int color = upObject.colorID;
        int select = upObject.upgradeID;
        float amount = upObject.upgradeAmount;
        tI.tM.pum.CostOfTurrets[tI.turretNum] += Mathf.RoundToInt(upObject.scoreCost / 4);
        //If 0 - Health, 1 - FireRate, 2 - Damage, 3 - Projectile Speed, 4 - Projectile Size, 5 - MISC
        switch (select)
        {
            case 0: //Health Upgrade
                {
                    HealthUpgrade((int)amount);
                    print("Health Up To " + amount);
                    break;
                }
            case 1: //FireRate
                {
                    FireRateUpgrade(color, amount);
                    //print("Speed Up To " + amount);
                    break;
                }
            case 2: //Damage
                {
                    DamageUpgrade(color, amount);
                    //print("Damage Up To " + amount);
                    break;
                }
            case 3: //Freeze Rate (How long the enemy will be frozen for)
                {
                    //RechargeUpgrade(color, amount);
                    SlowUpgrade(color, amount);
                    break;
                }
            case 4: //Overheat Rate (How Fast Weapon Overheats)
                {
                    OverheatUpgrade(color, amount);
                    break;
                }
            case 5: //MISC
                {
                    MISCUpgrade((int)amount);
                    break;
                }
        }
        if(FindObjectOfType<GameManager>().endlessMode)
        {
            //Increase Value Of Upgrade
            upObject.upgradeAmount += upObject.upgradeAmount / 2;
        }
        else
        {
            upObject.transform.parent.GetComponent<UpgradeManager>().ApplyUpgrade(upObject.orderID);
        }
        
    }
    void HealthUpgrade(int amount)
    {
        tI.maxHealth += amount;
        tI.currentHealth += amount;
    }
    void FireRateUpgrade(int color, float amount)
    {
        switch (color)
        {
            case 0: //Green
                {
                    tI.turrets[color].GetComponent<LaserBulletStats>().chargeRate += amount;
                    break;
                }
            case 1: //Blue
                {
                    tI.turrets[color].GetComponent<FreezerStats>().chargeUpRate += amount;
                    break;
                }
            case 2: //Red
                {
                    tI.turrets[color].GetComponent<RailGunStats>().chargeUpRate += amount;
                    break;
                }
            case 3: //All
                {
                    tI.turrets[0].GetComponent<LaserBulletStats>().chargeRate += amount;
                    tI.turrets[1].GetComponent<FreezerStats>().chargeUpRate += amount;
                    tI.turrets[2].GetComponent<RailGunStats>().chargeUpRate += amount;
                    break;
                }
        }
    }
    void DamageUpgrade(int color, float amount)
    {
        switch (color)
        {
            case 0: //Green
                {
                    tI.turrets[color].GetComponent<LaserBulletStats>().damage += (int)amount;
                    break;
                }
            case 1: //Blue
                {
                    tI.turrets[color].GetComponent<FreezerStats>().damage += (int)amount;
                    break;
                }
            case 2: //Red
                {
                    tI.turrets[color].GetComponent<RailGunStats>().damage += (int)amount;
                    break;
                }
            case 3: //All
                {
                    tI.turrets[0].GetComponent<LaserBulletStats>().damage += (int)amount;
                    tI.turrets[1].GetComponent<FreezerStats>().damage += (int)amount;
                    tI.turrets[2].GetComponent<RailGunStats>().damage += (int)amount;
                    break;
                }
        }

    }
    void SlowUpgrade(int color, float amount)
    {
        switch (color)
        {
            case 0: //Green
                {
                    //tI.turrets[color].GetComponent<LaserBulletStats>().damage += (int)amount;
                    break;
                }
            case 1: //Blue
                {
                    tI.turrets[color].GetComponent<FreezerStats>().slowPercentage += (int)amount;
                    break;
                }
            case 2: //Red
                {
                    //tI.turrets[color].GetComponent<RailGunStats>().damage += (int)amount;
                    break;
                }
            case 3: //All
                {
                    //tI.turrets[0].GetComponent<LaserBulletStats>().damage += (int)amount;
                   // tI.turrets[1].GetComponent<FreezerStats>().damage += (int)amount;
                    //tI.turrets[2].GetComponent<RailGunStats>().damage += (int)amount;
                    break;
                }
        }
    }
    void RechargeUpgrade(int color, float amount)
    {
        switch (color)
        {
            case 0: //Green
                {
                    tI.turrets[color].GetComponent<LaserBulletStats>().damage += (int)amount;
                    break;
                }
            case 1: //Blue
                {
                    tI.turrets[color].GetComponent<FreezerStats>().damage += (int)amount;
                    break;
                }
            case 2: //Red
                {
                    tI.turrets[color].GetComponent<RailGunStats>().damage += (int)amount;
                    break;
                }
            case 3: //All
                {
                    tI.turrets[0].GetComponent<LaserBulletStats>().damage += (int)amount;
                    tI.turrets[1].GetComponent<FreezerStats>().damage += (int)amount;
                    tI.turrets[2].GetComponent<RailGunStats>().damage += (int)amount;
                    break;
                }
        }

    }
    void OverheatUpgrade(int color, float amount)
    {
        tI.turrets[color].GetComponent<FreezerStats>().maxEnergyLevel += (int)amount;
    }
    //This is for unlocking blue, red, and upgrades for green blue and red. 
    void MISCUpgrade(int ID)
    {
        switch (ID)
        {
            case 0: //Unlock Blue Turret
                {
                    tI.blueActive = true;
                    //print("Blue Unlocked");
                    break;
                }
            case 1: //Unlock Red Turret
                {
                    tI.redActive = true;
                    //print("Red Unlocked");
                    break;
                }
            case 2: //Upgrade Green - Rapid Charged Shots
                {
                    tI.turrets[0].GetComponent<LaserBulletStats>().upgradeGreen = true;
                    //tI.upgradeGreen = true;
                    break;
                }
            case 3: //Upgrade Blue - Bomb
                {
                    tI.turrets[1].GetComponent<FreezerStats>().upgradeBlue = true;
                    //tI.upgradeBlue = true;
                    break;
                }
            case 4: //Upgrade Red - Piercing
                {
                    tI.turrets[2].GetComponent<RailGunStats>().upgradeRed = true;
                    tI.turrets[2].GetComponent<RailGunStats>().peircingShots = 2;
                    //tI.upgradeRed = true;
                    break;
                }
            case 5: //Green Extra Charges
                {
                    tI.turrets[0].GetComponent<LaserBulletStats>().maxChargedShots = 6;
                    break;
                }
            case 6: //Blue Stop
                {
                    tI.turrets[1].GetComponent<FreezerStats>().slowPercentage = 100;
                    break;
                }
            case 7: //Red Three Peircings
                {
                    tI.turrets[2].GetComponent<RailGunStats>().peircingShots = 5;
                    break;
                }
            case 8: //Blue Expansion Rate
                {
                    tI.turrets[1].GetComponent<FreezerStats>().expansionRate = 1.5f;
                    break;
                }
        }
    }

    public void DisableTurret(float timeOfDisabled)
    {
        tI.disabledTime = timeOfDisabled;
        DisableSound();
    }
    public void Revive()
    {
        tI.currentHealth = tI.maxHealth;
    }
    public void OnDeath()
    {

        //touchSelected = 0;
        //GetComponent<SpriteRenderer>().color = Color.blue;
        tI.currentColor = -1;
        tI.currentHealth = tI.maxHealth;
        tI.tM.DestroyedTurret(tI.turretNum);
        gameObject.SetActive(false);
        tI.tM.UpdateTurrets();
        DisableSound();
        
    }
    public void DisableSound()
    {
        tI.turrets[tI.selectedColor].GetComponent<AudioSource>().Stop();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Disabler>())
            {
                DisableTurret(collision.GetComponent<Disabler>().disableTime);
                LoseHealth(collision.GetComponent<Disabler>().eI.damageDealtToTowers);
                Destroy(collision.gameObject);
            }
            else if (collision.GetComponent<Bomber>())
            {
                LoseHealth(collision.GetComponent<Bomber>().eI.damageDealtToTowers);
                collision.GetComponent<Bomber>().OnDeath();
            }
            else if (collision.GetComponent<DeathWave>())
            {
                LoseHealth(collision.GetComponent<DeathWave>().damageToTowers);
                DisableTurret(collision.GetComponent<DeathWave>().disableTime);
            }
            else if (collision.GetComponent<RocketController>())
            {
                LoseHealth(collision.GetComponent<RocketStats>().damageDealtToTowers);
                Destroy(collision.gameObject);
            }
            else if(collision.GetComponent<Sun>())
            {
                tI.isActive = false;
                OnDeath();
                Destroy(collision.gameObject);
            }
            else
            {
                LoseHealth(collision.GetComponent<EnemyInfo>().damageDealtToTowers);
                Destroy(collision.gameObject);
            }
        }
        if (collision.CompareTag("Regen"))
        {
            float reg = collision.GetComponent<RegenMini>().healthToRegen;

            if (tI.currentHealth + reg > tI.currentHealth)
            {
                //float newReg = (tI.currentHealth + reg) - tI.currentHealth;
                tI.currentHealth = tI.maxHealth;
            }
            else
            {
                tI.currentHealth += collision.GetComponent<RegenMini>().healthToRegen;
            }

            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("EnemyBullet"))
        {
            LoseHealth(collision.GetComponent<EnemyBulletStats>().damage);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("EnemyLaser"))
        {
            LoseHealth(collision.GetComponent<EnemyBulletStats>().damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Time.timeScale > 0)
        {
            if (collision.CompareTag("EnemyLaser"))
            {
                if (constHit > 0)
                {
                    constHit -= Time.deltaTime;
                }
                else
                {
                    LoseHealth(collision.GetComponent<EnemyBulletStats>().damage);
                    constHit = maxConstHit;
                }

            }
        }
    }
}

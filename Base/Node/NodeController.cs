using UnityEngine;
using UnityEngine.PlayerLoop;
using TMPro;

public class NodeController : MonoBehaviour
{
    public EnemyGenerationManager eGM;
    public NodeInfo nI;
    public GameObject healthRegen;
    public GameObject waveWipe;
    public GameObject shield;
    public GameObject PauseEnemiesIcon;
    public GameObject MultiScoreIcon;

    [SerializeField] Transform abilitiesParent, upgradedParent;
    public GameObject[] upgradedAbilities;
    public AbilityStats[] aSS;
    public TowerInfo[] tI;
    public GameObject[] icons;
    public bool[] isUpgraded;
    float constHit = 0.1f, maxConstHit = 0.1f;
    private void Start()
    {
        eGM = FindObjectOfType<EnemyGenerationManager>();
        nI = GetComponent<NodeInfo>();
    }
    void Update()
    {
        CoolDown();
    }
    void CoolDown()
    {
        if (nI.rechargeHealth > 0)
        {
            nI.rechargeHealth -= Time.deltaTime * nI.rateHealth;
            if(aSS[0].countDownText.gameObject.activeSelf)
            {
                aSS[0].countDownText.text = string.Format("{0}%" , Mathf.Round(((nI.maxRechargeHealth - nI.rechargeHealth) / nI.maxRechargeHealth) * 100));
            }
            else
            {
                aSS[0].countDownText.gameObject.SetActive(true);
                aSS[0].recharging = true;
                icons[0].SetActive(false);
            }
        }
        else
        {
            if (aSS[0].countDownText.gameObject.activeSelf)
            {
                aSS[0].countDownText.gameObject.SetActive(false);
                icons[0].SetActive(true);
                aSS[0].recharging = false;
            }
        }
        if (nI.rechargeWipe > 0)
        {
            nI.rechargeWipe -= Time.deltaTime * nI.rateWipe;
            if (aSS[1].countDownText.gameObject.activeSelf)
            {
                aSS[1].countDownText.text = string.Format("{0}%", Mathf.Round(((nI.maxRechargeWipe - nI.rechargeWipe ) / nI.maxRechargeWipe) * 100));
            }
            else
            {
                aSS[1].countDownText.gameObject.SetActive(true);
                icons[1].SetActive(false);
                aSS[1].recharging = true;
            }
        }
        else
        {
            if (aSS[1].countDownText.gameObject.activeSelf)
            {
                aSS[1].countDownText.gameObject.SetActive(false);
                icons[1].SetActive(true);
                aSS[1].recharging = false;
            }
        }
        if (nI.rechargeShield > 0)
        {
            nI.rechargeShield -= Time.deltaTime * nI.rateShield;
            if (aSS[2].countDownText.gameObject.activeSelf)
            {
                aSS[2].countDownText.text = string.Format("{0}%", Mathf.Round(((nI.maxRechargeShield - nI.rechargeShield) / nI.maxRechargeShield) * 100));
            }
            else
            {
                aSS[2].countDownText.gameObject.SetActive(true);
                icons[2].SetActive(false);
                aSS[2].recharging = true;
            }
        }
        else
        {
            if (aSS[2].countDownText.gameObject.activeSelf)
            {
                aSS[2].countDownText.gameObject.SetActive(false);
                icons[2].SetActive(true);
                aSS[2].recharging = false;
            }
        }
        if (nI.rechargePause > 0)
        {
            nI.rechargePause -= Time.deltaTime * nI.ratePause;
            if (aSS[3].countDownText.gameObject.activeSelf)
            {
                aSS[3].countDownText.text = string.Format("{0}%", Mathf.Round(((nI.maxRechargePause - nI.rechargePause) / nI.maxRechargePause) * 100));
            }
            else
            {
                aSS[3].countDownText.gameObject.SetActive(true);
                icons[3].SetActive(false);
                aSS[3].recharging = true;
            }
        }
        else
        {
            if (aSS[3].countDownText.gameObject.activeSelf)
            {
                aSS[3].countDownText.gameObject.SetActive(false);
                icons[3].SetActive(true);
                aSS[3].recharging = false;
            }
        }
        if (nI.rechargeMulti > 0)
        {
            nI.rechargeMulti -= Time.deltaTime * nI.rateMulti;
            if (aSS[4].countDownText.gameObject.activeSelf)
            {
                aSS[4].countDownText.text = string.Format("{0}%", Mathf.Round(((nI.maxRechargeMulti - nI.rechargeMulti) / nI.maxRechargeMulti) * 100));
            }
            else
            {
                aSS[4].countDownText.gameObject.SetActive(true);
                icons[4].SetActive(false);
                aSS[4].recharging = true;
            }
        }
        else
        {
            if (aSS[4].countDownText.gameObject.activeSelf)
            {
                aSS[4].countDownText.gameObject.SetActive(false);
                icons[4].SetActive(true);
                aSS[4].recharging = false;
            }
        }
    }
    public void ActivateAbility(int abilityNum)
    {
        if (nI.disabledTime <= 0)
        {
            switch (abilityNum)
            {
                case 0:
                    {
                        if (nI.rechargeHealth <= 0)
                        {
                            HealthRegen();
                            if(!isUpgraded[0])
                            {
                                nI.maxRechargeHealth += 60;
                            }
                            else
                            {
                                nI.maxRechargeHealth += 90;
                            }
                            
                            nI.rechargeHealth = nI.maxRechargeHealth;
                        }
                        
                        break;
                    }
                case 1:
                    {
                        if(nI.rechargeWipe <= 0)
                        {
                            WipeEnemies();
                            if (!isUpgraded[1])
                            {
                                nI.maxRechargeWipe += 60;
                            }
                            else
                            {
                                nI.maxRechargeWipe += 90;
                            }
                            nI.rechargeWipe = nI.maxRechargeWipe;
                            if (aSS[1].countDownText.gameObject.activeSelf)
                            {
                                aSS[1].countDownText.gameObject.SetActive(false);
                            }
                        }
                        
                        break;
                    }
                case 2:
                    {
                        if(nI.rechargeShield <= 0)
                        {
                            ShieldUp();
                            if (!isUpgraded[2])
                            {
                                nI.maxRechargeShield += 60;
                            }
                            else
                            {
                                nI.maxRechargeShield += 90;
                            }
                            nI.rechargeShield = nI.maxRechargeShield;
                            if (aSS[2].countDownText.gameObject.activeSelf)
                            {
                                aSS[2].countDownText.gameObject.SetActive(false);
                            }
                        }
                        
                        break;
                    }
                case 3:
                    {
                        if(nI.rechargePause <= 0)
                        {
                            PauseEnemies();
                            nI.maxRechargePause += 60;
                            nI.rechargePause = nI.maxRechargePause;
                            if (aSS[3].countDownText.gameObject.activeSelf)
                            {
                                aSS[3].countDownText.gameObject.SetActive(false);
                            }
                        }
                        
                        break;

                    }
                case 4:
                    {
                        if(nI.rechargeMulti <= 0)
                        {
                            ScoreMultiplier();
                            nI.maxRechargeMulti += 60;
                            nI.rechargeMulti = nI.maxRechargeMulti;
                            if (aSS[4].countDownText.gameObject.activeSelf)
                            {
                                aSS[4].countDownText.gameObject.SetActive(false);
                            }
                        }
                       
                        break;

                    }
            }
        }

    }
    //Heals all turrets including base with certain amount of health
    public void HealthRegen()
    {
        if (!isUpgraded[0])
        {
            //GameObject newHealthRegen = (GameObject)Instantiate(healthRegen, transform.position + (Vector3.up * 5), transform.rotation);
            healthRegen.GetComponent<HealthRegen>().healthToRegen = aSS[0].hp;
            healthRegen.GetComponent<HealthRegen>().duration = aSS[0].duration;
            healthRegen.GetComponent<HealthRegen>().parentNode = abilitiesParent;
            healthRegen.SetActive(true);
            healthRegen.transform.parent = null;
        }
        else
        {
            //GameObject newHealthRegen = (GameObject)Instantiate(upgradedAbilities[0], transform.position + (Vector3.up * 5), transform.rotation);
            upgradedAbilities[0].SetActive(true);
            upgradedAbilities[0].transform.parent = null;
            upgradedAbilities[0].GetComponent<HealthRegenManager>().parentNode = upgradedParent;
            //upgradedAbilities[0]
            //newHealthRegen.GetComponent<HealthRegen>().healthToRegen = aSS[0].hp;
            //newHealthRegen.GetComponent<HealthRegen>().duration = aSS[0].duration;
        }

    }
    //Launches a wall wiping all enemies
    public void WipeEnemies()
    {
        if (!isUpgraded[1])
        {
            //GameObject newWaveWipe = (GameObject)Instantiate(waveWipe, transform.position, transform.rotation);
            waveWipe.transform.position = transform.position;
            waveWipe.GetComponent<WaveWipe>().damage = aSS[1].damage;
            waveWipe.GetComponent<WaveWipe>().speed = aSS[1].speed;
            waveWipe.GetComponent<WaveWipe>().parentNode = abilitiesParent;
            waveWipe.SetActive(true);
            waveWipe.transform.parent = null;
        }
        else
        {
            //GameObject newWaveWipe = (GameObject)Instantiate(upgradedAbilities[1], transform.position, transform.rotation);
            upgradedAbilities[1].transform.position = transform.position;
            upgradedAbilities[1].SetActive(true);
            upgradedAbilities[1].transform.parent = null;
            upgradedAbilities[1].GetComponent<WaveWipe>().parentNode = upgradedParent;
            //newWaveWipe.GetComponent<WaveWipe>().damage = aSS[1].damage;
            //newWaveWipe.GetComponent<WaveWipe>().speed = aSS[1].speed;
        }


    }
    //Shield With Degrading Temp Health
    public void ShieldUp()
    {
        if (!isUpgraded[2])
        {
            //GameObject newShield = (GameObject)Instantiate(shield, transform.position + (transform.up * 1), transform.rotation);
            //shield.transform.localPosition = transform.up * 1;
            shield.GetComponent<ShieldStats>().shieldStrength = aSS[2].hp;
            shield.GetComponent<ShieldStats>().duration = aSS[2].duration;
            shield.GetComponent<ShieldStats>().parentNode = abilitiesParent;
            shield.gameObject.SetActive(true);
            shield.transform.parent = null;
        }
        else
        {
            if(upgradedAbilities[2].activeSelf)
            {
                upgradedAbilities[2].GetComponent<ShieldManager>().ReviveShield();
            }
            else
            {
                //GameObject newShield = (GameObject)Instantiate(upgradedAbilities[2], transform.position + (transform.up * 1), transform.rotation);
                upgradedAbilities[2].SetActive(true);
                upgradedAbilities[2].transform.parent = null;
                upgradedAbilities[2].GetComponent<ShieldManager>().ReviveShield();
                upgradedAbilities[2].GetComponent<ShieldManager>().parentNode = upgradedParent;
            }
            
            //newShield.GetComponent<ShieldStats>().shieldStrength = aSS[2].hp;
            //newShield.GetComponent<ShieldStats>().duration = aSS[2].duration;
        }

    }

    public void PauseEnemies()
    {
        //GameObject newPause = (GameObject)Instantiate(PauseEnemiesIcon, transform.position + (Vector3.up * 4), transform.rotation);
        PauseEnemiesIcon.SetActive(true);
        PauseEnemiesIcon.transform.parent = null;
        eGM.PauseEnemies(aSS[3].duration);
    }
    public void ScoreMultiplier()
    {
        //GameObject newMulti = (GameObject)Instantiate(MultiScoreIcon, transform.position + (Vector3.up * 3), transform.rotation);
        MultiScoreIcon.SetActive(true);
        MultiScoreIcon.transform.parent = null;
        eGM.ScoreMultiplier(2, 10);
        
    }
    public void LoseHealth(int hitDamage)
    {
        int newHealth = nI.currentHealth - hitDamage;
        print(newHealth);
        if (newHealth > 0)
        {
            nI.currentHealth = newHealth;
        }
        else if (newHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void UpgradeNode(NodeUpgrades upObject)
    {
        int select = upObject.upgradeID;
        int option = upObject.optionID;
        float amount = upObject.upgradeAmount;
        switch (select)
        {
            case 0://Health Regen
                {
                    switch (option)
                    {
                        //Regen HP
                        case 0:
                            {
                                aSS[select].hp += (int)amount;
                                break;
                            }
                        //Regen Duration
                        case 1:
                            {
                                aSS[select].duration += (int)amount;
                                break;
                            }
                        //Reduce Energy
                        case 2:
                            {
                                aSS[select].GetComponent<Abilities>().batteryCost -= 5;
                                break;
                            }
                        //Health Upgrade
                        case 3:
                            {
                                isUpgraded[0] = true;
                                nI.maxRechargeHealth += 150;
                                break;
                            }
                    }

                    break;
                }
            case 1://DeathWave
                {
                    switch (option)
                    {
                        case 0: //Damage
                            {

                                aSS[select].damage += (int)amount;

                                break;
                            }
                        case 1: //Speed Reduced
                            {

                                aSS[select].speed -= amount;
                                break;
                            }
                        //Reduce Energy
                        case 2:
                            {
                                aSS[select].GetComponent<Abilities>().batteryCost -= 5;
                                break;
                            }
                        case 3:
                            {
                                isUpgraded[1] = true;
                                nI.maxRechargeWipe += 150;
                                break;
                            }
                    }
                    break;
                }
            case 2://Shield
                {
                    switch (option)
                    {
                        //Shield Capacity
                        case 0:
                            {
                                aSS[select].hp += (int)amount;
                                break;
                            }
                        //Shield Duration
                        case 1:
                            {
                                aSS[select].duration += amount;
                                break;
                            }
                        //Reduce Energy
                        case 2:
                            {
                                aSS[select].GetComponent<Abilities>().batteryCost -= 5;
                                break;
                            }
                        //Upgrade
                        case 3:
                            {
                                isUpgraded[2] = true;
                                nI.maxRechargeShield += 150;
                                break;
                            }
                    }
                    break;
                }
            case 3: //Boosts Health (Node) , Damage Boost, ,FireRate Boost, Score Boost
                {
                    switch (option)
                    {
                        //Health (Node)
                        case 0:
                            {
                                nI.currentHealth += (int)upObject.upgradeAmount;
                                nI.maxHealth += (int)upObject.upgradeAmount;
                                break;
                            }
                        //Damage Boost
                        case 1:
                            {
                                for (int i = 0; i < tI.Length; i++)
                                {
                                    tI[i].turrets[0].GetComponent<LaserBulletStats>().damage += (int)upObject.upgradeAmount;
                                    tI[i].turrets[1].GetComponent<FreezerStats>().damage += (int)upObject.upgradeAmount;
                                    tI[i].turrets[2].GetComponent<RailGunStats>().damage += (int)upObject.upgradeAmount;
                                }
                                break;
                            }
                        //FireRate Boost
                        case 2:
                            {
                                for (int i = 0; i < tI.Length; i++)
                                {
                                    tI[i].turrets[0].GetComponent<LaserBulletStats>().chargeRate += (int)upObject.upgradeAmount;
                                    tI[i].turrets[1].GetComponent<FreezerStats>().chargeUpRate += (int)upObject.upgradeAmount;
                                    tI[i].turrets[2].GetComponent<RailGunStats>().chargeUpRate += (int)upObject.upgradeAmount;
                                }
                                break;
                            }
                        //Score Boost
                        case 3:
                            {
                                //eGM.scoreBoost += (int)upObject.upgradeAmount;
                                eGM.ScoreBoost((int)upObject.upgradeAmount);
                                break;
                            }
                    }
                    break;
                }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Disabler>())
            {
                LoseHealth(Mathf.CeilToInt(collision.GetComponent<EnemyInfo>().damageDealtToTowers / 2));
                nI.DisableNode(collision.GetComponent<Disabler>().disableTime);
                Destroy(collision.gameObject);
            }
            else if (collision.GetComponent<Bomber>())
            {
                collision.GetComponent<Bomber>().OnDeath();
            }
            else if (collision.GetComponent<DeathWave>())
            {
                LoseHealth(Mathf.CeilToInt(collision.GetComponent<DeathWave>().damageToTowers / 2));
                nI.DisableNode(collision.GetComponent<DeathWave>().disableTime);
            }
            else if (collision.GetComponent<RocketStats>())
            {
                LoseHealth(Mathf.CeilToInt(collision.GetComponent<RocketStats>().damageDealtToTowers / 2));
                Destroy(collision.gameObject);
            }
            else if(collision.GetComponent<Sun>())
            {
                gameObject.SetActive(false);
            }
            else
            {
                LoseHealth(Mathf.CeilToInt(collision.GetComponent<EnemyInfo>().damageDealtToTowers / 2));
                print("HIT");
                Destroy(collision.gameObject);
            }
        }
        if (collision.CompareTag("Regen"))
        {
            float reg = collision.GetComponent<RegenMini>().healthToRegen;

            if (nI.currentHealth + reg > nI.currentHealth)
            {
                //float newReg = (tI.currentHealth + reg) - tI.currentHealth;
                nI.currentHealth = nI.maxHealth;
            }
            else
            {
                nI.currentHealth += collision.GetComponent<RegenMini>().healthToRegen;
            }

            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("EnemyBullet"))
        {
            LoseHealth(Mathf.CeilToInt(collision.GetComponent<EnemyBulletStats>().damage / 2));
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("EnemyLaser"))
        {
            LoseHealth(Mathf.CeilToInt(collision.GetComponent<EnemyBulletStats>().damage / 2));
            //Destroy(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyLaser"))
        {
            
            //Destroy(collision.gameObject);
            if (constHit > 0)
            {
                constHit -= Time.deltaTime;
            }
            else
            {
                LoseHealth(Mathf.CeilToInt(collision.GetComponent<EnemyBulletStats>().damage / 2));
                constHit = maxConstHit;
            }
        }
    }
}

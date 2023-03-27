using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfo : MonoBehaviour
{
    public TowerManager tM;
    // public Transform healthBar;
    public TextMeshPro healthText;
    //public Transform[] bullet;
    public Transform[] turrets;
    public Vector2 selectedPOS, touchPosition;
    public int turretNum;
    public int maxHealth, currentHealth;
    public float disabledTime;
    public bool isActive = true, disabled = false, secondAimer = false;
    public bool blueActive, redActive; //Able to use other turrets
    //public bool upgradeGreen, upgradeBlue, upgradeRed; //Able To Use UpgradeVersion Of Turret
    public int selectedColor = 0;
    public int touchSelected;  //Can Either be First or Second Touch or 0 or 1.
    public int currentColor = 0; //0 Is Blue , 1 Is Green , 2 is Grey
    public WeaponBar weaponBar;

    //Red Example is after charge shot was fired
    //public float maxGreenFireRate = 1, maxBlueFireRate = 2.5f, maxRedFireRate = 4;
    //ProjectileSpeed
    //public float greenProjectileSpeed = 8, blueProjectileSpeed = 8, redProjectileSpeed = 8;
    //Damage For Each Weapon
    //public int greenDamage = 1, blueDamage = 5, redDamage = 10;
    //public float slowTime, slowPercentage;
    //public bool laserCharging, impulseCharging, railgunCharging;
    //Green Stats
    //public int chargedShots = 0, maxChargedShots = 3;

    //Blue Stats
    //public float impulseChargeRate, impulseChargeCapacity = 4;

    //Red Stats
    //public float railgunChargeRate, railgunChargeCapacity = 5;

    private void OnEnable()
    {
        //tM = GetComponentInParent<TowerManager>();
        currentHealth = maxHealth;
        //healthBar = transform.GetChild(0);
    }
    // Update is called once per frame
    void Update()
    {
        //healthText.text = currentHealth.ToString();
        //float healthpercentage = (float)currentHealth / (float)maxHealth * 1.0f;
        //print (healthpercentage);
        // healthBar.localScale = new Vector3(healthBar.localScale.x, healthpercentage);
        if (disabledTime <= 0)
        {
            disabled = false;
            if (selectedColor != currentColor || currentColor == 3)
            {
                if (selectedColor == 0)
                {
                    //GetComponent<SpriteRenderer>().color = Color.green;
                    currentColor = 0;
                }
                else if (selectedColor == 1)
                {
                    //GetComponent<SpriteRenderer>().color = Color.blue;
                    currentColor = 1;
                }
                else if (selectedColor == 2)
                {
                    //GetComponent<SpriteRenderer>().color = Color.red;
                    currentColor = 2;
                }
            }

        }
        else
        {
            if (!disabled)
            {
                //GetComponent<SpriteRenderer>().color = Color.grey;
                //currentColor = 3;
                disabled = true;
            }
            disabledTime -= Time.deltaTime;
        }


    }


}

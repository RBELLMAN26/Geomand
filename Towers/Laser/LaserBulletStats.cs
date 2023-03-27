using UnityEngine;

public class LaserBulletStats : MonoBehaviour
{
    public TowerInfo tI;
    public TowerController tC;
    public Transform[] bullet;
    public Transform bulletHolder;
    public Transform aimer;
    public TurretAimer tA; 
    public bool upgradeGreen; //Able To Use UpgradeVersion Of Turret
    public Vector2 selectedPOS, touchPosition;
    //Red Example is after charge shot was fired
    public float chargeLevel = 0, chargeRate = 75, maxCharge = 100;
    //ProjectileSpeed
    public float projectileSpeed = 8;
    //Damage For Each Weapon
    public int damage = 1;

    public bool greenFired;
    public bool isReset;
    //Green Stats
    public int chargedShots = 0, maxChargedShots = 3;

    public float delaySpread = 0.1f, maxDelaySpread = 0.1f;
    
    float constHit = 0.25f, maxConstHit = 0.25f;
}

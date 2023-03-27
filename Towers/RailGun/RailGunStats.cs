using UnityEngine;

public class RailGunStats : MonoBehaviour
{
    public TowerInfo tI;
    public TowerController tC;
    public Transform[] bullet;
    public Transform aimer;
    public TurretAimer tA;
    public bool upgradeRed; //Able To Use UpgradeVersion Of Turret
    public Vector2 selectedPOS, touchPosition;

    //Red Example is after charge shot was fired
    public float chargeUp, maxChargeUp, chargeUpRate; //Time When Weapon is able to fire
    public float coolDownRate; //Starts at maxChargeUp and cools down
    //ProjectileSpeed
    public float projectileSpeed = 8;
    public int peircingShots = 0;
    //Damage For Each Weapon
    public int damage = 1;

    public bool railgunCharging, firedRed, isReset;
    public bool railGunCharged;
    float constHit = 0.25f, maxConstHit = 0.25f;
 
}

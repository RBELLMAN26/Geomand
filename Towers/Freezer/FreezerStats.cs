using UnityEngine;

public class FreezerStats : MonoBehaviour
{
    public TowerInfo tI;
    public TowerController tC;
    public Transform bullet;
    public Transform aimer;
    public TurretAimer tA;
    public bool upgradeBlue; //Able To Use UpgradeVersion Of Turret

    public float chargeUp, maxChargeUp, chargeUpRate; //Time When Weapon is able to fire
    public float energyLevel, maxEnergyLevel, energyLevelRate; //Starts at zero, if energyLevel exceeds to maxEnergyLevel, weapon takes longer to cooldown
    public float coolDownRate, overCoolDownRate;
    public float slowTime, slowPercentage;
    public float freezeRadias, freezeSizeSpeed; //This creates a small explosion of where the bullet ends.
    public float expansionRate = 1;
    //ProjectileSpeed
    public float projectileSpeed = 8;
    //Damage For Each Weapon
    public int damage = 1;

    float constHit = 0.25f, maxConstHit = 0.25f;

    public bool impulseCharging, impulseCharged, overheated;
    public bool isReset;
    public bool firedBlue;
}

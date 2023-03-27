using TMPro;
using UnityEngine;

public class NodeInfo : MonoBehaviour
{
    public TowerInfo[] tIs;
    public int energy;
    public EnergyStats energyBar;
    public TextMeshPro healthText;
    public int currentHealth, maxHealth;
    public float disabledTime;
    public float rechargeHealth, rechargeWipe, rechargeShield, rechargePause, rechargeMulti;
    public float maxRechargeHealth = 100, maxRechargeWipe = 100, maxRechargeShield = 100, maxRechargePause = 100, maxRechargeMulti = 100;
    public float rateHealth = 30, rateWipe = 30, rateShield = 30, ratePause = 30, rateMulti = 30;
    bool disabled;


    private void Update()
    {
        energyBar.energyLevel = energy;
        //healthText.text = currentHealth.ToString();
        if (disabledTime <= 0)
        {
            if (disabled)
            {
                disabled = false;
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        else
        {
            if (!disabled)
            {
                GetComponent<SpriteRenderer>().color = Color.grey;
                disabled = true;
            }
            disabledTime -= Time.deltaTime;
        }
        
    }
    
    public void DisableNode(float timeOfDisabled)
    {
        disabledTime = timeOfDisabled;
    }

    public void UpdateEnergy(int newEnergy)
    {
        if (energy + newEnergy > 100)
        {
            energy = 100;
        }
        else
        {
            energy += newEnergy;

        }

    }
    public void Revive()
    {
        currentHealth = maxHealth;
    }
}

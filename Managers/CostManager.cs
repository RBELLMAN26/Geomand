using UnityEngine;

public class CostManager : MonoBehaviour
{
    //Script provides updates to the cost of items like upgrades and turrets
    //How ever the cost of abilities will gradually go up
    public GameManager gM;
    public int[] costOfUpgrades, costOfTurrets, costOfAbilities;
    public Abilities[] abs;
    int startingCostOfAbilities = 200;
    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<GameManager>();
    }

    public void TimeIncreaseItems()
    {
        for (int i = 0; i < costOfAbilities.Length; i++)
        {
            costOfAbilities[i] = Mathf.RoundToInt(gM.timeInMatch) + startingCostOfAbilities;
        }
    }
    void UpdateCostsToItems()
    {
        for (int i = 0; i < abs.Length; i++)
        {
            //abs[i].scoreCost = costOfAbilities[i];
        }
    }

}

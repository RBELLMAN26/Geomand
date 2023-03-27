using UnityEngine;

public class TowerUpgrades : MonoBehaviour
{
    public GameManager gM;
    public int[] costOfUpgrades;
    public Upgrades[] uP;
    public int startingCost;
    // Start is called before the first frame update
    private void Start()
    {
        gM = FindObjectOfType<GameManager>();
    }
    void OnEnable()
    {
        if (uP.Length == 0)
        {
            //uP = GetComponentsInChildren<Upgrades>();
            print(uP.Length);
            for (int i = 0; i < uP.Length; i++)
            {
                costOfUpgrades[i] = uP[i].scoreCost;
            }
        }


    }

    public void TimeIncreaseItems()
    {
        for (int i = 0; i < costOfUpgrades.Length; i++)
        {
            costOfUpgrades[i] = Mathf.RoundToInt(gM.timeInMatch) + startingCost;
        }
    }
    void UpdateCostsToItems()
    {
        for (int i = 0; i < uP.Length; i++)
        {
            uP[i].scoreCost = costOfUpgrades[i];
        }
    }

}

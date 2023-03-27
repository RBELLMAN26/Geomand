using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public int towerValue;
    public List<Upgrades> uPS = new List<Upgrades>(); //Will Have 50 Upgrades For Each Tower
    public Upgrades[] endlessUpgrades = new Upgrades[4];
    public Vector2[] upPositions;
    public TextMeshPro upgradeDescription;
    public bool endlessMode;

    private void Update()
    {
        if(uPS.Count <= 0 && !endlessMode)
        {
            ActivateEndless();
        }
    }
    //Checks the Costs of Applys The Upgrade For The Turret Specified
    public void ApplyUpgrade(int order)
    {
        if(!endlessMode)
        {
            if (uPS.Count > 0)
            {
                Destroy(uPS[order].gameObject);
                uPS.RemoveAt(order);
                for (int i = 0; i < uPS.Count; i++)
                {
                    if (i < 5)
                    {
                        uPS[i].orderID = i;
                        uPS[i].transform.localPosition = upPositions[i];
                    }
                    else
                    {
                        break;
                    }
                }
            }
        } 
    }

    public void ActivateEndless()
    {
        endlessMode = true;
        FindObjectOfType<GameManager>().finishedUpgrades[towerValue] = true;
        for (int i = 0; i < endlessUpgrades.Length; i++)
        {
            endlessUpgrades[i].gameObject.SetActive(true);
        }
    }

}

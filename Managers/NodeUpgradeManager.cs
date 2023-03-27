using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NodeUpgradeManager : MonoBehaviour
{
    public List<NodeUpgrades> uPS = new List<NodeUpgrades>(); //Will Have 50 Upgrades For Each Tower
    public Vector2[] upPositions;
    public TextMeshPro upgradeDescription;
    public bool endlessMode;
    private void Update()
    {
        if (uPS.Count <= 0 && !endlessMode)
        {
            ActivateEndless();
        }
    }

    public void ApplyUpgrade(int order)
    {
        if(uPS.Count > 0)
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
        else
        {
            FindObjectOfType<GameManager>().finishedUpgrades[4] = true;
        }
       
    }
    public void ActivateEndless()
    {
        endlessMode = true;
        FindObjectOfType<GameManager>().finishedUpgrades[4] = true;
        /*
        for (int i = 0; i < endlessUpgrades.Length; i++)
        {
            endlessUpgrades[i].gameObject.SetActive(true);
        }
        */
    }
}

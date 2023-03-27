using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public int selectedUpgradeColor;
    public int selectedUpgradeNum;
    public float selectedUpgradeAmount;
    public bool affordable;
    public UpgradeManager uM;
    public List<Upgrades> up = new List<Upgrades>();
    public Upgrades selectedUpgrade;
    public double score;
    private LayerMask upgradeMask;
    // Start is called before the first frame update
    void Start()
    {
        upgradeMask = 1 << 8;
        score = FindObjectOfType<GameManager>().score;
        if (uM == null)
        {
            //Debug.LogWarning(transform.parent.name);
            //uM = transform.parent.GetComponent<UpgradeManager>();


        }
        if (up.Count > 0)
        {
            up.Clear();
        }
        for (int i = 0; i < uM.uPS.Count; i++)
        {
            if (i < 5)
            {
                up.Add(uM.uPS[i]);
            }
            else
            {
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        FindUpgrade();
        /*
        if(up.Count == 0)
        {
            for (int i = 0; i < uM.uPS.Count; i++)
            {
                if (i < 5)
                {
                    up.Add(uM.uPS[i]);
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            for(int i = 0;  i < up.Count; i++)
            {
                if(Vector2.Distance(transform.position, up[i].transform.position) < 0.2f)
                {
                    selectedUpgrade = up[i];
                    selectedUpgradeNum = selectedUpgrade.upgradeID;
                    selectedUpgradeAmount = selectedUpgrade.upgradeAmount;
                    up[i].isSelected = true;
                    if(up[i].scoreCost > score)
                    {
                        up[i].affordable = false;
                       
                    }
                    else
                    {
                        up[i].affordable = true;
                        
                    }
                    //print(up[i].transform.name);
                }
                else
                {
                    up[i].isSelected = false;

                }
            }
        }
        */

    }

    void FindUpgrade()
    {
        //print(LayerMask.GetMask("Upgrades"));
        //print(LayerMask.NameToLayer("Upgrades"));
        //LayerMask.NameToLayer("Upgrades")
        Collider2D cols = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("Upgrades"));
        //
        //print(Physics2D.OverlapCircleAll(transform.position, 1f));

        if (cols != null)
        {
            if (selectedUpgrade == null)
            {
                selectedUpgrade = cols.GetComponent<Upgrades>();
                uM.upgradeDescription.text = selectedUpgrade.upgradeText;
                selectedUpgradeColor = selectedUpgrade.colorID;
                selectedUpgradeNum = selectedUpgrade.upgradeID;
                selectedUpgradeAmount = selectedUpgrade.upgradeAmount;
                cols.GetComponent<Upgrades>().isSelected = true;
            }
            else
            {
                selectedUpgrade.isSelected = false;
                selectedUpgrade = cols.GetComponent<Upgrades>();
                uM.upgradeDescription.text = selectedUpgrade.upgradeText;
                selectedUpgradeColor = selectedUpgrade.colorID;
                selectedUpgradeNum = selectedUpgrade.upgradeID;
                selectedUpgradeAmount = selectedUpgrade.upgradeAmount;
                cols.GetComponent<Upgrades>().isSelected = true;
            }
            //print("Found Upgrade");
            Debug.Log(cols.name);
            if (selectedUpgrade.scoreCost > score)
            {
                selectedUpgrade.affordable = false;

            }
            else
            {
                selectedUpgrade.affordable = true;

            }
        }
        else
        {
            if (selectedUpgrade != null)
            {
                uM.upgradeDescription.text = "";
                selectedUpgrade.isSelected = false;
                selectedUpgrade = null;
                selectedUpgradeColor = 0;
                selectedUpgradeNum = 0;
                selectedUpgradeAmount = 0;
            }

        }
    }
}

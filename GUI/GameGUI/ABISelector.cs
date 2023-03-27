using UnityEngine;

public class ABISelector : MonoBehaviour
{
    public int selectedAbilityNum;
    //public Abilities[] abi;
    public Abilities selectedAbility;
    public int selectedUpgradeNum;
    public float selectedUpgradeAmount;
    public bool affordable;
    public NodeUpgradeManager uM;
    //public List<Upgrades> up = new List<Upgrades>();
    public NodeUpgrades selectedUpgrade;
    public double score;
    public int batteryAmount;
    // Start is called before the first frame update
    void Start()
    {
        score = FindObjectOfType<GameManager>().score;
        batteryAmount = FindObjectOfType<NodeInfo>().energy;
        if (uM == null)
        {
            Debug.LogWarning(transform.parent.name);
            //uM = transform.parent.GetComponent<NodeUpgradeManager>();
            uM = FindObjectOfType<NodeUpgradeManager>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        FindUpgrade();
        /*
        if(abi.Length == 0)
        {
            abi = transform.parent.GetComponentsInChildren<Abilities>();
        }
        else
        {
            for(int i = 0;  i < abi.Length; i++)
            {
                if(Vector2.Distance(transform.position, abi[i].transform.position) < 0.2f)
                {
                    selectedAbility = abi[i];
                    selectedAbilityNum = abi[i].abilityID;
                    abi[i].isSelected = true;
                   // print(abi[i].transform.name);
                }
                else
                {
                    abi[i].isSelected = false;
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
        Collider2D cols = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("Upgrades", "Abilities"));
        //
        //print(Physics2D.OverlapCircleAll(transform.position, 1f));


        if (cols != null)
        {
            if (cols.GetComponent<NodeUpgrades>())
            {
                if (selectedAbility != null)
                {
                    selectedAbility.isSelected = false;
                    selectedAbility = null;
                }

                if (selectedUpgrade == null)
                {
                    selectedUpgrade = cols.GetComponent<NodeUpgrades>();
                    uM.upgradeDescription.text = selectedUpgrade.upgradeText;
                    selectedUpgradeNum = selectedUpgrade.upgradeID;
                    selectedUpgradeAmount = selectedUpgrade.upgradeAmount;
                    cols.GetComponent<NodeUpgrades>().isSelected = true;
                    //print("Found Upgrade");
                    Debug.Log(cols.name);
                }
                else
                {
                    selectedUpgrade.isSelected = false;
                    selectedUpgrade = cols.GetComponent<NodeUpgrades>();
                    uM.upgradeDescription.text = selectedUpgrade.upgradeText;
                    selectedUpgradeNum = selectedUpgrade.upgradeID;
                    selectedUpgradeAmount = selectedUpgrade.upgradeAmount;
                    cols.GetComponent<NodeUpgrades>().isSelected = true;
                    //print("Found Upgrade");
                    Debug.Log(cols.name);
                }
                if (selectedUpgrade.scoreCost > score)
                {
                    selectedUpgrade.affordable = false;

                }
                else
                {
                    selectedUpgrade.affordable = true;

                }
            }
            if (cols.GetComponent<Abilities>())
            {
                if (selectedUpgrade != null)
                {
                    selectedUpgrade.isSelected = false;
                    selectedUpgrade = null;
                }
                if (selectedAbility == null)
                {
                    selectedAbility = cols.GetComponent<Abilities>();
                    uM.upgradeDescription.text = selectedAbility.abilityText;
                    selectedAbilityNum = selectedAbility.abilityID;
                    //selectedUpgradeAmount = selectedUpgrade.upgradeAmount;
                    cols.GetComponent<Abilities>().isSelected = true;
                    //print("Found Upgrade");
                    Debug.Log(cols.name);
                }
                else
                {
                    selectedAbility.isSelected = false;
                    selectedAbility = cols.GetComponent<Abilities>();
                    uM.upgradeDescription.text = selectedAbility.abilityText;
                    selectedAbilityNum = selectedAbility.abilityID;
                    //selectedUpgradeAmount = selectedUpgrade.upgradeAmount;
                    cols.GetComponent<Abilities>().isSelected = true;
                    //print("Found Upgrade");
                    Debug.Log(cols.name);
                }
                if (selectedAbility.batteryCost > batteryAmount)
                {
                    selectedAbility.affordable = false;

                }
                else
                {
                    selectedAbility.affordable = true;

                }

            }
        }
        else
        {
            if (selectedUpgrade != null)
            {
                uM.upgradeDescription.text = "";
                selectedUpgrade.isSelected = false;
                selectedUpgrade = null;
                selectedUpgradeNum = 0;
                selectedUpgradeAmount = 0;
            }
            if (selectedAbility != null)
            {
                uM.upgradeDescription.text = "";
                selectedAbility.isSelected = false;
                selectedAbility = null;
                selectedAbilityNum = 0;
                //selectedUpgradeAmount = 0;
            }

        }

    }
}

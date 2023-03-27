using UnityEngine;
using UnityEngine.UI;

public class NodeGUI : MonoBehaviour
{
    public GameManager gM;
    public PlayTouchManager pTM;
    public GameObject selectorpre, selector;
    public Transform NodeAbilities, nodeUpgrades;
    public bool isDragging;
    public int abilitySelection;
    public Abilities[] Abilities;
    public GameObject nodeObject;
    public int costOfNode = 7500;
    public Text nodeCostText;
    public GameObject purchaseObject;
    public GameObject eventsObj;
    [SerializeField] AudioSource bought, declined;
    //public int energyLevel;
    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!nodeObject.activeSelf && !purchaseObject.activeSelf)
        {
            purchaseObject.SetActive(true);
            eventsObj.SetActive(false);
            UpdateCosts();
        }
        else if (nodeObject.activeSelf && purchaseObject.activeSelf)
        {
            purchaseObject.SetActive(false);
            eventsObj.SetActive(true);
        }

    }
    public void PurchaseNode()
    {
        if (CheckCostOfNode(costOfNode))
        {
            //activeTurrets[turretNum] = true;
            //tM.PurchasedTurret(turretNum);
            nodeObject.SetActive(true);
            nodeObject.GetComponent<NodeInfo>().currentHealth = nodeObject.GetComponent<NodeInfo>().maxHealth;
            nodeObject.GetComponent<NodeInfo>().energy = 100;
            UpdateCosts();
        }
    }

    bool CheckCostOfNode(int cost)
    {
        if (cost > gM.score)
        {
            //print("Not Enough Score");
            declined.Play();
            return false;
        }
        else
        {
            gM.score -= cost;
            bought.Play();
            return true;
        }
    }
    public void UpdateCosts()
    {
        costOfNode += 2500 * (gM.difficulty);
        nodeCostText.text = GetLetter(costOfNode.ToString(), costOfNode.ToString().Length);
    }
    string GetLetter(string value, int length)
    {
        string setText = value[0].ToString() + value[1].ToString();
        switch (length)
        {
            case 3:
                {
                    return setText+"0";
                }
            case 4:
                {
                    return setText + "a";
                }
            case 5:
                {
                    return setText + "b";
                }
            case 6:
                {
                    return setText + "c";
                }
            case 7:
                {
                    return setText + "d";
                }
            case 8:
                {
                    return setText + "e";
                }
            case 9:
                {
                    return setText + "f";
                }
            case 10:
                {
                    return setText + "g";
                }
        }
        return setText+"";
    }
    public void BeginDrag()
    {
        if(nodeObject.GetComponent<NodeInfo>().disabledTime <= 0)
        {
            if (!pTM.isTouch[0] && !pTM.isTouch[1])
            {
                Time.timeScale = 0.0f;
                if (gM.touchedObjects == null || gM.touchedObjects == this.gameObject)
                {
                    gM.touchedObjects = this.gameObject;
                    Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    isDragging = true;
                    //print("Dragging");
                    if (selector == null)
                    {
                        selector = (GameObject)Instantiate(selectorpre.gameObject, pos + (Vector2.up / 2), transform.rotation);

                        NodeAbilities.gameObject.SetActive(true);
                        nodeUpgrades.gameObject.SetActive(true);
                        nodeUpgrades.GetComponent<Animator>().SetBool("IsOpen", true);
                        selector.transform.parent = nodeObject.transform;

                    }
                }
            }
        }
    }
    public void StayDrag()
    {
        if(!nodeObject.activeSelf)
        {
            EndDrag();
        }
        if (nodeObject.GetComponent<NodeInfo>().disabledTime <= 0)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selector.transform.position = pos + (Vector2.up / 2);
            //print("Keep Dragging");
        }
    }
    public void EndDrag()
    {
        if (nodeObject.GetComponent<NodeInfo>().disabledTime <= 0)
        {
            Time.timeScale = 1f;
            Abilities ab = selector.GetComponent<ABISelector>().selectedAbility;
            NodeUpgrades up = selector.GetComponent<ABISelector>().selectedUpgrade;
            if (ab != null)
            {
                int num = selector.GetComponent<ABISelector>().selectedAbilityNum;
                selector.GetComponent<ABISelector>().uM.upgradeDescription.text = "";
                print(num);
                if (CheckBattery(num))
                {
                    SendAbility(num);
                }
                ab.isSelected = false;
            }
            if (up != null)
            {
                int num = selector.GetComponent<ABISelector>().selectedUpgradeNum;
                selector.GetComponent<ABISelector>().uM.upgradeDescription.text = "";
                int cost = up.scoreCost;

                print(num);
                if (CheckCost(cost))
                {
                    SendUpgrade(up);
                }
                up.isSelected = false;
            }
            gM.touchedObjects = null;
            Destroy(selector);
            NodeAbilities.gameObject.SetActive(false);
            nodeUpgrades.GetComponent<Animator>().SetBool("IsOpen", false);
            //nodeUpgrades.gameObject.SetActive(false);
            //print("Not Dragging");
        }
    }
    bool CheckBattery(int AbilityNum)
    {
        if (Abilities[AbilityNum].batteryCost > nodeObject.GetComponent<NodeInfo>().energy || Abilities[AbilityNum].aS.recharging)
        {
            //print("Not Enough Score");
            declined.Play();
            return false;
        }
        else
        {
            //gM.score -= Abilities[AbilityNum].scoreCost;
            nodeObject.GetComponent<NodeInfo>().energy -= Abilities[AbilityNum].batteryCost;
            bought.Play();
            return true;
        }
    }
    bool CheckCost(int cost)
    {
        if (cost > gM.score)
        {
            //print("Not Enough Score");
            declined.Play();
            return false;
        }
        else
        {
            gM.score -= cost;
            bought.Play();
            return true;
        }
    }
    void SendAbility(int AbilityNum)
    {
        nodeObject.GetComponent<NodeController>().ActivateAbility(AbilityNum);
    }

    void SendUpgrade(NodeUpgrades upObject)
    {
        nodeObject.GetComponent<NodeController>().UpgradeNode(upObject);
        upObject.transform.parent.GetComponent<NodeUpgradeManager>().ApplyUpgrade(upObject.orderID);

    }

}

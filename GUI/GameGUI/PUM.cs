using UnityEngine;
using UnityEngine.UI;

//P.U.M - Purchasing, Upgrades, Manager.
//Manages Purchasing Of Turrets and Upgrades and once upgraded will push to the turret.
public class PUM : MonoBehaviour
{
    public GameManager gM;
    public TowerManager tM;
    public WeaponBarManager wBM;
    //Checks The Turrets At Start To See Which Ones Are Active.  Will Determine Which Towers Need To Be Purchased
    public bool[] activeTurrets = new bool[4];
    //Will Equal Variable Of ActiveTurrets after Turrets scanned at start.
    public int[] CostOfTurrets = new int[4];
    public Transform[] purchaseTurrets = new Transform[4];
    public Transform[] upgradeTree = new Transform[4];
    int currentDifficulty;
    [SerializeField] AudioSource bought, declined;
    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<GameManager>();
        ScanTurrets();
    }

    //Scans If Turrets Are Disabled Are Active
    public void ScanTurrets()
    {
        for (int i = 0; i < activeTurrets.Length; i++)
        {
            if (!activeTurrets[i])
            {
                purchaseTurrets[i].gameObject.SetActive(true);
            }
            else
            {
                purchaseTurrets[i].gameObject.SetActive(false);
            }

        }
        wBM.UpdateBars();
    }
    public void PurchaseTurrets(int turretNum)
    {
        //print("Purchase Turret");
        //if(CheckCost(purchaseTurrets[turretNum].GetComponent<TurretPurchase>()))
        if (CheckCost(CostOfTurrets[turretNum]))
        {
            activeTurrets[turretNum] = true;
            tM.PurchasedTurret(turretNum);
            UpdateCosts();
        }
        ScanTurrets();


    }
    bool CheckCost(int cost)
    {
        if (cost > gM.score)
        {
            print("Not Enough Score");
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
        for (int i = 0; i < CostOfTurrets.Length; i++)
        {
            if (gM.difficulty > 0)
            {
                CostOfTurrets[i] = Mathf.RoundToInt(CostOfTurrets[i] + (200 * gM.difficulty));
                //purchaseTurrets[i].GetComponentInChildren<Text>().text = CostOfTurrets[i].ToString();
                purchaseTurrets[i].GetComponentInChildren<Text>().text = GetLetter(CostOfTurrets[i].ToString(), CostOfTurrets[i].ToString().Length);
            }
            else
            {
                CostOfTurrets[i] = Mathf.RoundToInt(CostOfTurrets[i] + 200);
                //purchaseTurrets[i].GetComponentInChildren<Text>().text = CostOfTurrets[i].ToString();
                purchaseTurrets[i].GetComponentInChildren<Text>().text = GetLetter(CostOfTurrets[i].ToString(), CostOfTurrets[i].ToString().Length);

            }

        }
    }
    string GetLetter(string value, int length)
    {
        string setText = value[0].ToString() + value[1].ToString();
        switch (length)
        {
            case 3:
                {
                    return setText + "0";
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
        return setText + "";
    }
}

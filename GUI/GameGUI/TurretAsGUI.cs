using UnityEngine;
using UnityEngine.UI;

public class TurretAsGUI : MonoBehaviour
{
    public GameManager gM;
    public TowerManager tM;
    public PlayTouchManager pTM;
    public PUM pum;
    //public Transform towerUpgrades;
    public GameObject towerObject;
    public GameObject selectorpre, selector;
    public GameObject[] tAssignedButtons = new GameObject[4];
    //public Text[] turretAssignments = new Text[4];
    public Text[] turretCosts = new Text[4];
    public bool isDragging;
    [SerializeField] AudioSource bought, declined;
    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<GameManager>();
        /*
        for (int i = 0;  i < tAssignedButtons.Length;  i++)
        {
            turretAssignments[i] = tAssignedButtons[i].GetComponentInChildren<Text>();
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < pum.activeTurrets.Length; i++)
        {
            tAssignedButtons[i].SetActive(pum.activeTurrets[i]);
            //turretAssignments[i].text = tM.assignedTurretTouchs[i].ToString();
        }
    }
    public void ChangeTurretAssignment(int Turret)
    {
        if (isDragging)
        {
            isDragging = false;
        }
        else
        {
            tM.UpdateTurretAssignment(Turret);
        }
    }
    public void BeginDrag(int turretNum)
    {
        if (!pTM.isTouch[0] && !pTM.isTouch[1])
        {
            if (gM.touchedObjects == null || gM.touchedObjects == this.gameObject)
            {
                gM.touchedObjects = this.gameObject;
            }
            pTM.gameObject.SetActive(false);
            Time.timeScale = 0.0f;
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;

            //print("Dragging");
            if (selector == null)
            {
                selector = (GameObject)Instantiate(selectorpre.gameObject, pos + (Vector2.up / 2), transform.rotation);
                selector.GetComponent<Selector>().uM = pum.upgradeTree[turretNum].GetComponent<UpgradeManager>();
                pum.upgradeTree[turretNum].gameObject.SetActive(true);
                pum.upgradeTree[turretNum].GetComponent<Animator>().SetBool("IsOpen", true);
                //selector.transform.parent = pum.upgradeTree[turretNum];

            }
        }


    }
    public void StayDrag(int turretNum)
    {
        if(!pum.tM.turrets[turretNum].gameObject.activeSelf)
        {
            EndDrag(turretNum);
        }
        if (gM.touchedObjects == null || gM.touchedObjects == this.gameObject)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selector.transform.position = pos + (Vector2.up);
            //print("Keep Dragging");
        }

    }
    public void EndDrag(int turretNum)
    {
        //Destroy(selector);
        Time.timeScale = 1f;
        pum.upgradeTree[turretNum].GetComponent<Animator>().SetBool("IsOpen", false);
        //pum.upgradeTree[turretNum].gameObject.SetActive(false);
       
        towerObject = pum.tM.turrets[turretNum].gameObject;
        Upgrades abSel = selector.GetComponent<Selector>().selectedUpgrade;
        if (abSel != null)
        {
            if (CheckCost(abSel))
            {
                int num = selector.GetComponent<Selector>().selectedUpgradeNum;
                float amount = selector.GetComponent<Selector>().selectedUpgradeAmount;
                selector.GetComponent<Selector>().uM.upgradeDescription.text = "";
                //print(num);
                SendUpgrade(abSel);
            }
            else
            {
                //Make A Error Sound Effect
                selector.GetComponent<Selector>().uM.upgradeDescription.text = "";
            }
            abSel.isSelected = false;
        }

        
        Destroy(selector);
        pTM.gameObject.SetActive(true);
        gM.touchedObjects = null;
        //towerUpgrades.gameObject.SetActive(false);
        print("Not Dragging");
    }
    bool CheckCost(Upgrades obj)
    {
        if (obj.scoreCost > gM.score)
        {
            print("Not Enough Score");
            declined.Play();
            return false;
        }
        else
        {
            gM.score -= obj.scoreCost;
            bought.Play();
            return true;
        }
    }
    void SendUpgrade(Upgrades upObject)
    {
        towerObject.GetComponent<TowerController>().UpgradeTurret(upObject);

    }
}

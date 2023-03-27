using TMPro;
using UnityEngine;

public class NodeUpgrades : MonoBehaviour
{
    public int orderID = -1; //Where in the active Order, -1 means its not in the active order of 5
    public int upgradeID; //If 0 - Health, 1 - DeathWave, 2 - Shield, 3 - Other
    public int optionID; //0 - HitPoints, 1 - Damage, 2 - Duration, 3 - Speed
    public string upgradeText;
    public float upgradeAmount;
    public int scoreCost = 100;
    public bool isSelected, affordable;
    public GameObject highLight;
    SpriteRenderer sR;
    SpriteRenderer hR;
    public TextMeshPro upText;
    public TextMeshPro upAmount;

    private void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
        hR = highLight.GetComponent<SpriteRenderer>();
        upText.text = scoreCost.ToString();
        upAmount.text = upgradeAmount.ToString();

    }
    private void Update()
    {
        highLight.SetActive(isSelected);
        if (isSelected)
        {
            if (affordable)
            {
                if (hR.color != new Color(0, 255, 0, 0.5f))
                {
                    hR.color = new Color(0, 255, 0, 0.5f);
                }

            }
            else
            {
                if (hR.color != new Color(255, 0, 0, 0.5f))
                {
                    //hR.color = Color.red;
                    hR.color = new Color(255, 0, 0, 0.5f);
                }

            }
        }
        else
        {
            if (hR.color != new Color(255, 255, 255, 1))
            {
                hR.color = new Color(255, 255, 255, 1);
            }

        }

    }
}

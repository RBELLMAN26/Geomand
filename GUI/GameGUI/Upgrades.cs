using TMPro;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public int orderID = -1; //Where in the active Order, -1 means its not in the active order of 5
    public int colorID; //Applys Upgrade To 0 - Green, 1 - Blue, 2 - Red, 3 - All Colors
    public int upgradeID; //If 0 - Health, 1 - FireRate, 2 - Damage, 3 - Projectile Speed, 4 - SlowTime, 5 - Other

    public string upgradeText;
    public float upgradeAmount;
    public int scoreCost = 100;
    public bool isSelected, affordable;
    public GameObject highLight;
    public SpriteRenderer hR;
    public SpriteRenderer sR;
    public TextMeshPro costText, amountText;
    private void Start()
    {
        costText.text = scoreCost.ToString();
        amountText.text = upgradeAmount.ToString();
        sR = GetComponent<SpriteRenderer>();
        hR = highLight.GetComponent<SpriteRenderer>();
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

using UnityEngine;

public class Abilities : MonoBehaviour
{
    public AbilityStats aS;
    public string abilityText;
    public int abilityID;
    public bool isSelected, affordable;
    public int batteryCost = 30;
    public GameObject highLight;
    public SpriteRenderer hR;

    private void Start()
    {
        hR = highLight.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (aS == null)
        {
            aS = GetComponent<AbilityStats>();
        }
        highLight.SetActive(isSelected);
        if (affordable && !aS.recharging)
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
}

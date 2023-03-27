using TMPro;
using UnityEngine;

public class NodeHealthText : MonoBehaviour
{
    public NodeInfo nI;
    public TextMeshPro healthText;
    public int healthlevel = 1;
    public int nextLevel;
    public string healthletter; //Goes Through All Letters starting A = 0, B = 00;
    // Start is called before the first frame update
    void Awake()
    {
        nI = GetComponentInParent<NodeInfo>();
        healthText = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {

        nextLevel = (int)Mathf.Pow(10.0f, (float)healthlevel);

        if (nI.currentHealth.ToString().Length > 3)
        {
            CheckLevel();
            healthlevel = nI.currentHealth.ToString().Length - 1;
            int firsttwo = nI.currentHealth / (int)Mathf.Pow(10.0f, (float)healthlevel - 1);
            healthText.text = firsttwo.ToString() + healthletter;

        }
        else
        {
            healthText.text = nI.currentHealth.ToString();
        }
    }

    void CheckLevel()
    {
        //if (nI.currentHealth.ToString().Length > (int)Mathf.Pow(10.0f, (float)healthlevel).ToString().Length)
        //{
        switch (nI.currentHealth.ToString().Length)
        {
            case 4:
                {
                    healthletter = "a";
                    break;
                }
            case 5:
                {
                    healthletter = "b";
                    break;
                }
            case 6:
                {
                    healthletter = "c";
                    break;
                }
            case 7:
                {
                    healthletter = "d";
                    break;
                }
            case 8:
                {
                    healthletter = "e";
                    break;
                }
            case 9:
                {
                    healthletter = "f";
                    break;
                }
            case 10:
                {
                    healthletter = "g";
                    break;
                }
            case 11:
                {
                    healthletter = "h";
                    break;
                }
            case 12:
                {
                    healthletter = "i";
                    break;
                }
            case 13:
                {
                    healthletter = "j";
                    break;
                }
            case 14:
                {
                    healthletter = "k";
                    break;
                }
            case 15:
                {
                    healthletter = "l";
                    break;
                }
            case 16:
                {
                    healthletter = "m";
                    break;
                }
        }
    }
}
